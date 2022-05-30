using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedThrowingKnivesDamage : InfluenceOnAttack
{
    private float damage;
    public float destroyDistance;
    [Space]
    public int countTargetsHit;
    public int countTargetsHitMax;

    private Vector3 spawnPosition;

    private void Start()
    {
        DataInitialization();

        spawnPosition = transform.position;
        countTargetsHit = 0;

        // ��������� ����
        damage = playerDamageController.defaultDamagePoisonedThrowingKnives;
    }

    private void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ��������� ������� �����
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
            //balance
            playerDamageController.AddOutingDamage(damage);

            // ������ ������ �����
            ActivateAttackPoisoningEffect(collision.gameObject, IsApplyEffect());

            // ��� ������ ����� (������., ��� ������� �����)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);

            // ����������� ���� ����� ������������ ���� ������
            countTargetsHit++;
            if (countTargetsHit >= countTargetsHitMax)
            {
                Destroy(gameObject);
            }
        }
        if (collision.transform.CompareTag("Wall"))
        {
            //Debug.Log("����� ��������");
            Destroy(gameObject);
        }
    }
}
