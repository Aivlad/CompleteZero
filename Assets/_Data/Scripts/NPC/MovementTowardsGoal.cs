using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTowardsGoal : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public Transform target;
    [Space]
    private float speed;
    private float stopDistance;
    public bool isMovement;    // флаг: объект движется или нет

    // взгляд движения
    public enum Facing { UP, DOWN, LEFT, RIGHT };
    private Facing facingDir = Facing.DOWN;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        speed = sceneManager.GetSpeed(type);
        stopDistance = sceneManager.GetMeleeDistance(type);

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        isMovement = false;
    }

    private void Update()
    {
        if (target != null)
        {
            var distanceToTarget = GetTargetDistance();
            if (distanceToTarget > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                isMovement = true;
            }
            else
            {
                isMovement = false;
            }
            DetermineDirectionView();
        }
    }

    public float GetTargetDistance()
    {
        return Vector2.Distance(target.position, transform.position);
    }

    private void DetermineDirectionView()
    {
        if (target != null)
        {
            // больше как #stub
            if (target.position.x < transform.position.x)
            {
                facingDir = Facing.LEFT;
            }
            else if (target.position.x > transform.position.x)
            {
                facingDir = Facing.RIGHT;
            }
            else if (target.position.y < transform.position.y)
            {
                facingDir = Facing.DOWN;
            }
            else
            {
                facingDir = Facing.UP;
            }
        }
    }
    public Facing GetFacing()
    {
        return facingDir;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target.transform;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetStopDistance(float distance)
    {
        stopDistance = distance;
    }

    public void ClearTarget()
    {
        target = null;
    }
}
