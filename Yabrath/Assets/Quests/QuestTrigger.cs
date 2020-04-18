using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public int questId = 1;

    public int addQuest = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            QuestManager manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>();

            manager.AddCurrent(questId, 1);

            if (addQuest != -1)
            {
                manager.AddNewQuest(addQuest);
            }

            Destroy(this.gameObject);
        }
    }
}
