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
            transform.Translate(targetPos * LimitJumpingInRoom(facing));

            StartCoroutine(Recharge());
        }
    }

    // ограничение прыжка: прыжек только в комнате (не выходим за стену)
    private float LimitJumpingInRoom(PlayerMovement.Facing facing)
    {
        RaycastHit2D[] hits;
        switch (facing)
        {
            case PlayerMovement.Facing.UP:
                hits = Physics2D.RaycastAll(transform.position, Vector2.up, dashRange);
                break;
            case PlayerMovement.Facing.DOWN:
                hits = Physics2D.RaycastAll(transform.position, Vector2.down, dashRange);
                break;
            case PlayerMovement.Facing.LEFT:
                hits = Physics2D.RaycastAll(transform.position, Vector2.left, dashRange);
                break;
            default:
                hits = Physics2D.RaycastAll(transform.position, Vector2.right, dashRange);
                break;
        }
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag("Wall"))
            {
                return Vector2.Distance(transform.position, hits[i].transform.position) - 0.5f;
            }
        }
        return dashRange;
    }

    // перезарядка механики
    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

}
