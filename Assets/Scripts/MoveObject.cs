using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float angle = 1f;

    Vector3 scale;
    private void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 2f)
        {
            angle = -1f;
        }
        else if(transform.position.x < -2f)
        {
            angle = 1f;
        }

        transform.localScale = new Vector3(scale.x * angle, scale.y, scale.z);
        transform.Translate(Vector2.right * Time.fixedDeltaTime * angle * 0.6f);
    }
}
