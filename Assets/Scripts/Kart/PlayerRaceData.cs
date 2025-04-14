using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class PlayerRaceData : MonoBehaviour
{

    public string playerName;
    public int lapCount;
    public float distance;

    public PlayerRaceData(string name, int lap, float distance)
    {
        playerName = name;
        lapCount = lap;
        this.distance = distance;
    }
}
