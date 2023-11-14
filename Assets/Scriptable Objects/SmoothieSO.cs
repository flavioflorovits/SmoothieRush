using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SmoothieSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public string objectName;

    public List<string> recipe;
}
