using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightDamage : MonoBehaviour
{
    public float damage;
    [Space]
    public int countWeaponKills;
    public bool isEffectActivated;

    private void Start()
    {
        countWeaponKills = 0;
        isEffectActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            ActivateAttackEffect(collision.gameObject);
        }
    }

    public void CountMurder()
    {
        countWeaponKills++;
        isEffectActivated = false;
    }

    private void ActivateAttackEffect(GameObject target)
    {
        // вызов света каждые 5 киллов
        if (countWeaponKills > 0 && countWeaponKills % 5 == 0 && !isEffectActivated)
        {
            isEffectActivated = true; // флаг, чтобы после каждого 5го килла эффект вешался только на одну цель
            EffectActivator effectActivator = target.GetComponent<EffectActivator>();
            if (effectActivator != null)
            {
                if (effectActivator.IsEffectLightActivated())
                {
                    effectActivator.UpdateEffectLight();
                }
                else
                {
                    effectActivator.CallEffectLight();
                }
            }
        }
    }
}
