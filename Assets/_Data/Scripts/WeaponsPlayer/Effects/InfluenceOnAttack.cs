using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceOnAttack : MonoBehaviour
{
    [Space]
    [Range(0, 100)]
    public float chanceToInfluence = 80;    // ���� ������� ������

    [Header("Source data")]
    protected PlayerDamageController playerDamageController;

    [Header("Additional effect on attack")]
    public bool isAdditionBleedingEffect = false;
    public bool isAdditionPoisoningEffect = false;
    public bool isAdditionFireEffect = false;
    [Range(0, 100)]
    public float additionChanceToInfluence = 50;    // ���� ������� ������

    /// <summary>
    /// ������������� ������
    /// </summary>
    protected void DataInitialization()
    {
        playerDamageController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageController>();
    }

    /// <summary>
    /// �������� � PlayerDamageController �������� ������ �������������� ��������
    /// </summary>
    protected void CheckingAdditionalFlags()
    {
        isAdditionBleedingEffect = playerDamageController.isAdditionBleedingEffect;
        isAdditionPoisoningEffect = playerDamageController.isAdditionPoisoningEffect;
        isAdditionFireEffect = playerDamageController.isAdditionFireEffect;
    }

    /// <summary>
    /// ���� ������������ ��������� �������
    /// </summary>
    protected bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceToInfluence;
    }

    /// <summary>
    /// ���� ������������ ��������������� �������
    /// </summary>
    protected bool IsAdditionApplyEffect()
    {
        return Random.Range(0, 100) <= additionChanceToInfluence;
    }

    /// <summary>
    /// ������� ������������ �������������� ������ ��� �����
    /// </summary>
    protected void CallAdditionalEffect(GameObject target)
    {
        if (isAdditionBleedingEffect)
            ActivateAttackBleedingEffect(target, IsAdditionApplyEffect());
        if (isAdditionPoisoningEffect)
            ActivateAttackPoisoningEffect(target, IsAdditionApplyEffect());
        if (isAdditionFireEffect)
            ActivateAttackFireEffect(target, IsAdditionApplyEffect());
    }

    /// <summary>
    /// ������������ ������ ������������
    /// </summary>
    protected void ActivateAttackBleedingEffect(GameObject target, bool isApplyEffect)
    {
        // ����� ������������ 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {

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

    /// <summary>
    /// ������������ ������ ����������
    /// </summary>
    protected void ActivateAttackPoisoningEffect(GameObject target, bool isApplyEffect)
    {
        // ����� ���������� 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            if (isApplyEffect && effectActivator.IsEffectPoisoningActicated())   // � ������������ ������ + ���� ������ ��� �����������, �� ������ ��������
            {
                effectActivator.UpdateEffectPoisoning();
            }
            else if (isApplyEffect)    // � ������������ ������
            {
                effectActivator.CallEffectPoisoning();
            }
        }
    }

    /// <summary>
    /// ������������ ������ ����
    /// </summary>
    protected void ActivateAttackFireEffect(GameObject target, bool isApplyEffect)
    {
        // ����� ����
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
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
}
