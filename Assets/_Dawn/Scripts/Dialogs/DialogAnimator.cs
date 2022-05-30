using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    [Header("Animation")]
    public Animator startAnim;
    public DialogManager dm;

    [Header("Speech")]
    public DialogTrigger dialogTrigger;
    public Dialog dialog;

    public void OnTriggerEnter2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", true);
        startAnim.gameObject.GetComponent<DialogTrigger>().SetDialog(dialog);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", false);
        dm.EndDialog();
    }
}
