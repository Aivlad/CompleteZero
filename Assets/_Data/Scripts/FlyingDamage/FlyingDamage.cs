using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDamage : MonoBehaviour
{
    public float damage;
    public TextMesh textMesh;

    private void Start()
    {
        textMesh.text = "-" + damage;
    }

    public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}
