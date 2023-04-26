using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollRange = 20f;
    [SerializeField]
    private float speed = 3f;

    public bool canScroll = true;

    void Update()
    {
        if (!canScroll) return;

        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        if (transform.position.x <= -scrollRange)
        {
            transform.position = target.position + Vector3.right * scrollRange;
        }
    }

    public void StartScroll()
    {
        canScroll = true;
    }

    public void StopScroll()
    {
        canScroll = false;
    }
}

