using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : VitalCharacteristics
{
    [Space]
    public GameObject flyingDamagePrefab;
    public Vector3 offset;

    private Rigidbody2D r2d;

    [Header("Balance data")]
    private SaveDataToPlainTextFile balanceManager;
    private float totalDamage;
    private int totalStrokes;

    [Header("health")]
    public PlayerUILifeController playerUILifeController;
    public SceneManagerNPCState sceneManagerNPCState;

    [Header("Damage evasion")]
    public float dodgeСhance = 0f;

    [Header("PlayerEnemiesSpeedIncreaseSpecification")]
    public bool isDamage = false;
    public int CountRoomNoDamage = 0;
    private PlayerEnemiesSpeedIncreaseSpecification playerEnemiesSpeedIncreaseSpecification;
    private PlayerMovement playerMovement;
    private RoomSpawnEnemies currentRoomsSpawnEnemies;


    private void Start()
    {
        
        // UI Change Cell Life
        //fact hp
        var sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneManager != null)
        {
            sceneManagerNPCState = sceneManager.GetComponent<SceneManagerNPCState>();
            if (sceneManagerNPCState != null)
            {
                healthMax = sceneManagerNPCState.healthGGStart + (sceneManagerNPCState.healthGGEnd * (sceneManagerNPCState.floorNumber - 1));
            }
        }
        else
        {
            healthMax = 7;
        }
        health = healthMax;
        //ui
        if (sceneManager != null)
        {
            playerUILifeController = sceneManager.GetComponent<PlayerUILifeController>();
            if (playerUILifeController != null)
            {
                playerUILifeController.ChangeCountCell(this);
            }
        }    
     
        r2d = GetComponent<Rigidbody2D>();

        //balance
        var balanceManagerSource = GameObject.FindGameObjectWithTag("BalanceManager");
        if (balanceManagerSource != null)
        {
            balanceManager = balanceManagerSource.GetComponent<SaveDataToPlainTextFile>();
            totalDamage = 0;
            totalStrokes = 0;
        }
        else
        {
            Debug.LogWarning("Balance manager = null");
        }
    }

    private void Update()
    {
        if (r2d != null)
            r2d.WakeUp();   // заставляем высегда быть активным (для моментов с StayTrigger)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterRoom"))
        {
            if (playerEnemiesSpeedIncreaseSpecification == null)
                playerEnemiesSpeedIncreaseSpecification = GetComponent<PlayerEnemiesSpeedIncreaseSpecification>();

            if (playerMovement == null)
                playerMovement = GetComponent<PlayerMovement>();

            currentRoomsSpawnEnemies = collision.GetComponent<RoomSpawnEnemies>();
            ChangeSpeed();
        }
    }

    private void ChangeSpeed()
    {
        if (!isDamage)
        {
            CountRoomNoDamage++;
            if (CountRoomNoDamage > 2)
            {
                for (int i = 0; i < CountRoomNoDamage - 2; i++)
                {
                    playerEnemiesSpeedIncreaseSpecification.IncreaseSpeed(sceneManagerNPCState, currentRoomsSpawnEnemies, playerMovement, this);
                }
            }
        }
        else
        {
            //CountRoomNoDamage--;
            //if (CountRoomNoDamage > 2)
            //{
            //    for (int i = 0; i < CountRoomNoDamage - 2; i++)
            //    {
            //        playerEnemiesSpeedIncreaseSpecification.IncreaseSpeed(sceneManagerNPCState, currentRoomsSpawnEnemies, playerMovement, this);
            //    }
            //}
            CountRoomNoDamage = 0;
            isDamage = false;
        }
    }


    public override void DealDamage(float damage)
    {
        if (!IsDodgeChance())   // если не уклонились, то получили урон
        {
            isDamage = true;
            ChangeSpeed();
            health -= damage;
            CheckDeath();
            CallFlyingDamage(damage);

            // UI Change Cell Life
            if (playerUILifeController != null)
            {
                playerUILifeController.ChangeCountCell(this);
            }
        }
        else // если уклонились, то урон не прошел
        {
            CallFlyingDamage("evasion");
            damage = 0; // for balance
        }

        //balance
        if (balanceManager != null)
        {
            totalDamage += damage;
            totalStrokes++;
        }
    }

    /// <summary>
    /// Подлечить
    /// </summary>
    /// <param name="value">насколько увеличить текущее здоровье</param>
    public void Treatment(float value)
    {
        health += value;
        if (health > healthMax)
            health = healthMax;

        // UI Change Cell Life
        if (playerUILifeController != null)
        {
            playerUILifeController.ChangeCountCell(this);
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CallFlyingDamage(float damage)
    {
        var inst = Instantiate(flyingDamagePrefab);
        inst.transform.parent = gameObject.transform;
        inst.transform.position = transform.position + offset;
        inst.GetComponent<FlyingDamage>().damage = damage;
    }

    private void CallFlyingDamage(string damageText)
    {
        var inst = Instantiate(flyingDamagePrefab);
        inst.transform.parent = gameObject.transform;
        inst.transform.position = transform.position + offset;
        inst.GetComponent<FlyingDamage>().damage = 0;
        inst.GetComponent<FlyingDamage>().damageText = damageText;
    }

    public void IncreaseMaxHealth(int magnificationAmount)
    {
        healthMax += magnificationAmount;
        if (playerUILifeController != null)
        {
            playerUILifeController.ChangeCountCell(this);
        }
    }

    private bool IsDodgeChance()
    {
        return Random.Range(0, 100) <= dodgeСhance;
    }

    //balance
    public void ResetToatalDamageAndStrokes()
    {
        totalDamage = 0;
        totalStrokes = 0;
    }

    //balance
    public float GetTotalDamage()
    {
        return totalDamage;
    }

    //balance
    public int GetTotalStrokes()
    {
        return totalStrokes;
    }
}
