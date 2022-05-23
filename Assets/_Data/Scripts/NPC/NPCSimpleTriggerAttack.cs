using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleTriggerAttack : MonoBehaviour
{
    public GameObject zoneAttack;
    public float cooldown;
    public bool isReadyAttack;

    private void Start()
    {
        zoneAttack.SetActive(false);
        isReadyAttack = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isReadyAttack && collision.CompareTag("Player"))
        {
            StartCoroutine(ActionAttack());
        }
    }


    private IEnumerator ActionAttack()
    {
        // ����� ����, ������ �����
        isReadyAttack = false;
        zoneAttack.SetActive(true);

        // ����
        yield return new WaitForSeconds(1f);
        zoneAttack.SetActive(false);
        yield return new WaitForSeconds(cooldown - 1);

        // ����� ������ � ���
        isReadyAttack = true;
    }
    
}
