using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCList : MonoBehaviour
{
    [SerializeField]
    OCStats[] ocArray;
    public Dictionary<string, OCStats> allOCs = new Dictionary<string, OCStats>();

    // Start is called before the first frame update
    void Awake()
    {
        int i = 0;
        foreach(OCStats stats in ocArray)
           {
               allOCs.Add(ocArray[i].characterName, ocArray[i]);
               Debug.Log(allOCs[ocArray[i].characterName]);
               i++;
           }
    }

}
