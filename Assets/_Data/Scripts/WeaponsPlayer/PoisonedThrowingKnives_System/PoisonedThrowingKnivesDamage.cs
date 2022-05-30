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

        // назначаем урон
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
            // нанесение чистого урона
            collision.GetComponent<ObjectCharacteristics>().DealDamage(damage);
            //balance
            playerDamageController.AddOutingDamage(damage);

            // родной эффект атаки
            ActivateAttackPoisoningEffect(collision.gameObject, IsApplyEffect());

            // доп эффект атаки (наприм., при наличии итема)
            CheckingAdditionalFlags();
            CallAdditionalEffect(collision.gameObject);

            // уничтожение ножа после пролетачерез трех врагов
            countTargetsHit++;
            if (countTargetsHit >= countTargetsHitMax)
            {
                Destroy(gameObject);
            }
        }
        if (collision.transform.CompareTag("Wall"))
        {
            //Debug.Log("Стена колайдер");
            Destroy(gameObject);
        }
    }
}
