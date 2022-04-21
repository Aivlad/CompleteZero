using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicDash : MonoBehaviour
{
    public KeyCode action = KeyCode.E;

    [Space]
    public float dashRange;
    private PlayerMovement pm;
    private Vector2 targetPos;

    [Space]
    public bool isReadyAttack;  // готовность атаки
    public float cooldown;      // откат

    private void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        isReadyAttack = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(action) && pm != null && isReadyAttack)
        {
            isReadyAttack = false;
            PlayerMovement.Facing facing = pm.GetFacing();
            targetPos = Vector2.zero;
            switch (facing)
            {
                case PlayerMovement.Facing.UP:
                    targetPos.y = 1;
                    break;
                case PlayerMovement.Facing.DOWN:
                    targetPos.y = -1;
                    break;
                case PlayerMovement.Facing.LEFT:
                    targetPos.x = -1;
                    break;
                case PlayerMovement.Facing.RIGHT:
                    targetPos.x = 1;
                    break;
            }
            transform.Translate(targetPos * dashRange);

            StartCoroutine(Recharge());
        }
    }

    // перезарядка механики
    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

}
