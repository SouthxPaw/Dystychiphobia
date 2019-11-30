using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Button startButton;
    public Queue<string> sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        sentences = new Queue<string>();
        
    }


    public void StartDialogue(Dialogue dialouge)
    {

        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach(string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }



    public void EndDialouge()
    {
      
        animator.SetBool("isOpen", false);
        Destroy(startButton.gameObject);
        
    }

}
