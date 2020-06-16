using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipTrigger : MonoBehaviour
{
    public string text = "";

    private GameObject tip = null;

    public int time = 0;

    private float actual_time = 0;
    private bool start_count = false;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Canvas");
        tip = go.transform.GetChild(go.transform.childCount - 1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tip.SetActive(true);
            tip.transform.GetChild(0).GetComponent<Text>().text = text;

            if (time > 0)
                start_count = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tip.transform.GetChild(0).GetComponent<Text>().text = "";
        tip.SetActive(false);
        Destroy(this.gameObject);
    }
}
