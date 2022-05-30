using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;

    public Animator boxAnim;
    public Animator startAnim;

    private Queue<string> sentences;

    [Header("Skip")]
    public GameObject skipButton;
    private readonly string SKIP_BUTTON_KEY = "SKIP_BUTTON_KEY";

    private void Start()
    {
        sentences = new Queue<string>();

        // если есть запись, то диалог уже читался, можно скипать
        if (PlayerPrefs.HasKey(SKIP_BUTTON_KEY))
        {
            skipButton.SetActive(true);
        }
        else
        {
            skipButton.SetActive(false);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

        nameText.text = dialog.name;
        sentences.Clear();

        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    public void EndDialog()
    {
        PlayerPrefs.SetInt(SKIP_BUTTON_KEY, 1);
        boxAnim.SetBool("boxOpen", false);
    }

    public void SkipDialog()
    {
        while (sentences.Count != 0)
        {
            sentences.Dequeue();
        }
        EndDialog();
    }
}
