using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalCharacteristics : MonoBehaviour
{
    public float health;
    public float healthMax;

    public bool isShakeDamage = true;

    public virtual void DealDamage(float damage)
    {
        Debug.LogError("Забыл переопределить");
    }
}
