using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLife : MonoBehaviour
{
    public int max_life = 100;
    public int actual_life = 0;

    private int last_life = 0;

    public RectTransform rect = null;
    private Transform main_camera_transform = null;

    // Start is called before the first frame update
    void Start()
    {
        main_camera_transform = Camera.main.transform;
        last_life = actual_life = max_life;
    }

    // Update is called once per frame
    void Update()
    {
       

        if(last_life != actual_life)
        {

        }

        last_life = actual_life;
        rect.LookAt(main_camera_transform);
    }

    public void onUpdateHpBar()
    {
        
    }

    public void ReceiveDmg(int dmg)
    {
        actual_life -= dmg; 
    }

}
