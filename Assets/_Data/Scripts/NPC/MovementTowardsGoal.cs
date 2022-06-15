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
    public float speed;
    private float stopDistance;
    public bool isMovement;    // флаг: объект движется или нет

    [Header("Animation")]
    private Animator animatorController;
    private bool isViewLeft = true;

    [Header("Audio")]
    public NPCSimpleSoundtrack soundtrackController;
    private bool isOnSound = false;


    // взгляд движения
    public enum Facing { UP, DOWN, LEFT, RIGHT };
    private Facing facingDir = Facing.DOWN;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        speed = sceneManager.GetSpeed(type);
        stopDistance = sceneManager.GetMeleeDistance(type);
        if (stopDistance == 0)
            stopDistance = sceneManager.GetRangerDistance(type);

        if (target == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
        isMovement = false;

        //animation
        animatorController = GetComponent<Animator>();
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

                //audio
                if (!isOnSound && soundtrackController != null)
                {
                    isOnSound = true;
                    soundtrackController.PlaySound();
                }

            }
            else
            {
                isMovement = false;
                //audio
                if (isOnSound && soundtrackController != null)
                {
                    isOnSound = false;
                    soundtrackController.StopSound();
                }
            }
            DetermineDirectionView();

            //animation
            if (animatorController != null)
                animatorController.SetBool("isRun", isMovement);
        }
    }


    public float GetTargetDistance()
    {
        if (target == null)
            return 0;
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
                if (!isViewLeft)
                    Flip();
            }
            else if (target.position.x > transform.position.x)
            {
                facingDir = Facing.RIGHT;
                if (isViewLeft)
                    Flip();
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

    private void Flip()
    {
        isViewLeft = !isViewLeft;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
