using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBleeding : MonoBehaviour
{
    public ObjectCharacteristics effectTarget;
    [Space]
    public float damage;
    public float damageDelay;
    public float duration;

    private int countCallTotal;
    private int countCallCurrent;

    private void OnEnable()
    {
        // вычисляем сколько раз мы должны нанести урон
        countCallTotal = (int)(duration / damageDelay);
        countCallCurrent = 0;
        // запускаем процесс нанесения урона
        StartCoroutine(ToDamage());
    }

    public void EffectUpdate()
    {
        countCallCurrent = 0;
    }

    private IEnumerator ToDamage()
    {
        yield return new WaitForSeconds(damageDelay);
        countCallCurrent++;
        DamageAction();
        if (countCallCurrent >= countCallTotal)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ToDamage());
        }
    }

    private void DamageAction()
    {
        effectTarget.DealDamage(damage);
    }
}
