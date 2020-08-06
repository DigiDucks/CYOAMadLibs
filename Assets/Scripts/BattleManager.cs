using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BattleManager : MonoBehaviour
{
    #region Variables
    public enum StatType { STR,INT,CHA};

    public bool battling = false;

    public OCStats currentBattler;
    public OCStats currentEnemy;

    Dictionary<string,OCStats> allOCS;

    [Header("GameObjects in the Scene")]
    public Transform battlePosition;
    public Transform dialoguePosition;
    public Image ocImage;
    [SerializeField] Sprite blank;

    [Header("Battle Stats")]
    public StatType playerAttackType;
    public int playerStatStrength;

    public StatType enemyAttackType;
    public int enemyStatStrength;

    public StatType previousAttack;

    public int playerPoints = 0, enemyPoints = 0;
    int wonRound = 0;
    bool endBattle = false;
    bool chooseBattler = false;

    InMemoryVariableStorage variableStorage;
    ClickManager clickManager;

    static float[,] artist = new float[3,3]{ { 0.1f, 0.4f, 0.5f }, { 0.3f, 0.1f, 0.6f }, { 0.1f, 0.3f, 0.6f } };
    static float[,] generalist = new float[3, 3] { { 0.3f, 0.3f, 0.4f }, { 0.4f, 0.3f, 0.3f }, { 0.3f, 0.4f, 0.3f } };
    static float[,] giver = new float[3, 3] { { 0.1f, 0.1f, 0.8f }, { 0.4f, 0.2f, 0.4f }, { 0.2f, 0.3f, 0.5f } };
    static float[,] leader = new float[3, 3] { { 0.5f, 0.2f, 0.3f }, { 0.5f, 0.1f, 0.4f }, { 0.4f, 0.1f, 0.5f } };
    static float[,] peacemaker = new float[3, 3] { { 0.5f, 0.3f, 0.2f }, { 0.6f, 0.3f, 0.1f }, { 0.3f, 0.3f, 0.4f } };
    static float[,] performer = new float[3, 3] { { 0.2f, 0.3f, 0.5f }, { 0.6f, 0.1f, 0.3f }, { 0.1f, 0.1f, 0.8f } };
    static float[,] questioner = new float[3, 3] { { 0.3f, 0.2f, 0.5f }, { 0.5f, 0.3f, 0.2f }, { 0.2f, 0.5f, 0.3f } };
    static float[,] reformer = new float[3, 3] { { 0.5f, 0.2f, 0.3f }, { 0.8f, 0.1f, 0.1f }, { 0.6f, 0.1f, 0.3f } };
    static float[,] thinker = new float[3, 3] { { 0.1f, 0.2f, 0.7f }, { 0.7f, 0.2f, 0.1f }, { 0.2f, 0.7f, 0.1f } };
    #endregion

    private void Start()
    {
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
        clickManager = FindObjectOfType<ClickManager>();
        allOCS = FindObjectOfType<OCList>().allOCs;
    }

    public void ChooseBattler(OCStats stats)
    {
        if (chooseBattler)
        {
            currentBattler = stats;
            previousAttack = currentBattler.MainStat();
            variableStorage.SetValue("$playerName", currentBattler.characterName);
            clickManager.canBePaused = true;
            clickManager.Progress();
            Debug.Log("Player set to " +currentBattler.characterName);
        }
    }

    [YarnCommand("setEnemy")]
    public void SetEnemy(string enemyName)
    {
        currentEnemy = allOCS[enemyName];
        variableStorage.SetValue("$enemyName", enemyName);
        ocImage.sprite = currentEnemy.portrait;
        ocImage.transform.position = battlePosition.position; 
        Debug.Log("Enemy set to " + currentEnemy.characterName);
    }

    [YarnCommand("chooseBattler")]
    public void ChooseBattler(string boolean)
    {
        if (boolean == "true")
        {
            chooseBattler = true;
        }
        else
        {
            chooseBattler = false;
        }
    }


    [YarnCommand("declareAttack")]
    public void DeclareAttack(string type)
    {
        switch (type)
        {
            case "STR": playerAttackType = StatType.STR;
                playerStatStrength = currentBattler.STR;
                Debug.Log("Chose Strength");
                break;
            case "INT":
                playerAttackType = StatType.INT;
                playerStatStrength = currentBattler.INT;
                break;
            case "CHA":
                playerAttackType = StatType.CHA;
                playerStatStrength = currentBattler.CHA;
                break;
        }
        Debug.Log(playerAttackType.ToString() + " " + playerStatStrength.ToString());    
        EnemyAI();
        Result();
    }

    [YarnCommand("nextRoom")]
    public void NextRoom(string room)
    {
        variableStorage.SetValue("$nextRoom", room);
        Debug.Log("Next room set to " + room);
    }

    [YarnCommand("restartBattle")]
    public void RestartBattle()
    {
        enemyPoints = 0;
        endBattle = false;
    }

    [YarnCommand("clearEnemy")]
    public void ClearEnemy()
    {
        ocImage.sprite = blank;
        if (currentEnemy.addToPartyOnDeath)
        {
            FindObjectOfType<PartyManager>().AddToParty(currentEnemy.characterName);
        }
        playerPoints = 0;
    }

    void EnemyAI()
    {
        switch (currentEnemy.archetype)
        {
            case OCStats.Archetypes.Artist:
                AIBrain(artist);
                break;
            case OCStats.Archetypes.Generalist:
                AIBrain(generalist);
                break;
            case OCStats.Archetypes.Giver:
                AIBrain(giver);
                break;
            case OCStats.Archetypes.Leader:
                AIBrain(leader);
                break;
            case OCStats.Archetypes.Peacemaker:
                AIBrain(peacemaker);
                break;
            case OCStats.Archetypes.Performer:
                AIBrain(performer);
                break;
            case OCStats.Archetypes.Questioner:
                AIBrain(questioner);
                break;
            case OCStats.Archetypes.Reformer:
                AIBrain(reformer);
                break;
            case OCStats.Archetypes.Thinker:
                AIBrain(thinker);
                break;
        }
    }

    void AIBrain(float[,] algorithm)
    {
        int index =0;
        switch (previousAttack)
        {
            case StatType.STR: index = 0;
                break;
            case StatType.INT: index = 1;
                break;
            case StatType.CHA: index = 2;
                break;
        }

        bool rolling = false;
        int i = 0;
        while (!rolling)
        {
            if (Random.Range(1, 10) > (algorithm[index, i] * 10) && i <2)
            {
                i++;
            }
            else
            {
                switch (index)
                {
                    case 0: enemyAttackType = StatType.STR;
                        break;
                    case 1: enemyAttackType = StatType.INT;
                        break;
                    case 2: enemyAttackType = StatType.CHA;
                        break;

                }
                enemyStatStrength = currentEnemy.GetStat(enemyAttackType);
                rolling = true;
            }
        }
    }
    void SendToStorage()
    {
        variableStorage.SetValue("$enemyAttack",enemyStatStrength);
        variableStorage.SetValue("$playerAttack", playerStatStrength);
        variableStorage.SetValue("$wonRound", wonRound);
        variableStorage.SetValue("$endBattle", endBattle);

    }

    #region Battle Calculations
    void TypeCheck()
    {
        if (enemyAttackType == StatType.STR && playerAttackType == StatType.CHA)
        {
            playerStatStrength *= 2;
        }
        if (enemyAttackType == StatType.INT && playerAttackType == StatType.STR)
        {
            playerStatStrength *= 2;
        }
        if (enemyAttackType == StatType.CHA && playerAttackType == StatType.INT)
        {
            playerStatStrength *= 2;
        }
        if (enemyAttackType == StatType.CHA && playerAttackType == StatType.STR)
        {
            enemyStatStrength *= 2;
        }
        if (enemyAttackType == StatType.STR && playerAttackType == StatType.INT)
        {
            enemyStatStrength *= 2;
        }
        if (enemyAttackType == StatType.INT && playerAttackType == StatType.CHA)
        {
            enemyStatStrength *= 2;
        }
    }
    void Contest()
    {
        if (enemyStatStrength > playerStatStrength)
        {
            enemyPoints++;
            wonRound = 1;
        }
        else if (enemyStatStrength == playerStatStrength)
        {
            wonRound = 2;
        }
        else
        {
            playerPoints++;
            wonRound = 0;

        }

        if (playerPoints > 1)
        {
            //win
            endBattle = true;
        }
        else
        {
            battling = true;
        }
        if (enemyPoints > 1)
        {
            //knockout player
            endBattle = true;
        }
        else
        {
            battling = true;
        }
    }

    void Result()
    {
        TypeCheck();
        Contest();
        previousAttack = playerAttackType;
        SendToStorage();
    }
    #endregion

}
