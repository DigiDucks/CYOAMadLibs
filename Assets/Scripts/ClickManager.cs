using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClickManager : MonoBehaviour
{
    DialogueUI dialogueUI;
    [SerializeField]
    bool paused = false;
    public bool canBePaused = true;
    public GameObject canProgressIndicator;

    private void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    private void Update()
    {
        if (!paused)
        {
            if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height / 4)
            {
                Progress();
            }
        }
    }

    public void Progress()
    {
        dialogueUI.MarkLineComplete();
    }

    [YarnCommand("pause")]
    public void TempPause()
    {
        PauseToggle("true");
        canBePaused = false;

    }


    public void PauseToggle(string isPaused)
    {
        if (canBePaused)
        {
            if (isPaused == "true")
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
            canProgressIndicator.SetActive(!paused);
        }
    }
}
