using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyClick : MonoBehaviour
{
    BattleManager manager;
    OCStats stats;
    private void Start()
    {
        manager = FindObjectOfType<BattleManager>();
        stats = GetComponent<OCStats>();
    }
    public void OnClick()
    {
        manager.ChooseBattler(stats);
    }
}
