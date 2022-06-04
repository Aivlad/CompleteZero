using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShakeGolem : MonoBehaviour
{
    private MovementTowardsGoal movement;
    public ShakeCameraEffect shake;
    public float cooldown = 3f;
    private float currentTime;
    [Space]
    public float damage;

    private void Start()
    {
        movement = GetComponent<MovementTowardsGoal>();
        currentTime = 0;
    }

    private void Update()
    {
        if (movement.isMovement && currentTime >= cooldown)
        {
            Action();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void Action()
    {
        shake.Shake();
        List<VitalCharacteristics> targets = new List<VitalCharacteristics>();
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            targets.Add(player.GetComponent<PlayerCharacteristics>());
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            targets.Add(enemy.GetComponent<ObjectCharacteristics>());
        }

        foreach (var target in targets)
        {
            if (target.isShakeDamage)
            {
                target.DealDamage(damage);
            }
        }
    }
}
