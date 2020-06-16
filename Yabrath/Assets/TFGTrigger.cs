using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TFGTrigger : MonoBehaviour
{
    public string text = "";

    private GameObject tip = null;

    public int time = 0;
    
    public float actual_time = 0;
    private bool start_count = false;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Canvas");
        tip = go.transform.GetChild(go.transform.childCount - 2).gameObject;
    }

    private void Update()
    {
        if(start_count)
        {
            if (actual_time < time)
                actual_time += Time.deltaTime;
            else
            {
                tip.transform.GetChild(0).GetComponent<Text>().text = "";
                tip.SetActive(false);
                Destroy(this.gameObject);
            }
        }
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
