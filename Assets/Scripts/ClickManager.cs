using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClickManager : MonoBehaviour
{
    DialogueUI dialogueUI;

    private void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height/4)
        {
            dialogueUI.MarkLineComplete();
        }
    }
}
