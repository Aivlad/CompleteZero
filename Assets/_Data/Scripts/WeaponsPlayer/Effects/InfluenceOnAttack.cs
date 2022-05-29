using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceOnAttack : MonoBehaviour
{
    [Space]
    [Range(0, 100)]
    public float chanceToInfluence = 80;    // шанс вызвать эффект

    [Header("Source data")]
    protected PlayerDamageController playerDamageController;

    [Header("Additional effect on attack")]
    public bool isAdditionBleedingEffect = false;
    public bool isAdditionPoisoningEffect = false;
    public bool isAdditionFireEffect = false;
    [Range(0, 100)]
    public float additionChanceToInfluence = 50;    // шанс вызвать эффект

    /// <summary>
    /// Инициализация данных
    /// </summary>
    protected void DataInitialization()
    {
        playerDamageController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageController>();
    }

    /// <summary>
    /// Проверка у PlayerDamageController значение флагов дополнительных эффектов
    /// </summary>
    protected void CheckingAdditionalFlags()
    {
        isAdditionBleedingEffect = playerDamageController.isAdditionBleedingEffect;
        isAdditionPoisoningEffect = playerDamageController.isAdditionPoisoningEffect;
        isAdditionFireEffect = playerDamageController.isAdditionFireEffect;
    }

    /// <summary>
    /// Шанс срабатывания основного эффекта
    /// </summary>
    protected bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceToInfluence;
    }

    /// <summary>
    /// Шанс срабатывания дополнительного эффекта
    /// </summary>
    protected bool IsAdditionApplyEffect()
    {
        return Random.Range(0, 100) <= additionChanceToInfluence;
    }

    /// <summary>
    /// Попытка активировать дополнительный эффект при атаке
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
    /// Активировать эффект кровотечения
    /// </summary>
    protected void ActivateAttackBleedingEffect(GameObject target, bool isApplyEffect)
    {
        // вызов кровотечения 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {

            if (isApplyEffect && effectActivator.IsEffectBleedingActicated())   // с определенным шансом + если объект уже активирован, то просто обнуляем
            {
                effectActivator.UpdateEffectBleeding();
            }
            else if (isApplyEffect)    // с определенным шансом
            {
                effectActivator.CallEffectBleeding();
            }
        }
    }

    /// <summary>
    /// Активировать эффект отравления
    /// </summary>
    protected void ActivateAttackPoisoningEffect(GameObject target, bool isApplyEffect)
    {
        // вызов отравления 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            if (isApplyEffect && effectActivator.IsEffectPoisoningActicated())   // с определенным шансом + если объект уже активирован, то просто обнуляем
            {
                effectActivator.UpdateEffectPoisoning();
            }
            else if (isApplyEffect)    // с определенным шансом
            {
                effectActivator.CallEffectPoisoning();
            }
        }
    }

    /// <summary>
    /// Активировать эффект огня
    /// </summary>
    protected void ActivateAttackFireEffect(GameObject target, bool isApplyEffect)
    {
        // вызов огня
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            if (isApplyEffect && effectActivator.IsEffectFireActivated())   // с определенным шансом + если объект уже активирован, то просто обнуляем
            {
                effectActivator.UpdateEffectFire();
            }
            else if (isApplyEffect)    // с определенным шансом
            {
                effectActivator.CallEffectFire();
            }
        }
    }
}
