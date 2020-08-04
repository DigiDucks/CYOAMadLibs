using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public Transform spawnArea;

    public OCStats toAdd;

    private void Start()
    {
        int i= 0;
        while( i < 5)
        {
            AddToParty(toAdd);
            i++;
        }

    }

    public void AddToParty(OCStats stats)
    {
        GameObject objToSpawn = Instantiate(Resources.Load<GameObject>("OCParty"), spawnArea);
        objToSpawn.GetComponent<OCStats>().SetStats(stats);
    }
}
