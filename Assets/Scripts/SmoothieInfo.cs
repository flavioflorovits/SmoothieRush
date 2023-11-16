using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothieInfo : MonoBehaviour
{

    public SpriteRenderer colorSprite;
    public SmoothieSO smoothieSO;
    public GameObject blender;

    private void OnDestroy()
    {
        blender.SetActive(true);
    }

}
