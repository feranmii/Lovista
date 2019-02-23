using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    
    private Queue<string> sentences;

    public Dialogue currentDialogue;
    void Start()
    {
        sentences = new Queue<string>();    
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        animator.SetTrigger("Change");
        nameText.text = dialogue.name;
        sentences.Clear();
        
        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.sentence);
        }

        

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        animator.SetTrigger("Change");

        if (sentences.Count == 0)
        {
            currentDialogue.isFinished = true;
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if(currentDialogue.isFinished && currentDialogue.NextDialogue != null)
            StartDialogue(currentDialogue.NextDialogue);
        else
        {
            Debug.Log("End of Conversation");
        }
        
    }
     
    
}
