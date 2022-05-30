using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgenshternDamage : InfluenceOnAttack
{
    private float damage;


    private void Start()
    {
        DataInitialization();

        // ��������� ����
        damage = playerDamageController.defaultDamageMorgenshtern;

        Debug.Log("Start 2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ��������� ����
            damage = playerDamageController.defaultDamageMorgenshtern;
            // ��������� ������� �����
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
            //balance
            playerDamageController.AddOutingDamage(damage);

            // ������ ������ �����
            ActivateAttackBleedingEffect(collision.gameObject, IsApplyEffect());

            // ��� ������ ����� (������., ��� ������� �����)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);
        }
    }    
}
