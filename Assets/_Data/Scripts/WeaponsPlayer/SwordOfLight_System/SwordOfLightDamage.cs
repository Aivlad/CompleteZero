using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightDamage : InfluenceOnAttack
{
    private float damage;
    private float defaultDamage;
    [Space]
    public int countWeaponKills;
    private bool isEffectActivated; // флаг: баф только первого удара после каждого 5 килла


    private void Start()
    {
        DataInitialization();

        countWeaponKills = 0;

        // назначаем урон
        damage = playerDamageController.defaultDamageSwordOfLight;
        defaultDamage = damage;
        isEffectActivated = true;
    }

    private void Update()
    {
        ActivateAttackEffect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // назначаем урон
            defaultDamage = playerDamageController.defaultDamageSwordOfLight;
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            // доп эффект атаки (наприм., при наличии итема)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);

            damage = defaultDamage;
        }
    }

    public void CountMurder()
    {
        countWeaponKills++;

        isEffectActivated = true;
    }

    private void ActivateAttackEffect()
    {
        // каждый 5й килл = +5 урона (одноразово)
        if (isEffectActivated && countWeaponKills > 0 && countWeaponKills % 5 == 0)
        {
            isEffectActivated = false;
            damage += 5;
        }
    }
}
