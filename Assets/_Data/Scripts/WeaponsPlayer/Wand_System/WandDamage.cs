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
    private bool IsTargetHit;   // ����, ��� �� ������ � ����

    private void Start()
    {
        Destroy(gameObject, 0.3f);
        IsTargetHit = false;

        // ��������� ����
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageController>().defaultDamageWand;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ��������� ������� �����
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            ActivateAttackEffect(collision.gameObject);
            IsTargetHit = true;
        }
    }

    private void OnDestroy()
    {
        // ���� �� �� ������ � ����, �� � ������ �������� ���� ���� � ������� ���������
        if (!IsTargetHit && IsApplyEffect())
        {
            Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
        }
    }

    private void ActivateAttackEffect(GameObject target)
    {
        // ����� ����
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            bool isApplyEffect = IsApplyEffect();
            if (isApplyEffect && effectActivator.IsEffectFireActivated())   // � ������������ ������ + ���� ������ ��� �����������, �� ������ ��������
            {
                effectActivator.UpdateEffectFire();
            }
            else if (isApplyEffect)    // � ������������ ������
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
