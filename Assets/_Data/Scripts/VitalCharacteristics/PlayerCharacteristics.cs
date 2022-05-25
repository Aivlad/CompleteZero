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

    private void Start()
    {
        health = healthMax;

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
        r2d.WakeUp();   // заставляем высегда быть активным (для моментов с StayTrigger)
    }

    public override void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        CallFlyingDamage(damage);

        //balance
        if (balanceManager != null)
        {
            totalDamage += damage;
            totalStrokes ++;
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
