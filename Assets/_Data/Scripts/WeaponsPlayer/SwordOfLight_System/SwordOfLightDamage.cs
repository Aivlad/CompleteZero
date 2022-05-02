using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightDamage : MonoBehaviour
{
    public float damage;
    private float defaultDamage;
    [Space]
    public int countWeaponKills;
    private bool isEffectActivated; // ����: ��� ������ ������� ����� ����� ������� 5 �����

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
            // ��������� ������� �����
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
        // ������ 5� ���� = +5 ����� (����������)
        if (isEffectActivated && countWeaponKills > 0 && countWeaponKills % 5 == 0)
        {
            isEffectActivated = false;
            damage += 5;
        }
    }
}
