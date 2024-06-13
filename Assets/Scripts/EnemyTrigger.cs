using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    public static UnityEvent OnEnemyHit = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnemyHit?.Invoke();
    }
}
