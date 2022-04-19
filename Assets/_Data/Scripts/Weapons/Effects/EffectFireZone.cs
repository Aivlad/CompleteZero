using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFireZone : MonoBehaviour
{
    [Space]
    public float damage;
    public float damageDelay;
    public float duration;

    [Space]
    public List<ObjectCharacteristics> targets;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectCharacteristics target = collision.GetComponent<ObjectCharacteristics>();
        if (target != null && !targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ObjectCharacteristics target = collision.GetComponent<ObjectCharacteristics>();
        if (target != null)
        {
            targets.Remove(target);
        }
    }

    private void OnEnable()
    {
        Destroy(gameObject, duration);

        // запускаем процесс нанесения урона
        StartCoroutine(ToDamage());
    }

    private IEnumerator ToDamage()
    {
        yield return new WaitForSeconds(damageDelay);

        foreach (ObjectCharacteristics target in targets)
        {
            target?.DealDamage(damage);
        }

        StartCoroutine(ToDamage());
    }


}
