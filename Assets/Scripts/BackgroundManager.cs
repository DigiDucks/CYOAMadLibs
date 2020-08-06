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
            default: Debug.LogError("Incorrect Background Name called: " + name);
                return;
        }

        backgroundImage.sprite = backgrounds[index];
    }


}
