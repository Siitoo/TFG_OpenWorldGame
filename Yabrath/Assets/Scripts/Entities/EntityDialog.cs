using UnityEngine;
using System.IO;

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

    void Awake()
    {
        string path = Application.dataPath + "/Dialogs/" + text.name + ".json";
        string json_string = File.ReadAllText(path);
        dialogs = JsonUtility.FromJson<Dialogs>(json_string);
    }

}
