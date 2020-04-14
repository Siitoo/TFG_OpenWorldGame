using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public int questId = 1;

    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>().AddCurrent(questId, 1);

        Destroy(this.gameObject);

    }
}
