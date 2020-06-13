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
    private float actual_life = 0;
    public Slider slider = null;

    private void Start()
    {
        actual_life = max_life;
        slider.maxValue = max_life;
        slider.value = actual_life;
    }

    public void GetDamage(float dmg)
    {
        actual_life -= dmg;
        slider.value = actual_life;
    }

    public void AttackKick1()
    {
        GameObject[] enemies = EnemiesHitted(true, false);

        DoDmg(enemies,kick1_dmg);
    }

    public void AttackKick2()
    {
        GameObject[] enemies = EnemiesHitted(true, false);

        DoDmg(enemies, kick2_dmg);
    }

    public void AttackKick3()
    {
        GameObject[] enemies = EnemiesHitted(true,false);

        DoDmg(enemies, kick3_dmg);
    }

    public void AttackPunch1()
    {
        GameObject[] enemies = EnemiesHitted(false);

        DoDmg(enemies,punch1_dmg);
    }

    public void AttackPunch2()
    {
        GameObject[] enemies = EnemiesHitted(false,true);

        DoDmg(enemies, punch2_dmg);
    }

    public void AttackPunch3(int hit)
    {
        bool left = false;

        if (hit == 0)
            left = true;

        GameObject[] enemies = EnemiesHitted(false, left);

        DoDmg(enemies, punch3_dmg);
    }

    private GameObject[] EnemiesHitted(bool kick = true, bool left = false)
    {
        Collider[] colliders;

        if (kick)
        {
            if(left)
            {
                colliders = Physics.OverlapBox(left_kick.position, Vector3.up / 1.78f, left_kick.rotation);
            }
            else
            {
                colliders = Physics.OverlapBox(right_kick.position, Vector3.up / 1.78f, right_kick.rotation);
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
                hits.SetValue(col.gameObject, index);
        }
        

        return hits;
    }

    private void DoDmg(GameObject[] enemies, int dmg)
    {
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
               enemy.GetComponent<EntityLife>().ReceiveDmg(dmg);
        }
    }

}
