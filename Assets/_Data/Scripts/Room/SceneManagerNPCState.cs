using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerNPCState : MonoBehaviour
{
    [Header("Data for calculation")]
    public int floorNumber; // íîìåð ýòàæà
    public int healthGGStart;   // õï ÃÃ íà 1 ýòàæå
    public int healthGGEnd;   // õï çà ýòàæ +
    public List<int> killingTime;   // âðåìÿ óáñéèñâà, i = ñëîæíîñòü

    [Space]
    public List<float> dps;   // âðåìÿ óáñéèñâà, i = ñëîæíîñòü


    // äîñòóïíûå âðàãè
    public enum TypesOfEnemies 
    { 
        none,
        Scarecrow,
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

    #region NPC State
    [Header("Ghost")]
    public int complexityGhost = 1;
    public int dpsDebaffGhost = 0;
    public float healthGhost;
    public float speedMovementGhost;
    public float meleeDistanceAttackGhost;
    public float cooldownMeleeAttackGhost;
    public float damageMeleeAttackGhost;
    [Header("Scarecrow")]
    public int complexityScarecrow = 1;
    public int dpsDebaffScarecrow = 0;
    public float healthScarecrow;
    [Header("Skeleton")]
    public int complexitySkeleton = 1;
    public int dpsDebaffSkeleton = 0;
    public float healthSkeleton;
    public float speedMovementSkeleton;
    public float meleeDistanceAttackSkeleton;
    public float cooldownMeleeAttackSkeleton;
    public float damageMeleeAttackSkeleton;
    [Header("Little spider")]
    public int complexityLittleSpider = 1;
    public int dpsDebaffLittleSpider = 0;
    public float healthLittleSpider;
    public float speedMovementLittleSpider;
    public float meleeDistanceAttackLittleSpider;
    public float cooldownMeleeAttackLittleSpider;
    public float damageMeleeAttackLittleSpider;
    [Header("Spider")]
    public int complexitySpider = 2;
    public int dpsDebaffSpider = 0;
    public float healthSpider;
    public float speedMovementSpider;
    public float meleeDistanceAttackSpider;
    public float cooldownMeleeAttackSpider;
    public float damageMeleeAttackSpider;
    [Header("Red spider")]
    public int complexityRedSpider = 3;
    public int dpsDebaffRedSpider = 0;
    public float healthRedSpider;
    public float speedMovementRedSpider;
    public float meleeDistanceAttackRedSpider;
    public float cooldownMeleeAttackRedSpider;
    public float damageMeleeAttackRedSpider;
    [Header("Blue spider")]
    public int complexityBlueSpider = 3;
    public int dpsDebaffBlueSpider = 0;
    public float healthBlueSpider;
    public float speedMovementBlueSpider;
    public float meleeDistanceAttackBlueSpider;
    public float cooldownMeleeAttackBlueSpider;
    public float damageMeleeAttackBlueSpider;
    [Header("Cobra")]
    public int complexityCobra = 2;
    public int dpsDebaffCobra = 1;
    public float healthCobra;
    public float speedMovementCobra;
    public float meleeDistanceAttackCobra;
    public float rangerDistanceAttackCobra;
    public float cooldownMeleeAttackCobra;
    public float cooldownRangerAttackCobra;
    public float damageMeleeAttackCobra;
    public float damageRangerAttackCobra;
    [Header("Red cobra")]
    public int complexityRedCobra = 3;
    public int dpsDebaffRedCobra = 1;
    public float healthRedCobra;
    public float speedMovementRedCobra;
    public float rangerDistanceAttackRedCobra;
    public float cooldownRangerAttackRedCobra;
    public float damageRangerAttackRedCobra;
    [Header("Nest")]
    public int complexityNest = 2;
    public int dpsDebaffNest = 0;
    public float healthNest;
    [Header("Rat")]
    public int complexityRat = 1;
    public int dpsDebaffRat = 0;
    public float healthRat;
    public float speedMovementRat;
    public float meleeDistanceAttackRat;
    public float cooldownMeleeAttackRat;
    public float damageMeleeAttackRat;
    public float passiveRangeRat;
    [Header("Golem")]
    public int complexityGolem = 3;
    public int dpsDebaffGolem = 3;
    public float healthGolem;
    public float speedMovementGolem;
    public float meleeDistanceAttackGolem;
    public float cooldownMeleeAttackGolem;
    public float damageMeleeAttackGolem;
    public float passiveRangeGolem;

    #endregion

    private void Start()
    {
        dps = new List<float>(killingTime.Count);
        for (int i = 0; i < killingTime.Count; i++)
        {
            dps.Add((healthGGStart + healthGGEnd * (floorNumber - 1)) / (float)killingTime[i]);
        }

        LocalCalculateDamage();
        LocalCalculateHealth();
    }

    private void LocalCalculateDamage()
    {
        damageMeleeAttackGhost = ÑalculateDamage(dps[complexityGhost - 1], dpsDebaffGhost, cooldownMeleeAttackGhost);

        damageMeleeAttackSkeleton = ÑalculateDamage(dps[complexitySkeleton - 1], dpsDebaffSkeleton, cooldownMeleeAttackSkeleton);
        damageMeleeAttackLittleSpider = ÑalculateDamage(dps[complexityLittleSpider - 1], dpsDebaffLittleSpider, cooldownMeleeAttackLittleSpider);
        damageMeleeAttackRat = ÑalculateDamage(dps[complexityRat - 1], dpsDebaffRat, cooldownMeleeAttackRat);

        damageMeleeAttackSpider = ÑalculateDamage(dps[complexitySpider - 1], dpsDebaffSpider, cooldownMeleeAttackSpider);
        damageMeleeAttackCobra = ÑalculateDamage(dps[complexityCobra - 1], dpsDebaffCobra, cooldownMeleeAttackCobra);
        damageRangerAttackCobra = ÑalculateDamage(dps[complexityCobra - 1], dpsDebaffCobra, cooldownRangerAttackCobra, coeffAttackSpeed: 2); ;

        damageMeleeAttackRedSpider = ÑalculateDamage(dps[complexityRedSpider - 1], dpsDebaffRedSpider, cooldownMeleeAttackRedSpider);
        damageMeleeAttackBlueSpider = ÑalculateDamage(dps[complexityBlueSpider - 1], dpsDebaffBlueSpider, cooldownMeleeAttackBlueSpider);
        damageRangerAttackRedCobra = ÑalculateDamage(dps[complexityRedCobra - 1], dpsDebaffRedCobra, cooldownRangerAttackRedCobra, coeffAttackSpeed: 2);
        damageMeleeAttackGolem = ÑalculateDamage(dps[complexityGolem - 1], dpsDebaffGolem, cooldownMeleeAttackGolem);
    }

    private void LocalCalculateHealth()
    {
        healthGhost = CalulateHealth(healthGhost);

        healthSkeleton = CalulateHealth(healthSkeleton);
        healthLittleSpider = CalulateHealth(healthLittleSpider);
        healthRat = CalulateHealth(healthRat);

        healthSpider = CalulateHealth(healthSpider);
        healthCobra = CalulateHealth(healthCobra);

        healthRedSpider = CalulateHealth(healthRedSpider);
        healthBlueSpider = CalulateHealth(healthBlueSpider);
        healthRedCobra = CalulateHealth(healthRedCobra);
        healthGolem = CalulateHealth(healthGolem);
    }


    public float GetHealth(TypesOfEnemies type)
    {
        switch (type)
        {
            case TypesOfEnemies.Ghost:
                return healthGhost;
            case TypesOfEnemies.Scarecrow:
                return healthScarecrow;
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

    private float ÑalculateDamage(float generalDps, float individualDps, float attackSpeed, int coeffGeneralDps = 1, int coeffIndividualDps = 1, int coeffAttackSpeed = 1)
    {
        return Mathf.Round((coeffGeneralDps * generalDps - coeffIndividualDps * individualDps) / (coeffAttackSpeed * attackSpeed));
    }

    private float CalulateHealth(float baseHealth)
    {
        return (baseHealth + (baseHealth * (floorNumber - 1) / 2));
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
