﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PartyManager : MonoBehaviour
{
    public Transform spawnArea;

    OCList ocList;
    InMemoryVariableStorage variableMemory;

    public OCStats mainCharacter;

    public int numberDead = 0;

    public GameObject mainMenu;

    Dictionary<string,OCStats> allOCs;
    Dictionary<string, GameObject> inParty = new Dictionary<string, GameObject>();

    private void Start()
    {
        ocList = FindObjectOfType<OCList>();
        allOCs = ocList.allOCs;
       variableMemory= FindObjectOfType<InMemoryVariableStorage>();
    }

    public void SetMainCharacter(string character)
    {
        mainCharacter = allOCs[character];
        AddToParty(mainCharacter.characterName);
        if (mainCharacter.characterName == "Hansel")
        {
            AddToParty("Gretel");
        }
        else
        {
            AddToParty("Hansel");
        }
        FindObjectOfType<DialogueRunner>().StartDialogue("mainHall");
    }

    [YarnCommand("statCheck")]
    public void StatCheck(string stat)
    {
        string value = stat.Substring(3, 1);
        int toCheck = Convert.ToInt32(value);
        int charStat = 0;
  
        switch (stat.Substring(0,3))
        {
            case "STR": charStat = mainCharacter.STR;
                break;
            case "INT": charStat = mainCharacter.INT;
                break;
            case "CHA": charStat = mainCharacter.CHA;
                break;
            default: Debug.LogError("Stat checking string pass didn't work");
                break;
        }

        if (charStat >= toCheck)
        {
            Debug.Log("parsed int" + toCheck);
            variableMemory.SetValue("$passedCheck", true);
        }
        else
        {
            variableMemory.SetValue("$passedCheck",false);
        }

    }

    [YarnCommand("knockOut")]
    public void KnockOut(string character)
    {
        Image i = inParty[character].GetComponent<Image>();
        inParty[character].GetComponent<Image>().color = new Color(150, 50, 50);
        inParty[character].GetComponent<Button>().enabled = false;
        numberDead++;
        if(numberDead >= inParty.Count-1)
        {
            FindObjectOfType<ClickManager>().SetNextNode("GameOver");
        }
    }

    [YarnCommand("revive")]
    public void Revive()
    {
        foreach(KeyValuePair<string, GameObject> obj in inParty)
        {
            obj.Value.GetComponent<Image>().color = Color.black;
            obj.Value.GetComponent<Button>().enabled = true;
        }
        numberDead = 0;

        
    }

    [YarnCommand("addTo")]
    public void AddToParty(string stats)
    {
        if (!inParty.ContainsKey(stats))
        {
            Debug.Log("added " + stats + " to party");
            GameObject objToSpawn = Instantiate(Resources.Load<GameObject>("OCParty"), spawnArea);
            inParty.Add(stats, objToSpawn);
            objToSpawn.GetComponent<OCStats>().SetStats(allOCs[stats]);
        }
    }

    public void AddToParty(OCStats stats)
    {
        string charName = stats.characterName;
        if (!inParty.ContainsKey(charName))
        {
            Debug.Log("added " + stats + " to party");
            GameObject objToSpawn = Instantiate(Resources.Load<GameObject>("OCParty"), spawnArea);
            inParty.Add(charName, objToSpawn);
            objToSpawn.GetComponent<OCStats>().SetStats(stats);
        }
    }

    public void Leave(string name)
    {
        if (inParty.ContainsKey(name))
        {
            inParty[name].SetActive(false);
            inParty.Remove(name);
        }
    }

    public void MenuStart()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue("Opening");
        mainMenu.SetActive(false);
    }
}
