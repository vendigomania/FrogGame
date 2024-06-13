using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private bool isFinish;

    public static UnityEvent OnFinish = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFinish) OnFinish?.Invoke();
    }
}
