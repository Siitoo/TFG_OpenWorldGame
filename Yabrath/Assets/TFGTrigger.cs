using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TFGTrigger : MonoBehaviour
{
    public string text = "";

    private GameObject tip = null;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Canvas");
        tip = go.transform.GetChild(go.transform.childCount - 1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        tip.SetActive(true);
        tip.transform.GetChild(0).GetComponent<Text>().text = text;
    }

    private void OnTriggerExit(Collider other)
    {
        tip.transform.GetChild(0).GetComponent<Text>().text = "";
        tip.SetActive(false);
    }
}
