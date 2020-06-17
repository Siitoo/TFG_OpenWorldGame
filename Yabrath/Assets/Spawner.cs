using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] prefabs;
    public bool destroy = false;
    public bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!trigger)
        {
            foreach (GameObject go in prefabs)
            {
                GameObject.Instantiate(go, transform);
            }

            if (destroy)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trigger)
        {
            if (other.tag == "Player")
            {
                foreach (GameObject go in prefabs)
                {
                    GameObject tmp = GameObject.Instantiate(go);
                    tmp.transform.parent = null;
                    Vector3 v = new Vector3(148, 72, 448);
                    tmp.transform.position = v;
                }

                if (destroy)
                    Destroy(this.gameObject);
            }
        }

    }
}
