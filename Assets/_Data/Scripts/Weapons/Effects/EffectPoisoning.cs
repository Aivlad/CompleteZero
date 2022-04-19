using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoisoning : MonoBehaviour
{
    public ObjectCharacteristics effectTarget;
    [Space]
    public float damage;
    private float startingDamage;
    public float damageDelay;
    public float duration;

    private int countCallTotal;
    private int countCallCurrent;

    private void OnEnable()
    {
        // вычисляем сколько раз мы должны нанести урон
        countCallTotal = (int)(duration / damageDelay);
        countCallCurrent = 0;
        // сохранение стартового значения яда
        startingDamage = damage;
        // запускаем процесс нанесения урона
        StartCoroutine(ToDamage());
    }

    public void EffectUpdate()
    {
        countCallCurrent = 0;
        // при обновление яд стакается
        damage++;
    }

    private IEnumerator ToDamage()
    {
        yield return new WaitForSeconds(damageDelay);
        countCallCurrent++;
        DamageAction();
        if (countCallCurrent >= countCallTotal)
        {
            damage = startingDamage;    // значение урона яда возвращаем назад
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
