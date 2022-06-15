using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgenshternAttackSysten : MonoBehaviour
{
    public KeyCode attackButton = KeyCode.Mouse0;
    [Space]
    public GameObject morgenshternSystemSplash;
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
                Invoke("CallAttack", 0.5f);
                
            }
        }
    }

    private void CallAttack()
    {
        //audio
        if (playerSoundtrack != null)
            playerSoundtrack.PlaySound(false);

        //animation
        if (playerAnimator != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPosition = transform.position;

            if (mousePosition.x < myPosition.x)
                playerAnimator.SetBool("isRightMorgenshternAttack", true);
            else
                playerAnimator.SetBool("isLeftMorgenshternAttack", true);
        }

        morgenshternSystemSplash.SetActive(true);
        StartCoroutine(StubAttackVisualization());
        isReadyAttack = false;
        StartCoroutine(Recharge());
    }

        IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

    IEnumerator StubAttackVisualization()
    {
        yield return new WaitForSeconds(0.2f);
        morgenshternSystemSplash.SetActive(false);
    }
}
