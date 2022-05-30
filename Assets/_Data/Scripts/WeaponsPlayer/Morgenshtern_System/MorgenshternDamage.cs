using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgenshternDamage : InfluenceOnAttack
{
    private float damage;


    private void Start()
    {
        DataInitialization();

        // назначаем урон
        damage = playerDamageController.defaultDamageMorgenshtern;

        Debug.Log("Start 2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // назначаем урон
            damage = playerDamageController.defaultDamageMorgenshtern;
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
            //balance
            playerDamageController.AddOutingDamage(damage);

            // родной эффект атаки
            ActivateAttackBleedingEffect(collision.gameObject, IsApplyEffect());

            // доп эффект атаки (наприм., при наличии итема)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);
        }
    }    
}
