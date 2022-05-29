using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandDamage : MonoBehaviour
{
    private float damage;

    [Space]
    [Range(0, 100)]
    public float chanceCauseFire;

    [Space]
    public GameObject fireZonePrefab;
    private bool IsTargetHit;   // флаг, что мы попали в цель

    private void Start()
    {
        Destroy(gameObject, 0.3f);
        IsTargetHit = false;

        // назначаем урон
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageController>().defaultDamageWand;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            ActivateAttackEffect(collision.gameObject);
            IsTargetHit = true;
        }
    }

    private void OnDestroy()
    {
        // если мы не попали в цель, то с шансом вызываем зону огн€ в области попадани€
        if (!IsTargetHit && IsApplyEffect())
        {
            Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
        }
    }

    private void ActivateAttackEffect(GameObject target)
    {
        // вызов огн€
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            bool isApplyEffect = IsApplyEffect();
            if (isApplyEffect && effectActivator.IsEffectFireActivated())   // с определенным шансом + если объект уже активирован, то просто обнул€ем
            {
                effectActivator.UpdateEffectFire();
            }
            else if (isApplyEffect)    // с определенным шансом
            {
                effectActivator.CallEffectFire();
            }
        }
    }

    private bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceCauseFire;
    }

}
