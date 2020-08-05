using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public Transform spawnArea;

    OCList ocList;

    Dictionary<string,OCStats> allOCs;

    private void Start()
    {
        ocList = FindObjectOfType<OCList>();
        allOCs = ocList.allOCs;
        AddToParty(allOCs["Dragon"]);
        AddToParty(allOCs["Mimic"]);
    }

    public void AddToParty(OCStats stats)
    {
        Debug.Log("added " + stats.characterName + " to party");
        GameObject objToSpawn = Instantiate(Resources.Load<GameObject>("OCParty"), spawnArea);
        objToSpawn.GetComponent<OCStats>().SetStats(stats);
    }
}
