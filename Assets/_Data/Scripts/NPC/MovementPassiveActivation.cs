using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPassiveActivation : MonoBehaviour
{
    public MovementTowardsGoal source;  // скрипт самого движения
    public float radiusActivation;      // радиус активации движения

    private CircleCollider2D cc2d;

    private void Start()
    {
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
