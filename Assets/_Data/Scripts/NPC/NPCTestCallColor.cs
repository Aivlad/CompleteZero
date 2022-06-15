using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTestCallColor : MonoBehaviour
{
    public Color color;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
