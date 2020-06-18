using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrafficCharacterController : MonoBehaviour
{
    private float movementSpeed = 1.0f;
    public float rotationSpeed = 120f;
    public float stopDistance = 2.5f;
    public Vector3 destination = Vector3.zero;
    public bool reachedDestination = false;

    private GameObject sun = null;
    WorldManager manager = null;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = Random.Range(1.5f, 3.5f);

        sun = GameObject.FindGameObjectWithTag("Sun");
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<WorldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }

        if(!sun.GetComponent<DayNightCycle>().actual_frame_day || manager.despawnCitizens)
        {
            Destroy(this.gameObject);
        }

    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

}
