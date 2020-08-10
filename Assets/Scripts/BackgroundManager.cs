using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BackgroundManager : MonoBehaviour
{
    public Image backgroundImage;

    public Sprite[] backgrounds;

    [YarnCommand("changeBackground")]
    public void ChangeBackground(string name)
    {
        int index = 0;
        switch (name)
        {
            case "placeholder0": index = 0;
                break;
            case "placeholder1": index = 1;
                break;
            case "treasureroom": index = 2;
                break;
            case "blank": index = 3;
                break;
            case "reflectionroom": index = 4;
                break;
            case "reflectionreveal": index = 5;
                break;
            case "classroom": index = 6;
                break;
            case "mainTunnel": index = 7;
                break;
            case "room": index = 8;
                break;
            case "outside": index = 9;
                break;
            case "door": index = 10;
                break;
            case "bedroom": index = 11;
                break;
            case "hell": index = 12;
                break;
            case "grandHall": index = 13;
                break;

            default: Debug.LogError("Incorrect Background Name called: " + name);
                return;
        }

        backgroundImage.sprite = backgrounds[index];
    }


}
