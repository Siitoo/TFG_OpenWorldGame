using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject[] enemies = EnemiesHitted(false);

        DoDmg(enemies, punch2_dmg);
    }

    public void AttackPunch3()
    {
        GameObject[] enemies = EnemiesHitted(false);

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
                colliders = Physics.OverlapBox(left_punch.position, Vector3.up / 1.78f, left_punch.rotation);
            }
            else
            {
                colliders = Physics.OverlapBox(right_punch.position, Vector3.up / 1.78f, right_punch.rotation);
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
            enemy.GetComponent<EntityLife>().ReceiveDmg(dmg);
        }
    }

}
