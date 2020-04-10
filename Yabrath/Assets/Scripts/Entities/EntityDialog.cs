using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EntityDialog : MonoBehaviour
{
    public TextAsset text;

    [System.Serializable]
    public class Dialog
    {
        public int id;
        public string text;
        public string type;
        public int[] answers;
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
        string path = Application.dataPath + "/Dialogs/" + text.name + ".json";
        string json_string = File.ReadAllText(path);
        dialogs = JsonUtility.FromJson<Dialogs>(json_string);
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void NextDialog(int new_actual = -2)
    {
        if (new_actual == -1)
        {
            FinishDialog();
            return;
        }
        else if (new_actual != -2)
            actual_dialog = new_actual;
        else
        {
            if(dialogs.dialogs[actual_dialog].answers.Length == 1)
            {
                actual_dialog = dialogs.dialogs[actual_dialog].answers[0];

                if (actual_dialog == -1)
                {
                    FinishDialog();
                    return;
                }
            }
        }

        GameObject dialog_ui = canvas.transform.GetChild(1).gameObject;
        dialog_ui.SetActive(true);

        dialog_ui.transform.GetChild(0).GetComponent<Text>().text = dialogs.dialogs[actual_dialog].text;

        if (dialogs.dialogs[actual_dialog].type != "NPCTalk")
        {

            GameObject answers_ui = canvas.transform.GetChild(2).gameObject;
            answers_ui.SetActive(true);

            if (dialogs.dialogs[actual_dialog].answers[0] != -1)
            {
                for (int i = 0; i < dialogs.dialogs[actual_dialog].answers.Length; ++i)
                {
                    answers_ui.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[i]].text;
                    answers_ui.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                    int tmp = i;
                    answers_ui.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { OnClickButtonDialog(tmp); });
                }
            }
            else
            {
                answers_ui.SetActive(false);
            }
        }

    }

    public void FinishDialog()
    {
        actual_dialog = 0;
        GameObject dialog_ui = canvas.transform.GetChild(1).gameObject;
        dialog_ui.SetActive(false);
        GameObject answers_ui = canvas.transform.GetChild(2).gameObject;
        answers_ui.SetActive(false);
    }

    public void OnClickButtonDialog(int id_button)
    {
        actual_dialog = dialogs.dialogs[dialogs.dialogs[actual_dialog].answers[id_button]].answers[0];
       NextDialog(actual_dialog);

    }

}
