using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public TextAsset text;

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


}
