using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New World Event", menuName ="WorldEvent")] 
public class WorldEvent : ScriptableObject
{
    public float dayStart;

    public int[] QuestNeeded;

    public int priority;

    public string result; 
}
