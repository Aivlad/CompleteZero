using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHoleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerMovement>().isLevitationOverPits)
            {
                var characteristics = collision.GetComponent<PlayerCharacteristics>();
                characteristics.DealDamage(characteristics.healthMax);
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            var characteristics = collision.GetComponent<ObjectCharacteristics>();
            characteristics.DealDamage(characteristics.healthMax);
        }
    }
}
