using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedThrowingKnivesAttackSystem : MonoBehaviour
{
    public KeyCode attackButton = KeyCode.Mouse0;
    [Space]
    public GameObject knifePrefab;
    public float flightForce;
    [Space]
    public bool isReadyAttack;
    public float cooldown;

    [Header("Audio")]
    public PlayerSoundtrack playerSoundtrack;

    [Header("Animation")]
    private Animator playerAnimator;

    private void Start()
    {
        isReadyAttack = true;

        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(attackButton))
        {
            if (isReadyAttack)
            {
                //audio
                if (playerSoundtrack != null)
                    playerSoundtrack.PlaySound(false);

                GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPosition = transform.position;
                Vector2 direction = (mousePosition - myPosition).normalized;
                knife.GetComponent<Rigidbody2D>().velocity = direction * flightForce;
                isReadyAttack = false;
                
                //animation
                if (playerAnimator != null)
                {
                    if (mousePosition.x < myPosition.x)
                        playerAnimator.SetBool("isRightKnifeAttack", true);
                    else
                        playerAnimator.SetBool("isLeftKnifeAttack", true);
                }

                StartCoroutine(Recharge());
            }
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

}
