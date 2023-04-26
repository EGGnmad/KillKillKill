using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite face;
    Sprite idleFace;

    private void Start()
    {
        idleFace = renderer.sprite;
    }

    public void ChangeFaceToIdle()
    {
        renderer.sprite = idleFace;
    }

    public void ChangeFaceToOther()
    {
        renderer.sprite = face;
    }

    
}
