﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEntityBehaviour : MonoBehaviour
{
    EntityLife life;
    bool seePlayer = false;
    bool attack = false;
    bool die = false;

    Animator anim;
    Transform player_transform;

    public Transform hand = null;
    public bool needCount = true;

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

        if (seePlayer && !die)
        {
            if (!attack)
            {
                Movement();
                if(!gameObject.GetComponent<AudioSource>().isPlaying)
                    gameObject.GetComponent<AudioSource>().Play();
            }
            else
                gameObject.GetComponent<AudioSource>().Stop();

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
                if(needCount)
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>().AddCurrent(3, 1);
                StartCoroutine("DieTime");
            }

            die = true;
        }

    }

    void Movement()
    {
        transform.LookAt(player_transform);
        transform.Translate(Vector3.forward*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
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

    public void Attack(float dmg)
    {
        Collider[] colliders;
        Vector3 m = new Vector3(0.5f, 1, 0.5f);
        colliders = Physics.OverlapBox(hand.position, m , hand.rotation);

        foreach (Collider col in colliders)
        {
            if(col.tag == "Player")
            {
                hand.gameObject.GetComponent<AudioSource>().Play();
                col.gameObject.GetComponent<PlayerAttacks>().GetDamage(dmg);
                return;
            }
        }

    }


}
