using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject activeBtn;
    [SerializeField] private GameObject inactiveBtn;
    [SerializeField] private TMP_Text lvlText;

    public static UnityAction<int> OnClick;

    private int Level;

    public void Init(int level)
    {
        Level = level;
        lvlText.text = (level + 1).ToString();
    }

    public void UpdateView(bool active)
    {
        activeBtn.SetActive(active);
        inactiveBtn.SetActive(!active);
    }

    public void Click()
    {
        OnClick?.Invoke(Level);
    }
}
