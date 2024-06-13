using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetListener : MonoBehaviour, IPointerDownHandler
{
    public static UnityEvent<Vector3> OnLaunch = new UnityEvent<Vector3>();

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //hits for sure, because we are in the click event
        Physics.Raycast(ray, out hit);

        Vector3 tr = new Vector3(hit.point.x, hit.point.y, 0f);
        OnLaunch?.Invoke(tr);
    }
}
