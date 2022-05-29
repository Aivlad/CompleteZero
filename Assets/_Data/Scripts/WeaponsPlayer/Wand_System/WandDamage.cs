using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandDamage : InfluenceOnAttack
{
    private float damage;

    [Space]
    public GameObject fireZonePrefab;
    private bool IsTargetHit;   // ����, ��� �� ������ � ����

    private void Start() 
    {
        DataInitialization();

        Destroy(gameObject, 0.3f);
        IsTargetHit = false;

        // ��������� ����
        damage = playerDamageController.defaultDamageWand;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ��������� ������� �����
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);

            // ������ ������ �����
            ActivateAttackFireEffect(collision.gameObject, IsApplyEffect());

            // ��� ������ ����� (������., ��� ������� �����)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);

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
}
