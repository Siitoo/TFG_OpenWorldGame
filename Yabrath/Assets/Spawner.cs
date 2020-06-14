using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
      foreach(GameObject go in prefabs)
        {
            GameObject.Instantiate(go, transform);
        }
    }


}
