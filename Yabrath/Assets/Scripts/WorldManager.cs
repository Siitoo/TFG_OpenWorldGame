using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{

    public WorldEvent[] events;

    private bool newDay = false;
    private float totalDay = 0;

    public GameObject eventPanel = null;

    private void Update()
    {
        if(newDay)
        {
            newDay = false;
            CheckEvents();
        }
    }

    public void setNewDay()
    {
        totalDay++;
        newDay = true;
    }


    private void CheckEvents()
    {
        WorldEvent worldEvent = null;

        for(int i = 0; i < events.Length; ++i)
        {
            WorldEvent actualEvent = events[i];

            if(actualEvent.dayStart == totalDay)
            {
                bool itsActualEvent = true;

                for(int j = 0; j < actualEvent.QuestNeeded.Length; ++j)
                {
                    if(gameObject.GetComponent<QuestManager>().CheckQuestCompleted(actualEvent.QuestNeeded[j]))
                    {
                        itsActualEvent = true;
                    }
                    else
                    {
                        itsActualEvent = false;
                        break;
                    }
                }

                if(itsActualEvent)
                {
                    if(worldEvent != null)
                    {
                        if(actualEvent.priority < worldEvent.priority)
                        {
                            worldEvent = actualEvent;
                        }
                    }
                    else
                    {
                        worldEvent = actualEvent;
                    }
                }
            }


        }

        if(worldEvent != null)
        {
            SetEventText(worldEvent.result);
        }

    }

    private void SetEventText(string description)
    {
        eventPanel.SetActive(true);
        eventPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = description;
    }

}
