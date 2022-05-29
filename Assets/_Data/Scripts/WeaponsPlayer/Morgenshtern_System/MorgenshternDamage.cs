using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgenshternDamage : MonoBehaviour
{
    private float damage;

    [Space]
    [Range(0, 100)]
    public float chanceCauseBleeding;

    private PlayerDamageController playerDamageController;

    private void Start()
    {
        playerDamageController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageController>();
        // назначаем урон
        damage = playerDamageController.defaultDamageMorgenshtern;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // назначаем урон
            damage = playerDamageController.defaultDamageMorgenshtern;
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            ActivateAttackEffect(collision.gameObject);
        }
    }

    private void ActivateAttackEffect(GameObject target)
    {
        // вызов кровотечения 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            bool isApplyEffect = IsApplyEffect();
            if (isApplyEffect && effectActivator.IsEffectBleedingActicated())   // с определенным шансом + если объект уже активирован, то просто обнуляем
            {
                effectActivator.UpdateEffectBleeding();
            }
            else if (isApplyEffect)    // с определенным шансом
            {
                effectActivator.CallEffectBleeding();
            }
        }
    }

    private bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceCauseBleeding;
    }
}
