using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEntityBehaviour : MonoBehaviour
{
    EntityLife life;
    bool seePlayer = false;
    bool attack = false;
    bool die = false;

    Animator anim;
    Transform player_transform;

    // Start is called before the first frame update
    void Start()
    {
        life = gameObject.GetComponent<EntityLife>();
        anim = gameObject.GetComponent<Animator>();
        seePlayer = anim.GetBool("SeePlayer");
        attack = anim.GetBool("AttackRange");
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Enemy")
        {
            if (seePlayer && !die)
            {
                if (!attack)
                    Movement();

                if (Mathf.Abs(Vector3.SqrMagnitude(transform.position - player_transform.position)) <= 1.0f)
                {
                    attack = true;
                }
                else
                    attack = false;

                anim.SetBool("AttackRange", attack);

            }

            if (life.actual_life <= 0)
            {

                if (!die)
                {
                    anim.SetBool("Die", true);
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>().AddCurrent(3, 1);
                    StartCoroutine("DieTime");
                }

                die = true;
            }
        }
    }

    void Movement()
    {
        transform.LookAt(player_transform);
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Enemy")
        {
            seePlayer = true;
            anim.SetBool("SeePlayer", seePlayer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Enemy")
        {
            seePlayer = true;
            anim.SetBool("SeePlayer", seePlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Enemy")
        {
            seePlayer = true;
            anim.SetBool("SeePlayer", seePlayer);
        }
    }

    IEnumerator DieTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
