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

    [Header("Animation")]
    public Animator parentAnimatorController;

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
        //animation
        if (parentAnimatorController != null)
            parentAnimatorController.SetBool("isAttack", true);
        yield return new WaitForSeconds(1 / cooldown - 0.1f);
        zoneAttack.SetActive(true);

        // ����
        yield return new WaitForSeconds(0.1f);
        zoneAttack.SetActive(false);
        //animation
        if (parentAnimatorController != null)
            parentAnimatorController.SetBool("isAttack", false);

        // ����� ������ � ���
        isReadyAttack = true;
    }

    public void CallOnAttack()
    {
        zoneAttack.SetActive(true);
    }

    public void CallOffAttack()
    {
        zoneAttack.SetActive(false);
    }
}
