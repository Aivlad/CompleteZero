using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightDamage : MonoBehaviour
{
    public float damage;
    private float defaultDamage;
    [Space]
    public int countWeaponKills;
    private bool isEffectActivated; // флаг: баф только первого удара после каждого 5 килла

    private void Start()
    {
        countWeaponKills = 0;

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
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
            
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
