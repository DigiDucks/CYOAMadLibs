using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn;

public class OCStats : MonoBehaviour
{
    public string characterName;

    public enum Archetypes{Leader,Peacemaker,Reformer,Generalist,Questioner,Thinker,Artist,Performer,Giver };

    public Archetypes archetype;

    public int STR, INT, CHA;

    public Sprite portrait;
    [Header("Components")]
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statText;

    public void SetStats(OCStats stats)
    {
        characterName = stats.characterName;
        archetype = stats.archetype;
        STR = stats.STR;
        INT = stats.INT;
        CHA = stats.CHA;
        portrait = stats.portrait;

        image.sprite = portrait;
        nameText.text = characterName;
        statText.text = "S-" + STR.ToString() + " I-" + INT.ToString() + " C-" + CHA.ToString();

    }

    public BattleManager.StatType MainStat()
    {
        int[] stats = new int[3] { STR, INT, CHA };
        int mainStat = 0;
        int mainStatIndex = 0;
        for(int i = 0; i <3; i++)
        {
            if (stats[i] > mainStat)
            {
                mainStat = stats[i];
                mainStatIndex = i;
            }
        }

        switch (mainStatIndex)
        {
            case 0: return BattleManager.StatType.STR;
            case 1: return BattleManager.StatType.INT;
            case 2: return BattleManager.StatType.CHA;
        }

        return BattleManager.StatType.STR;
        
    }

    public int GetStat(BattleManager.StatType statType)
    {
        switch (statType)
        {
            case BattleManager.StatType.STR: return STR;
            case BattleManager.StatType.INT: return INT;
            case BattleManager.StatType.CHA: return CHA;
        }

        return 3;
    }
}
