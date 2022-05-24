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

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
                targets[i].DealDamage(damage);
        }
        yield return new WaitForSeconds(damageDelay);

        StartCoroutine(ToDamage());
    }


}
