using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandDamage : InfluenceOnAttack
{
    private float damage;

    [Space]
    public GameObject fireZonePrefab;
    private bool IsTargetHit;   // флаг, что мы попали в цель

    private void Start() 
    {
        DataInitialization();

        Destroy(gameObject, 0.3f);
        IsTargetHit = false;

        // назначаем урон
        damage = playerDamageController.defaultDamageWand;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            // родной эффект атаки
            ActivateAttackFireEffect(collision.gameObject, IsApplyEffect());

            // доп эффект атаки (наприм., при наличии итема)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);

            IsTargetHit = true;
        }
    }

    private void OnDestroy()
    {
        // если мы не попали в цель, то с шансом вызываем зону огня в области попадания
        if (!IsTargetHit && IsApplyEffect())
        {
            Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
        }
    }
}
