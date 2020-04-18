using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoorTriggerEnter : MonoBehaviour
{
    private Transform t;

    private void Awake()
    {
        t = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.transform.localEulerAngles = new Vector3(0,-90,90);
            gameObject.transform.localPosition = new Vector3(6.1f,0.0f,5.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.transform.localEulerAngles = new Vector3 (t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, t.localRotation.eulerAngles.z);
            gameObject.transform.localPosition =  new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z);
        }
    }
}
