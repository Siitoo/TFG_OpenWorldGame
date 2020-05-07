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
        public int nextQuest;
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

    public GameObject questIconPrefab = null;
    public List<GameObject> questIcons;

    public GameObject[] questTrigger;

    void Awake()
    {
        string path = Application.dataPath + "/Quests/" + text.name + ".json";
        string json_string = File.ReadAllText(path);
        quests = JsonUtility.FromJson<Quests>(json_string);
        questIcons = new List<GameObject>();
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

                    if(q.nextQuest > 0)
                    {
                        AddNewQuest(q.nextQuest);
                    }

                    active_quests.Remove(q);

                    foreach(GameObject go in questIcons)
                    {
                        if(go.GetComponent<QuestIconIdentifier>().questId == q.questId)
                        {
                            questIcons.Remove(go);

                            Destroy(go);
                            break;
                        }
                    }

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

                if(id-1 < questTrigger.Length)
                {
                    questTrigger[id-1].SetActive(true);

                    GameObject go = GameObject.Instantiate(questIconPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
                    go.GetComponent<QuestIconIdentifier>().questId = id;
                    questIcons.Add(go);
                }

                UpdateQuestPanel();

                break;
            }
        }
    }

    public void UpdateQuestPanel()
    {
        if(mission_panel.transform.childCount > 0)
        {
            mission_panel.gameObject.SetActive(true);
        }
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

    private void Update()
    {
        if(questIcons != null && questIcons.Count > 0)
        {
            foreach(GameObject go in questIcons)
            {
                GameObject triggerObject = questTrigger[go.GetComponent<QuestIconIdentifier>().questId-1];

                Image image = go.GetComponent<Image>();

                float minX = image.GetPixelAdjustedRect().width / 2;
                float maxX = Screen.width - minX;

                float minY = image.GetPixelAdjustedRect().height / 2;
                float maxY = Screen.height - minY;

                Vector2 pos = Camera.main.WorldToScreenPoint(triggerObject.transform.position);

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if(Vector3.Dot((triggerObject.transform.position - Camera.main.transform.position), Camera.main.transform.forward) < 0)
                {
                    if(pos.x < Screen.width/2)
                    {
                        pos.x = maxX;
                    }
                    else
                    {
                        pos.x = minX;
                    }
                }


                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);

                go.transform.position = pos;
                go.transform.GetChild(0).GetComponent<Text>().text = ((int)Vector3.Distance(triggerObject.transform.position, player.transform.position)).ToString();
            }
        }
    }

    public bool CheckQuestCompleted(int id)
    {
        return quests.quests[id].completed;
    }

}
