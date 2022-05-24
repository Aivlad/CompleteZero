using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPassiveActivation : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public MovementTowardsGoal source;  // ������ ������ ��������
    public float radiusActivation;      // ������ ��������� ��������

    private CircleCollider2D cc2d;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        radiusActivation = sceneManager.GetPassiveRange(type);

        cc2d = GetComponent<CircleCollider2D>();
        cc2d.radius = radiusActivation;
        source.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            source.SetTarget(collision.gameObject);
            source.enabled = true;
            cc2d.enabled = false;
        }
    }
}
