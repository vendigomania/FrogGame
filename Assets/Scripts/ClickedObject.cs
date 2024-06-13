using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickedObject : MonoBehaviour
{
    [SerializeField] private GameObject mark;

    public Transform markTransform => mark.transform;
}
