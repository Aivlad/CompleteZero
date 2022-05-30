using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDamage : MonoBehaviour
{
    public float damage = 0;
    public string damageText = "";
    public TextMesh textMesh;

    private void Start()
    {
        if (damage != 0)
            textMesh.text = "-" + damage;
        else
            textMesh.text = damageText;
    }

    public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}
