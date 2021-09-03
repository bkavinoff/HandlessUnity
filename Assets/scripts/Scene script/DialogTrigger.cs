using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public static DialogTrigger sharedInstance;
    public Dialog dialog;
    public void TriggerDialogue()
    {
       // FindObjectOfType<DialogManager>().StartDialogue(dialog);
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

}
