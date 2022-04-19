using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightIncomingDamage : MonoBehaviour
{
    private GameObject damageSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Splash"))
        {
            damageSource = collision.gameObject;
        }
    }

    private void OnDestroy()
    {
        if (damageSource != null)
        {
            SwordOfLightDamage sold = damageSource.GetComponent<SwordOfLightDamage>();
            if (sold != null)
            {
                sold.CountMurder();
            }
        }
    }
}
