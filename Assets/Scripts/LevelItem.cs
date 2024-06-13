using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private Transform levelLine;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform frog;

    public static UnityAction OnLose; //send progress
    public static UnityAction<float> OnProgressUpdate;

    public float ProgressPercent => (maxDistance - Vector2.Distance(endPoint.position, frog.position)) / maxDistance;

    bool moveFrog;
    Vector3 frogTarget;
    Platform last;

    float maxDistance;
    float remainStart = 1f;

    public void Start()
    {
        TargetListener.OnLaunch.AddListener(Launch);
        Platform.OnAchieve.AddListener(OnAchieve);
        EnemyTrigger.OnEnemyHit.AddListener(OnEnemyHit);

        maxDistance = Vector2.Distance(endPoint.position, frog.position);
    }

    // Update is called once per frame
    void Update()
    {
        remainStart -= Time.deltaTime;
        if (remainStart > 0f) return;

        if(endPoint.position.y > 4.2f)
            levelLine.Translate(Vector2.down * Time.deltaTime * 0.6f);

        if(frog.position.y < -4.8f)
        {
            OnLose?.Invoke();
        }

        if(moveFrog)
        {
            frog.position = Vector2.Lerp(frog.position, frogTarget, Time.deltaTime * 2f);
            if (Vector2.Distance(frog.position, frogTarget) < 0.5f)
                OnLose?.Invoke();

            Vector2 dir = frogTarget - frog.position;
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            frog.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else if(last != null)
        {
            frog.position = last.transform.position;
        }
    }

    private void Launch(Vector3 _vec)
    {
        if (moveFrog) return;

        SoundController.Instance.Jump();
        Debug.Log($"Launch {_vec}");
        frogTarget = _vec;
        moveFrog = true;
    }

    private void OnAchieve(Platform pl)
    {
        moveFrog = false;
        frog.rotation = Quaternion.identity;
        last = pl;

        OnProgressUpdate?.Invoke(ProgressPercent);
    }

    private void OnEnemyHit()
    {
        OnLose?.Invoke();
    }
}
