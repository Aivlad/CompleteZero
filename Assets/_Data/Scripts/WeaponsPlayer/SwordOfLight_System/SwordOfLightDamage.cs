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
            // ��������� ������� �����
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
        // ����� ����� ������ 5 ������
        if (countWeaponKills > 0 && countWeaponKills % 5 == 0 && !isEffectActivated)
        {
            isEffectActivated = true; // ����, ����� ����� ������� 5�� ����� ������ ������� ������ �� ���� ����
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
