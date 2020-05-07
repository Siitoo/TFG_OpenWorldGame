using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    public GameObject[] citizensPrefabs;


    public int citizensToSpawn = 2;

    public Waypoint waypoint = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        int count = 0;

        while(count < citizensToSpawn)
        {
            int spawnaObj = Mathf.RoundToInt(Random.Range(0f, citizensPrefabs.Length - 1));
            GameObject obj = Instantiate(citizensPrefabs[spawnaObj]);
            obj.transform.position = transform.position;
            obj.GetComponent<WaypointNavigator>().currentWaypoint = waypoint;

            yield return new WaitForSeconds(1.0f);

            count++;

        }

    }
}
