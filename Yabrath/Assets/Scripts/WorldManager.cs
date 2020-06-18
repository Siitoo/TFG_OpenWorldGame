using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public WorldEvent[] events;

    public bool despawnCitizens = false;
    public bool newDay = false;
    private float totalDay = 0;

    public GameObject eventPanel = null;
    public GameObject[] results;

    WorldEvent worldEvent = null;
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
        despawnCitizens = true;
    }

    private void CheckEvents()
    {
      
        int selected = 0;

        for(int i = 0, k = 0; i < events.Length; ++i, ++k)
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

                            selected = k;
                        }
                    }
                    else
                    {
                        selected = k;
                        worldEvent = actualEvent;
                    }
                }
            }

           
        }

        if(worldEvent != null)
        {
            Cursor.visible = true;
            SetEventText(worldEvent.result);

            if(results[selected] != null)
                Instantiate(results[selected]);
        }

    }

    private void SetEventText(string description)
    {
        eventPanel.SetActive(true);
        eventPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = description;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
