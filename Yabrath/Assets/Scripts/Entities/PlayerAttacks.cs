using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour
{
    public Transform left_kick = null;
    public Transform right_kick = null;
    public int kick1_dmg = 10;
    public int kick2_dmg = 20;
    public int kick3_dmg = 30;


    public Transform left_punch = null;
    public Transform right_punch = null;
    public int punch1_dmg = 10;
    public int punch2_dmg = 10;
    public int punch3_dmg = 10;

    public float max_life = 100;
    public float actual_life = 0;
    public Slider slider = null;

    public float time_to_regen = 10.0f;
    public float regen_per_second = 5.0f;
    private float actual_time = 0.0f;

    private void Start()
    {
        actual_life = max_life;
        slider.maxValue = max_life;
        slider.value = actual_life;
    }

    private void Update()
    {
        if(max_life > actual_life)
        {
            if(actual_time < time_to_regen)
            {
                actual_time += Time.deltaTime;
            }
            else
            {
                actual_life += regen_per_second * Time.deltaTime;

                if (actual_life > max_life)
                {
                    actual_life = max_life;
                    actual_time = 0.0f;
                }

                slider.value = actual_life;
            }
        }

        if(actual_life <= 0.0f)
        {
            gameObject.GetComponent<BasicBehaviour>().death = true;
        }

    }

    public void GetDamage(float dmg)
    {
        actual_life -= dmg;
        slider.value = actual_life;
        actual_time = 0.0f;
    }

    public void AttackKick1()
    {
        GameObject[] enemies = EnemiesHitted(true, false);

        if(enemies.Length > 0)
        {
            right_kick.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies,kick1_dmg);
    }

    public void AttackKick2()
    {
        GameObject[] enemies = EnemiesHitted(true, false);

        if (enemies.Length > 0)
        {
            left_kick.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies, kick2_dmg);
    }

    public void AttackKick3()
    {
        GameObject[] enemies = EnemiesHitted(true,false);

        if (enemies.Length > 0)
        {
            right_kick.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies, kick3_dmg);
    }

    public void AttackPunch1()
    {
        GameObject[] enemies = EnemiesHitted(false);

        if (enemies.Length > 1)
        {
            left_punch.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies,punch1_dmg);
    }

    public void AttackPunch2()
    {
        GameObject[] enemies = EnemiesHitted(false,true);

        if (enemies.Length > 1)
        {
            right_punch.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies, punch2_dmg);
    }

    public void AttackPunch3(int hit)
    {
        bool left = false;

        if (hit == 0)
            left = true;

        GameObject[] enemies = EnemiesHitted(false, left);

        if (enemies.Length > 1)
        {
            if(hit == 0)
                left_punch.gameObject.GetComponent<AudioSource>().Play();
            else
                right_punch.gameObject.GetComponent<AudioSource>().Play();
        }

        DoDmg(enemies, punch3_dmg);
    }

    private GameObject[] EnemiesHitted(bool kick = true, bool left = false)
    {
        Collider[] colliders;

        if (kick)
        {
            Vector3 m = new Vector3(0.5f, 1f, 0.5f);
            if(left)
            {
                colliders = Physics.OverlapBox(left_kick.position, m, left_kick.rotation);
            }
            else
            {
                colliders = Physics.OverlapBox(right_kick.position, m, right_kick.rotation);
            }
        }
        else
        {
            if (left)
            {
                colliders = Physics.OverlapSphere(left_punch.position, 0.8f);
            }
            else
            {
                colliders = Physics.OverlapSphere(right_punch.position, 0.8f);
            }
        }


        GameObject[] hits = new GameObject[colliders.Length];
        int index = 0;
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                hits.SetValue(col.gameObject, index);
                index += 1;
            }
        }
        

        return hits;
    }

    private void DoDmg(GameObject[] enemies, int dmg)
    {

        GameObject go = null;
        float min_distance = 9999;
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                Vector3 d = enemy.transform.position - transform.position;

                if(d.magnitude < min_distance)
                {
                    min_distance = d.magnitude;
                    go = enemy;
                }

            }
              
        }

        if(go != null)
            go.GetComponent<EntityLife>().ReceiveDmg(dmg);
    }

}
