using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // основные характеристики
    public float speed;
    private Vector2 direction;
    
    // клавиши управления
    [Space]
    public KeyCode upwardMovement = KeyCode.W;
    public KeyCode downwardMovement = KeyCode.S;
    public KeyCode leftwardMovement = KeyCode.A;
    public KeyCode rightwardMovement = KeyCode.D;

    // взгляд движения
    public enum Facing { UP, DOWN, LEFT, RIGHT};
    private Facing facingDir = Facing.DOWN;

    [Header("Balance data")]
    private int actionsPerRoom;

    [Header("Levitation over the pits")]
    public bool isLevitationOverPits = false;

    [Header("Animation")]
    public Animator playerAnimator;


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();

        //balance
        actionsPerRoom = 0;
    }

    private void Update()
    {
        TakeInput();
        Move();

        //balance
        TakeInputBalance();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        bool isRun = false;
        if (Input.GetKey(upwardMovement))
        {
            direction += Vector2.up;
            facingDir = Facing.UP;

            isRun = true;
        }
        if (Input.GetKey(leftwardMovement))
        {
            direction += Vector2.left;
            facingDir = Facing.LEFT;

            isRun = true;
        }
        if (Input.GetKey(rightwardMovement))
        {
            direction += Vector2.right;
            facingDir = Facing.RIGHT;

            isRun = true;
        }
        if (Input.GetKey(downwardMovement))
        {
            direction += Vector2.down;
            facingDir = Facing.DOWN;

            isRun = true;
        }

        if (playerAnimator != null)
            playerAnimator.SetBool("isRun", isRun);
    }

    //balance
    private void TakeInputBalance()
    {
        if (Input.GetKeyDown(upwardMovement))
        {
            actionsPerRoom++;
        }
        if (Input.GetKeyDown(leftwardMovement))
        {
            actionsPerRoom++;
        }
        if (Input.GetKeyDown(rightwardMovement))
        {
            actionsPerRoom++;
        }
        if (Input.GetKeyDown(downwardMovement))
        {
            actionsPerRoom++;
        }
    }

    //balance
    public int GetActionsPerRoomAndZeroing()
    {
        var ret = actionsPerRoom;
        actionsPerRoom = 0;
        return ret;
    }

    public Facing GetFacing()
    {
        return facingDir;
    }

    /// <summary>
    /// Увеличить скорость перемещения
    /// </summary>
    /// <param name="percentageIncreaseValue">на сколько процентов увеличить</param>
    public void IncreaseMovementSpeed(float percentageIncreaseValue)
    {
        speed += (speed * percentageIncreaseValue) / 100;
    }
}
