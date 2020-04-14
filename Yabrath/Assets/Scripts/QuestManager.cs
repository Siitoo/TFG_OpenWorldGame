using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Quest
{
    public int questId = 0;
    public string name = "No name" ;
    public string description = "No description";
    public int current = 0;
    public int total = 0;
    public bool completed = false;

    public Quest(int questId, string name, string description, int current, int total, bool completed = false)
    {
        this.questId = questId;
        this.name = name;
        this.description = description;
        this.current = current;
        this.total = total;
        this.completed = completed;
    }
}

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public int questId;
        public string name;
        public string description;
        public int current;
        public int total;
        public bool completed;
    }

    [System.Serializable]
    public class Quests
    {
        public Quest[] quests;
    }

    public Quests quests;
    public List<Quest> active_quests;

    public TextAsset text;

    public Image mission_panel = null;
    public GameObject text_base = null;

    void Awake()
    {
        string path = Application.dataPath + "/Quests/" + text.name + ".json";
        string json_string = File.ReadAllText(path);
        quests = JsonUtility.FromJson<Quests>(json_string);

    }

    public void CheckQuest(int id)
    {
        foreach(Quest q in quests.quests)
        {
            if(q.questId == id)
            {
                if(q.current >= q.total)
                {
                    q.completed = true;

                    active_quests.Remove(q);
                    UpdateQuestPanel();
                }

                break;
            }
        }

    }

    public void AddCurrent(int id,int value)
    {
        foreach (Quest q in quests.quests)
        {
            if (q.questId == id)
            {
                q.current += value;

                CheckQuest(id);

                break;
            }
        }
    }

    public void AddNewQuest(int id)
    {
        foreach (Quest q in quests.quests)
        {
            if (q.questId == id)
            {
                active_quests.Add(q);

                UpdateQuestPanel();

                break;
            }
        }
    }

    public void UpdateQuestPanel()
    {
        int childs = mission_panel.transform.childCount;

        for (int i = 1; i < childs; ++i)
        {
            GameObject.Destroy(mission_panel.transform.GetChild(i).gameObject);
        }

        int discountChilds = 1;

        foreach (Quest q in active_quests)
        {
            if (mission_panel != null)
            {
                GameObject go = GameObject.Instantiate(text_base, mission_panel.transform);
                go.transform.GetChild(0).GetComponent<Text>().text = q.name;
                go.transform.GetChild(1).GetComponent<Text>().text = q.description;

                go.GetComponent<RectTransform>().localPosition = new Vector3(0, 200 - 100 * discountChilds, 0);

                discountChilds++;

            }
        }
    }


}
