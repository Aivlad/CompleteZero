using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgenshternDamage : MonoBehaviour
{
    public float damage;

    [Space]
    [Range(0, 100)]
    public float chanceCauseBleeding;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ��������� ������� �����
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            ActivateAttackEffect(collision.gameObject);
        }
    }

    private void ActivateAttackEffect(GameObject target)
    {
        // ����� ������������ 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            bool isApplyEffect = IsApplyEffect();
            if (isApplyEffect && effectActivator.IsEffectBleedingActicated())   // � ������������ ������ + ���� ������ ��� �����������, �� ������ ��������
            {
                effectActivator.UpdateEffectBleeding();
            }
            else if (isApplyEffect)    // � ������������ ������
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
