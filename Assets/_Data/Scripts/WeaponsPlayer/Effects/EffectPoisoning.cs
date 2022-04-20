using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoisoning : MonoBehaviour
{
    public ObjectCharacteristics effectTarget;
    [Space]
    public float damage;
    private float startingDamage;
    public float damageDelay;
    public float duration;

    private int countCallTotal;
    private int countCallCurrent;

    private void OnEnable()
    {
        // ��������� ������� ��� �� ������ ������� ����
        countCallTotal = (int)(duration / damageDelay);
        countCallCurrent = 0;
        // ���������� ���������� �������� ���
        startingDamage = damage;
        // ��������� ������� ��������� �����
        StartCoroutine(ToDamage());
    }

    public void EffectUpdate()
    {
        countCallCurrent = 0;
        // ��� ���������� �� ���������
        damage++;
    }

    private IEnumerator ToDamage()
    {
        yield return new WaitForSeconds(damageDelay);
        countCallCurrent++;
        DamageAction();
        if (countCallCurrent >= countCallTotal)
        {
            damage = startingDamage;    // �������� ����� ��� ���������� �����
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ToDamage());
        }
    }

    private void DamageAction()
    {
        effectTarget.DealDamage(damage);
    }
}
