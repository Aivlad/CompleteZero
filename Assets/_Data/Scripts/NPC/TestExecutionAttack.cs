using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExecutionAttack : MonoBehaviour
{
    public GameObject targetAttack;
    private MovementTowardsGoal mtg;
    private MechanicScatterAroundWave msaw;
    public bool isReadyAttack;
    [Range(0, 100)]
    public float chanceWave = 50;



    private void Start()
    {
        mtg = GetComponent<MovementTowardsGoal>();
        msaw = GetComponent<MechanicScatterAroundWave>();
        isReadyAttack = false;
    }

    private void Update()
    {
        if (!mtg.isMovement && isReadyAttack)
        {
            isReadyAttack = false;
            if (IsApplyEffect())
            {
                LaunchingAction();
            }
            else
            {
                //transform.LookAt(new Vector3(targetAttack.transform.position.x, targetAttack.transform.position.y, targetAttack.transform.position.z));
                Look();
            }
        }
        if (mtg.isMovement)
        {
            isReadyAttack = true;
        }
        
    }

    public void LaunchingAction()
    {
        msaw.LaunchingAction();
        Debug.Log("Волновая атака");
    }

    public void Look()
    {
        transform.LookAt2D(targetAttack);
        Debug.Log("Я смотрю на тебя");
    }

    private bool IsApplyEffect()
    {
        return Random.Range(0, 100) <= chanceWave;
    }

}

static class Extensions
{
    #region LookAt2D
    public static void LookAt2D(this Transform me, Vector3 target, Vector3? eye = null)
    {
        float signedAngle = Vector2.SignedAngle(eye ?? me.up, target - me.position);

        if (Mathf.Abs(signedAngle) >= 1e-3f)
        {
            var angles = me.eulerAngles;
            angles.z += signedAngle;
            me.eulerAngles = angles;
        }
    }
    public static void LookAt2D(this Transform me, Transform target, Vector3? eye = null)
    {
        me.LookAt2D(target.position, eye);
    }
    public static void LookAt2D(this Transform me, GameObject target, Vector3? eye = null)
    {
        me.LookAt2D(target.transform.position, eye);
    }
    #endregion
}
