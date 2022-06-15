using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUIElementsStubsController : MonoBehaviour
{
    public GameObject EdgingEnvironment;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EdgingEnvironment.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EdgingEnvironment.SetActive(false);
        }
    }
}
