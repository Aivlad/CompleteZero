using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaysWords : MonoBehaviour
{
    public GameObject cloud;
    public TextMesh text;
    public string speech;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            text.text = speech;
            cloud.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {         
            cloud.SetActive(false);
        }
    }

}
