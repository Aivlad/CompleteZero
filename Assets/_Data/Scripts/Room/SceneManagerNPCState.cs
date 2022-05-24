using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerNPCState : MonoBehaviour
{
    // доступные враги
    public enum TypesOfEnemies 
    { 
        none, 
        Ghost, 
        Skeleton, 
        LittleSpider, 
        Spider,
        Cobra,
        Nest
    };

    [Header("Ghost")]
    public float healthGhost;
    public float speedMovementGhost;
    public float meleeDistanceAttackGhost;
    public float cooldownMeleeAttackGhost;
    public float damageMeleeAttackGhost;
    [Header("Skeleton")]
    public float healthSkeleton;
    public float speedMovementSkeleton;
    public float meleeDistanceAttackSkeleton;
    public float cooldownMeleeAttackSkeleton;
    public float damageMeleeAttackSkeleton;
    [Header("Little spider")]
    public float healthLittleSpider;
    public float speedMovementLittleSpider;
    public float meleeDistanceAttackLittleSpider;
    public float cooldownMeleeAttackLittleSpider;
    public float damageMeleeAttackLittleSpider;
    [Header("Spider")]
    public float healthSpider;
    public float speedMovementSpider;
    public float meleeDistanceAttackSpider;
    public float cooldownMeleeAttackSpider;
    public float damageMeleeAttackSpider;
    [Header("Cobra")]
    public float healthCobra;
    public float speedMovementCobra;
    public float meleeDistanceAttackCobra;
    public float rangerDistanceAttackCobra;
    public float cooldownMeleeAttackCobra;
    public float cooldownRangerAttackCobra;
    public float damageMeleeAttackCobra;
    public float damageRangerAttackCobra;
    [Header("Nest")]
    public float healthNest;


    public float GetHealth(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return healthGhost;
            case TypesOfEnemies.Skeleton:
                return healthSkeleton;
            case TypesOfEnemies.LittleSpider:
                return healthLittleSpider;
            case TypesOfEnemies.Spider:
                return healthSpider;
            case TypesOfEnemies.Cobra:
                return healthCobra;
            case TypesOfEnemies.Nest:
                return healthNest;
            default:
                return 0f;
        }
    }

    public float GetSpeed(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return speedMovementGhost;
            case TypesOfEnemies.Skeleton:
                return speedMovementSkeleton;
            case TypesOfEnemies.LittleSpider:
                return speedMovementLittleSpider;
            case TypesOfEnemies.Spider:
                return speedMovementSpider;
            case TypesOfEnemies.Cobra:
                return speedMovementCobra;
            default:
                return 0f;
        }
    }

    public float GetMeleeDistance(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return meleeDistanceAttackGhost;
            case TypesOfEnemies.Skeleton:
                return meleeDistanceAttackSkeleton;
            case TypesOfEnemies.LittleSpider:
                return meleeDistanceAttackLittleSpider;
            case TypesOfEnemies.Spider:
                return meleeDistanceAttackSpider;
            case TypesOfEnemies.Cobra:
                return meleeDistanceAttackCobra;
            default:
                return 0f;
        }
    }

    public float GetRangerDistance(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Cobra:
                return rangerDistanceAttackCobra;
            default:
                return 0f;
        }
    }

    public float GetCooldownMelee(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return cooldownMeleeAttackGhost;
            case TypesOfEnemies.Skeleton:
                return cooldownMeleeAttackSkeleton;
            case TypesOfEnemies.LittleSpider:
                return cooldownMeleeAttackLittleSpider;
            case TypesOfEnemies.Spider:
                return cooldownMeleeAttackSpider;
            case TypesOfEnemies.Cobra:
                return cooldownMeleeAttackCobra;
            default:
                return 0f;
        }
    }

    public float GetCooldownRanger(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Cobra:
                return cooldownRangerAttackCobra;
            default:
                return 0f;
        }
    }



    public float GetDamageMelee(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return damageMeleeAttackGhost;
            case TypesOfEnemies.Skeleton:
                return damageMeleeAttackSkeleton;
            case TypesOfEnemies.LittleSpider:
                return damageMeleeAttackLittleSpider;
            case TypesOfEnemies.Spider:
                return damageMeleeAttackSpider;
            case TypesOfEnemies.Cobra:
                return damageMeleeAttackCobra;
            default:
                return 0f;
        }
    }

    public float GetDamageRanger(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Cobra:
                return damageRangerAttackCobra;
            default:
                return 0f;
        }
    }


}
