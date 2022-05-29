using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    [Header("Attack Damage")]
    public float defaultDamageSwordOfLight = 15;
    public float defaultDamageMorgenshtern = 20;
    public float defaultDamageWand = 10;
    public float defaultDamagePoisonedThrowingKnives = 7;

    [Header("Additional Effects Flags")]
    public bool isAdditionBleedingEffect = false;
    public bool isAdditionPoisoningEffect = false;
    public bool isAdditionFireEffect = false;

    public void IncreaseDamage(float increasePercentage)
    {
        defaultDamageSwordOfLight += ConvertFromPercentToValue(defaultDamageSwordOfLight, increasePercentage);
        defaultDamageMorgenshtern += ConvertFromPercentToValue(defaultDamageMorgenshtern, increasePercentage);
        defaultDamageWand += ConvertFromPercentToValue(defaultDamageWand, increasePercentage);
        defaultDamagePoisonedThrowingKnives += ConvertFromPercentToValue(defaultDamagePoisonedThrowingKnives, increasePercentage);
    }

    private float ConvertFromPercentToValue(float value, float percent)
    {
        return (value * percent) / 100;
    }
}
