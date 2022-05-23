using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedThrowingKnivesDamage : MonoBehaviour
{
    public float damage;
    public float destroyDistance;
    [Space]
    public int countTargetsHit;
    public int countTargetsHitMax;

    private Vector3 spawnPosition;

    [Space]
    [Range(0, 100)]
    public float chanceCausePoisoning;


    private void Start()
    {
        spawnPosition = transform.position;
        countTargetsHit = 0;
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

            ActivateAttackEffect(collision.gameObject);

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


    private void ActivateAttackEffect(GameObject target)
    {
        // вызов отравления 
        EffectActivator effectActivator = target.GetComponent<EffectActivator>();
        if (effectActivator != null)
        {
            bool isApplyEffect = IsApplyEffect();
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

    private bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceCausePoisoning;
    }
}
