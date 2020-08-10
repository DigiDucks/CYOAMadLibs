using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ThreeFace : MonoBehaviour
{
    public Sprite[] faces;
    public Image image;

    [YarnCommand("changeFace")]
    public void ChangeFace(string face)
    {
        int i = 0;
        switch (face)
        {
            case "0":
                i = 0;
                break;
            case "1":
                i = 1;
                break;
            case "2":
                i = 2;
                break;
        }
        image.sprite = faces[i];
    }

    [YarnCommand("add3Face")]
    public void addThree()
    {
        PartyManager p = FindObjectOfType<PartyManager>();
        p.Leave("LostBoy");
        p.AddToParty(GetComponent<OCStats>());
    }
}
