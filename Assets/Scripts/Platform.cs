using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public static UnityEvent<Platform> OnAchieve = new UnityEvent<Platform>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnAchieve?.Invoke(this);
    }
}
