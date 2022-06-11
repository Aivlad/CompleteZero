using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleRangedAttackSpawn : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public GameObject projectilePrefab;
    public Transform transformPlayer;
    [Space]
    private float cooldown;
    public bool isReadyAttack;
    [Space]
    private float damage;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        cooldown = sceneManager.GetCooldownRanger(type);
        damage = sceneManager.GetDamageRanger(type);


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
        // ����� ����, ������ �����
        isReadyAttack = false;

        //���� �����
        var newObj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newObj.GetComponent<FlightOfConventionalProjectile>().damage = damage;

        // ����
        yield return new WaitForSeconds(1 / cooldown);

        // ����� ������ � ���
        isReadyAttack = true;
    }
}
