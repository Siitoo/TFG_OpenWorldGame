using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDayScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<WorldManager>().setNewDay();
            Destroy(this.gameObject);
        }
    }
}
