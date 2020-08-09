using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Yarn.Unity;

public class DungeonManager : MonoBehaviour
{

    public List<string> shortRooms = new List<string>();
    public List<string> longRooms = new List<string>();
    public List<string> randomRooms = new List<string>();

   int mainRoom = 0;
    int secondRoom = 0;

    public List<int> mainRoomChoices = new List<int>() { 0, 1, 2, 3,4,5};
    List<int> secondRoomChoices = new List<int>() { 0, 1, 2, 3,4,5 };

    Dictionary<string, string> mainHallDirections = new Dictionary<string, string>();
    Dictionary<string, string> secondHallDirections = new Dictionary<string, string>();

    ClickManager clickManager;

    bool inMainHall= true;

    // Start is called before the first frame update
    void Start()
    {
        clickManager = FindObjectOfType<ClickManager>();
    }
    
    public void MainHall(string entrance)
    {
        if (mainRoom < 6)
        {
            if (mainRoom > 2 && !mainHallDirections.ContainsValue("mainGate"))
            {
                mainHallDirections.Add(entrance, "mainGate");
                clickManager.SetNextNode("mainGate");
            }
            else
            {
                int selection = mainRoomChoices[UnityEngine.Random.Range(0, mainRoomChoices.Count)];
                mainRoomChoices.Remove(selection);
                switch (selection)
                {
                    case 0:
                        string randomNode = shortRooms[UnityEngine.Random.Range(0, shortRooms.Count)];
                        shortRooms.Remove(randomNode);
                        mainHallDirections.Add(entrance, randomNode);
                        clickManager.SetNextNode(randomNode);
                        break;
                    case 1:
                        string randomNode1 = randomRooms[UnityEngine.Random.Range(0, randomRooms.Count)];
                        clickManager.SetNextNode(randomNode1);
                        mainHallDirections.Add(entrance, randomNode1);
                        break;
                    case 2:
                        string randomNode2 = longRooms[UnityEngine.Random.Range(0, longRooms.Count)];
                        clickManager.SetNextNode(randomNode2);
                        longRooms.Remove(randomNode2);
                        mainHallDirections.Add(entrance, randomNode2);
                        break;
                    case 3:
                        clickManager.SetNextNode("mainGate");
                        mainHallDirections.Add(entrance, "mainGate");
                        break;
                    case 4:
                        string randomNode3 = shortRooms[UnityEngine.Random.Range(0, shortRooms.Count)];
                        clickManager.SetNextNode(randomNode3);
                        shortRooms.Remove(randomNode3);
                        mainHallDirections.Add(entrance, randomNode3);
                        break;
                    case 5:
                        string randomNode4 = randomRooms[UnityEngine.Random.Range(0, randomRooms.Count)];
                        clickManager.SetNextNode(randomNode4);
                        mainHallDirections.Add(entrance, randomNode4);
                        break;
                }
            }
            mainRoom++;
        }
        else
        {
           string randomNode1 = randomRooms[UnityEngine.Random.Range(0, randomRooms.Count)];
            clickManager.SetNextNode(randomNode1);
        }
    }

    public void SecondHall(string entrance)
    {
        if (secondRoom < 6)
        {
            if (secondRoom > 2 && !secondHallDirections.ContainsValue("secondGate"))
            {
                secondHallDirections.Add(entrance, "secondGate");
                clickManager.SetNextNode("secondGate");
            }
            else
            {
                int selection = secondRoomChoices[UnityEngine.Random.Range(0, secondRoomChoices.Count)];
                secondRoomChoices.Remove(selection);
                switch (selection)
                {
                    case 0:
                        string randomNode = shortRooms[UnityEngine.Random.Range(0, shortRooms.Count)];
                        clickManager.SetNextNode(randomNode);
                        shortRooms.Remove(randomNode);
                        secondHallDirections.Add(entrance, randomNode);
                        break;
                    case 1:
                        string randomNode1 = randomRooms[UnityEngine.Random.Range(0, randomRooms.Count)];
                        clickManager.SetNextNode(randomNode1);
                        secondHallDirections.Add(entrance, randomNode1);
                        break;
                    case 2:
                        string randomNode2 = longRooms[UnityEngine.Random.Range(0, longRooms.Count)];
                        clickManager.SetNextNode(randomNode2);
                        longRooms.Remove(randomNode2);
                        secondHallDirections.Add(entrance, randomNode2);
                        break;
                    case 3:
                        clickManager.SetNextNode("secondGate");
                        break;
                    case 4:
                        string randomNode3 = longRooms[UnityEngine.Random.Range(0, longRooms.Count)];
                        clickManager.SetNextNode(randomNode3);
                        longRooms.Remove(randomNode3);
                        secondHallDirections.Add(entrance, randomNode3);
                        break;
                    case 5:
                        string randomNode4 = randomRooms[UnityEngine.Random.Range(0, randomRooms.Count)];
                        clickManager.SetNextNode(randomNode4);
                        secondHallDirections.Add(entrance, randomNode4);
                        break;
                }
            }
            secondRoom++;
        }
    }

    [YarnCommand("addRoom")]
    public void AddRoom(string room)
    {
        shortRooms.Add(room);
    }


    [YarnCommand("returnToHall")]
    public void ReturnToHall()
    {
        if (inMainHall)
        {
            clickManager.SetNextNode("mainHall");
        }
        else
        {
            clickManager.SetNextNode("secondHall");
        }
    }

    [YarnCommand("moveHalls")]
    public void MoveHalls(string hall)
    {
        if(hall=="main")
        {
            inMainHall = true;
        }
        else
        {
            inMainHall = false;
        }
    }

    [YarnCommand("progressRoom")]
    public void ProgressRoom(string entrance)
    {
        if (inMainHall)
        {
            if(mainHallDirections.ContainsKey(entrance))
            {
                clickManager.SetNextNode(mainHallDirections[entrance]);
            }
            else
            {
                MainHall(entrance);
            }

        }
        else
        {
            if (secondHallDirections.ContainsKey(entrance))
            {
                clickManager.SetNextNode(secondHallDirections[entrance]);
            }
            else
            {
                SecondHall(entrance);
            }
        }
    }
    


}
