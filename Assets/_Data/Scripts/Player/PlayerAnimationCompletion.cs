using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCompletion : MonoBehaviour
{
    [Header("Animation")]
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void OffWandAttack()
    {
        //animation
        if (playerAnimator != null)
            playerAnimator.SetBool("isWandAttack", false);
    }

    public void OffKnivesAttack()
    {
        //animation
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isLeftKnifeAttack", false);
            playerAnimator.SetBool("isRightKnifeAttack", false);
        }
    }

    public void OffSwordAttack()
    {
        //animation
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRightSwordAttack", false);
            playerAnimator.SetBool("isLeftSwordAttack", false);
        }
    }

    public void OffMorgenshternAttack()
    {
        //animation
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRightMorgenshternAttack", false);
            playerAnimator.SetBool("isLeftMorgenshternAttack", false);
        }
    }
}
