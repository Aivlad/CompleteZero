using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleRangedAttackSpawn : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform transformPlayer;
    [Space]
    public float cooldown;
    public bool isReadyAttack;

    private void Start()
    {
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(GetReadyForAttack());
    }

    private void Update()
    {
        if (isReadyAttack)
        {
            StartCoroutine(ActionAttack());
        }
    }

    private IEnumerator GetReadyForAttack()
    {
        yield return new WaitForSeconds(2f);
        isReadyAttack = true;
    }

    private IEnumerator ActionAttack()
    {
        // атака была, делаем отдых
        isReadyAttack = false;

        //сама атака
        Instantiate(projectilePrefab, transform.position, transform.rotation);

        // ждем
        yield return new WaitForSeconds(cooldown);

        // снова готовы к бою
        isReadyAttack = true;
    }
}
