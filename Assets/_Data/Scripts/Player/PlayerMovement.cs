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
    private SaveDataToPlainTextFile balanceManager;
    private int actionsPerMinute;
    private float currentTime;
    private float sendTimeSecond = 60;


    private void Start()
    {
        //balance
        var balanceManagerSource = GameObject.FindGameObjectWithTag("BalanceManager");
        if (balanceManagerSource != null)
        {
            balanceManager = balanceManagerSource.GetComponent<SaveDataToPlainTextFile>();
            actionsPerMinute = 0;
        }
        else
        {
            //Debug.LogWarning("Balance manager = null");
        }
    }

    private void Update()
    {
        TakeInput();
        Move();

        //balance
        TakeInputBalance();
        if (balanceManager != null)
        {
            if (currentTime >= sendTimeSecond)
            {
                balanceManager.OtherSaveText($"ActionsPerMinute (approximately): \t{actionsPerMinute}");
                actionsPerMinute = 0;
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // animation #stub
        // ...
    }

    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(upwardMovement))
        {
            direction += Vector2.up;
            facingDir = Facing.UP;
        }
        if (Input.GetKey(leftwardMovement))
        {
            direction += Vector2.left;
            facingDir = Facing.LEFT;
        }
        if (Input.GetKey(rightwardMovement))
        {
            direction += Vector2.right;
            facingDir = Facing.RIGHT;
        }
        if (Input.GetKey(downwardMovement))
        {
            direction += Vector2.down;
            facingDir = Facing.DOWN;
        }
    }

    //balance
    private void TakeInputBalance()
    {
        if (Input.GetKeyDown(upwardMovement))
        {
            actionsPerMinute++;
        }
        if (Input.GetKeyDown(leftwardMovement))
        {
            actionsPerMinute++;
        }
        if (Input.GetKeyDown(rightwardMovement))
        {
            actionsPerMinute++;
        }
        if (Input.GetKeyDown(downwardMovement))
        {
            actionsPerMinute++;
        }
    }

    public Facing GetFacing()
    {
        return facingDir;
    }
}
