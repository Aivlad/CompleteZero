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
        RedCobra,
        Nest, 
        RedSpider,
        BlueSpider,
        Rat,
        Golem
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
    [Header("Red spider")]
    public float healthRedSpider;
    public float speedMovementRedSpider;
    public float meleeDistanceAttackRedSpider;
    public float cooldownMeleeAttackRedSpider;
    public float damageMeleeAttackRedSpider;
    [Header("Blue spider")]
    public float healthBlueSpider;
    public float speedMovementBlueSpider;
    public float meleeDistanceAttackBlueSpider;
    public float cooldownMeleeAttackBlueSpider;
    public float damageMeleeAttackBlueSpider;
    [Header("Cobra")]
    public float healthCobra;
    public float speedMovementCobra;
    public float meleeDistanceAttackCobra;
    public float rangerDistanceAttackCobra;
    public float cooldownMeleeAttackCobra;
    public float cooldownRangerAttackCobra;
    public float damageMeleeAttackCobra;
    public float damageRangerAttackCobra;
    [Header("Red cobra")]
    public float healthRedCobra;
    public float speedMovementRedCobra;
    public float rangerDistanceAttackRedCobra;
    public float cooldownRangerAttackRedCobra;
    public float damageRangerAttackRedCobra;
    [Header("Nest")]
    public float healthNest;
    [Header("Rat")]
    public float healthRat;
    public float speedMovementRat;
    public float meleeDistanceAttackRat;
    public float cooldownMeleeAttackRat;
    public float damageMeleeAttackRat;
    public float passiveRangeRat;
    [Header("Golem")]
    public float healthGolem;
    public float speedMovementGolem;
    public float meleeDistanceAttackGolem;
    public float cooldownMeleeAttackGolem;
    public float damageMeleeAttackGolem;
    public float passiveRangeGolem;


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
            case TypesOfEnemies.RedSpider:
                return healthRedSpider;
            case TypesOfEnemies.BlueSpider:
                return healthBlueSpider;
            case TypesOfEnemies.Cobra:
                return healthCobra;
            case TypesOfEnemies.RedCobra:
                return healthRedCobra;
            case TypesOfEnemies.Nest:
                return healthNest;
            case TypesOfEnemies.Rat:
                return healthRat;
            case TypesOfEnemies.Golem:
                return healthGolem;
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
            case TypesOfEnemies.RedSpider:
                return speedMovementRedSpider;
            case TypesOfEnemies.BlueSpider:
                return speedMovementBlueSpider;
            case TypesOfEnemies.Cobra:
                return speedMovementCobra;
            case TypesOfEnemies.RedCobra:
                return speedMovementRedCobra;
            case TypesOfEnemies.Rat:
                return speedMovementRat;
            case TypesOfEnemies.Golem:
                return speedMovementGolem;
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
            case TypesOfEnemies.RedSpider:
                return meleeDistanceAttackRedSpider;
            case TypesOfEnemies.BlueSpider:
                return meleeDistanceAttackBlueSpider;
            case TypesOfEnemies.Cobra:
                return meleeDistanceAttackCobra;
            case TypesOfEnemies.Rat:
                return meleeDistanceAttackRat;
            case TypesOfEnemies.Golem:
                return meleeDistanceAttackGolem;
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
            case TypesOfEnemies.RedCobra:
                return rangerDistanceAttackRedCobra;
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
            case TypesOfEnemies.RedSpider:
                return cooldownMeleeAttackRedSpider;
            case TypesOfEnemies.BlueSpider:
                return cooldownMeleeAttackBlueSpider;
            case TypesOfEnemies.Cobra:
                return cooldownMeleeAttackCobra;
            case TypesOfEnemies.Rat:
                return cooldownMeleeAttackRat;
            case TypesOfEnemies.Golem:
                return cooldownMeleeAttackGolem;
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
            case TypesOfEnemies.RedCobra:
                return cooldownRangerAttackRedCobra;
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
            case TypesOfEnemies.RedSpider:
                return damageMeleeAttackRedSpider;
            case TypesOfEnemies.BlueSpider:
                return damageMeleeAttackBlueSpider;
            case TypesOfEnemies.Cobra:
                return damageMeleeAttackCobra;
            case TypesOfEnemies.Rat:
                return damageMeleeAttackRat;
            case TypesOfEnemies.Golem:
                return damageMeleeAttackGolem;
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
            case TypesOfEnemies.RedCobra:
                return damageRangerAttackRedCobra;
            default:
                return 0f;
        }
    }

    public float GetPassiveRange(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Rat:
                return passiveRangeRat;
            case TypesOfEnemies.Golem:
                return passiveRangeGolem;
            default:
                return 0f;
        }
    }


}
