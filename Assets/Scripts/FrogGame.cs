using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrogGame : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject levelsScreen;
    [SerializeField] private LevelButton[] levelButton = new LevelButton[9];
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private TMP_Text progressLable;
    [SerializeField] private TMP_Text timeLable;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text endResultTitle;
    [SerializeField] private GameObject winImg;
    [SerializeField] private GameObject loseImg;
    [SerializeField] private TMP_Text nextAgainOption;

    [Header("Ingame")]
    [SerializeField] private Transform levelRoot;
    LevelItem[] levelItems;

    private int MaxLevel
    {
        get => PlayerPrefs.GetInt("MaxLevel", 0);
        set => PlayerPrefs.SetInt("MaxLevel", value);
    }

    private int currentLevel;
    private LevelItem item;
    private float time;

    // Start is called before the first frame update
    void Start()
    {

        int variant = Random.Range(0, sprites.Length);

        Screen.orientation = ScreenOrientation.Portrait;

        for(var i = 1; i < 9; i++)
        {
            levelButton[i] = Instantiate(levelButton[0], levelButton[0].transform.parent);
            levelButton[i].Init(i);
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            if (i == variant) background.sprite = sprites[i];
        }

        LevelButton.OnClick += (lvl) =>
        {
            currentLevel = lvl;
            StartGame();
        };

        levelItems = Resources.LoadAll<LevelItem>("/");

        LevelItem.OnProgressUpdate += (value) => progressLable.text = (value * 100f).ToString("0.0") + "%";
        LevelItem.OnLose += () => ShowEnd(false);
        EndPoint.OnFinish.AddListener(() => ShowEnd(true));

        for(int i = 0; i< sprites.Length; i++)
        {
            if(i == variant) background.sprite = sprites[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null) return;

        time += Time.deltaTime;
        timeLable.text = time.ToString("0.00") + "s";
    }

    public void ShowLevels()
    {
        startScreen.SetActive(false);
        levelsScreen.SetActive(true);

        for(var i = 0; i < 9; i++)
        {
            levelButton[i].UpdateView(i <= MaxLevel);
        }
    }

    public void StartGame()
    {
        int variant = Random.Range(0, sprites.Length);

        for (int i = 0; i < sprites.Length; i++)
        {
            if (i == variant) background.sprite = sprites[i];
        }

        levelsScreen.SetActive(false);
        gameScreen.SetActive(true);
        endScreen.SetActive(false);
        time = 0f;
        progressLable.text = "0%";
        timeLable.text = "0s";

        item = Instantiate(levelItems[currentLevel], levelRoot);



        for (int i = 0; i < sprites.Length; i++)
        {
            if (i == variant) background.sprite = sprites[i];
        }
    }

    public void Menu()
    {
        startScreen.SetActive(true);
        endScreen.SetActive(false);
    }

    private void ShowEnd(bool _isWin)
    {
        gameScreen.SetActive(false);


        if(_isWin)
        {
            if (currentLevel < 8) 
            {
                if(currentLevel == MaxLevel) MaxLevel++;
                currentLevel++;
            }
            SoundController.Instance.Win();
        }
        else SoundController.Instance.Lose();

        endResultTitle.text = _isWin ?
            time.ToString("0.00") + "s" :
            progressLable.text = (item.ProgressPercent * 100f).ToString("0.0") + "%";

        winImg.SetActive(_isWin);
        loseImg.SetActive(!_isWin);

        nextAgainOption.text = _isWin && currentLevel < 8 ? "Next level" : "Retry";

        endScreen.SetActive(true);
        Destroy(item.gameObject);
    }
}
