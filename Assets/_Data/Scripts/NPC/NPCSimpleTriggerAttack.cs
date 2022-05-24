using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleTriggerAttack : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public Transform bodyParent;
    [Space]
    public GameObject zoneAttack;
    private float cooldown;
    private float distance;
    public bool isReadyAttack;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        cooldown = sceneManager.GetCooldownMelee(type);
        distance = sceneManager.GetMeleeDistance(type);

        zoneAttack.SetActive(false);
        isReadyAttack = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isReadyAttack && collision.CompareTag("Player"))
        {
            var currentDistance = bodyParent.GetComponent<Collider2D>().Distance(collision).distance;
            if (currentDistance <= distance)
            {
                StartCoroutine(ActionAttack());
                //Debug.Log(currentDistance);
            }
        }
    }


    private IEnumerator ActionAttack()
    {
        // ����� ����, ������ �����
        isReadyAttack = false;
        zoneAttack.SetActive(true);

        // ����
        yield return new WaitForSeconds(0.1f);
        zoneAttack.SetActive(false);
        yield return new WaitForSeconds(cooldown - 0.1f);

        // ����� ������ � ���
        isReadyAttack = true;
    }
    
}
