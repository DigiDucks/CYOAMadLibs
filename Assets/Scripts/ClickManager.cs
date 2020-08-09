using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Yarn.Unity;

public class ClickManager : MonoBehaviour
{
    DialogueUI dialogueUI;
    DialogueRunner dialogueRunner;
    [SerializeField]
    bool paused = false;
    public bool canBePaused = true;
    public bool continuing = false;
    public string nextNode;
    public GameObject canProgressIndicator;
    public GameObject characerSelect;
    public GameObject skipButton;

    private void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
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

    [YarnCommand("nextRoom")]
    public void SetNextNode(string next)
    {
        nextNode = next;
    }

    public void StartDialogue()
    {
        if (continuing)
        {
            StartCoroutine(LoadDialogue());
        }
    }

    public IEnumerator LoadDialogue()
    {
        yield return new WaitForEndOfFrame();

        dialogueRunner.StartDialogue(nextNode);

    }

    [YarnCommand("setCharacterSelect")]
    public void CharacterSelect()
    {
        characerSelect.SetActive(true);
        skipButton.SetActive(false);
        PauseToggle("true");
    }

    [YarnCommand("setContinue")]
    public void SetContinue(string cont)
    {
        if(cont == "true")
        {

        continuing = true;
        }
        else
        {
            continuing = false;
        }
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
