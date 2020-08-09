using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class RandomDuckGenerator : MonoBehaviour
{
    [SerializeField]
    List<Sprite> ducks = new List<Sprite>();

   public Image _rend;
    OCStats stats;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<OCStats>();
    }
    
    [YarnCommand("duckGenerate")]
    public void GenerateDuck()
    {
        Sprite d = ducks[Random.Range(0, ducks.Count)];
        _rend.sprite = d;
        stats.portrait = d; 
    }

    [YarnCommand("addDuck")]
    public void AddDuck()
    {
        FindObjectOfType<PartyManager>().AddToParty(stats);
    }
}
