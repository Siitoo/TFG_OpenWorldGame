using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EntityDialog : MonoBehaviour
{
    public string text;

    [System.Serializable]
    public class Dialog
    {
        public int id;
        public string text;
        public string type;
        public int[] answers;
        public int questID;
    }

    [System.Serializable]
    public class Dialogs
    {
        public Dialog[] dialogs;
    }

    
    public Dialogs dialogs;
    private int actual_dialog = 0;

    private GameObject canvas = null;

    void Awake()
    {
        Cursor.visible = false;

        string path = Application.streamingAssetsPath + "/Dialogs/" + text + ".json";
        string json_string = File.ReadAllText(path);
        dialogs = JsonUtility.FromJson<Dialogs>(json_string);
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void NextDialog(int new_actual = -2)
    {
       if(dialogs.dialogs[actual_dialog].questID != 0)
        {

            GameObject go = GameObject.FindGameObjectWithTag("Manager");

            if (go != null)
            {
                go.GetComponent<QuestManager>().AddNewQuest(dialogs.dialogs[actual_dialog].questID);
            }

            if (dialogs.dialogs[actual_dialog].answers[0] == -4)
            {
                SetEnemyGroup();

                FinishDialog();
                return;
            }
        }

        if (new_actual == -1)
        {
            FinishDialog();
            return;
        }
        else if (new_actual != -2)
            actual_dialog = new_actual;
        else
        {
            if (dialogs.dialogs[actual_dialog].answers.Length == 1)
            {
                actual_dialog = dialogs.dialogs[actual_dialog].answers[0];

                if (actual_dialog == -1)
                {
                    FinishDialog();
                    return;
                }
                else if (actual_dialog == -3)
                {
                    FinishDialog();
                    gameObject.tag = "Untagged";
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BasicBehaviour>().npc_selected = null;
                    Destroy(this);   
                    return;
                }
            }
        }

        GameObject answers_ui = canvas.transform.GetChild(2).gameObject;
        answers_ui.SetActive(false);

        GameObject dialog_ui = canvas.transform.GetChild(1).gameObject;
        dialog_ui.SetActive(true);

        dialog_ui.transform.GetChild(0).GetComponent<Text>().text = dialogs.dialogs[actual_dialog].text;

        if (dialogs.dialogs[actual_dialog].type != "NPCTalk")
        {

            if (dialogs.dialogs[actual_dialog].answers[0] != -1)
            {
                Cursor.visible = true;

                answers_ui.SetActive(true);

                for (int i = 0; i < dialogs.dialogs[actual_dialog].answers.Length; ++i)
                {
                    answers_ui.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[i]].text;
                    answers_ui.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                    int tmp = i;
                    answers_ui.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { OnClickButtonDialog(tmp); });
                }
            }
        }
    }


    public bool isQuestionAnswered()
    {
        bool isQuestion = false;

        if (dialogs.dialogs[actual_dialog].type == "NPCQuestionAnswered")
            isQuestion = true;

        return isQuestion;
    }

    public void FinishDialog(bool lost = true)
    {
       
        GameObject dialog_ui = canvas.transform.GetChild(1).gameObject;
        dialog_ui.SetActive(false);
        GameObject answers_ui = canvas.transform.GetChild(2).gameObject;
        answers_ui.SetActive(false);

        if(lost)
            gameObject.tag = "Untagged";
        actual_dialog = 0;
    }

    public void OnClickButtonDialog(int id_button)
    {

        if (Cursor.visible)
            Cursor.visible = false;

        if (dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[id_button]].questID != 0)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Manager");

            if (go != null)
            {
                go.GetComponent<QuestManager>().AddNewQuest(dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[id_button]].questID);
            }
        }

       actual_dialog = dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[id_button]].answers[0];

        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        if (!tmp.GetComponent<BasicBehaviour>().do_dialog)
            NextDialog(actual_dialog);

    }

    public void SetEnemyGroup()
    {
        if (transform.parent.tag == "EnemyGroup")
        {
            GameObject parent = transform.parent.gameObject;

            for (int i = 0; i < parent.transform.childCount; ++i)
            {
                if (parent.transform.GetChild(i).tag == "NPC")
                    parent.transform.GetChild(i).tag = "Enemy";
                    parent.transform.GetChild(i).tag = "Enemy";
            }

        }
    }

}
