using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityLife : MonoBehaviour
{
    public int max_life = 100;
    public int actual_life = 0;

    private int last_life = 0;

    public RectTransform rect = null;
    private float max_width;
    private bool need_update_hp;
    private Transform main_camera_transform = null;

    // Start is called before the first frame update
    void Start()
    {
        main_camera_transform = Camera.main.transform;
        last_life = actual_life = max_life;
        max_width = rect.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (last_life != actual_life)
        {
            need_update_hp = true;
        }
        else
            need_update_hp = false;

        if (need_update_hp)
            onUpdateHpBar();

        rect.LookAt(main_camera_transform);
    }

    public void onUpdateHpBar()
    {
        if (actual_life < 0)
            actual_life = 0;

        rect.GetChild(0).GetComponent<Slider>().value = (float)actual_life/max_life;

        last_life = actual_life;
    }

    public void ReceiveDmg(int dmg)
    {
        actual_life -= dmg; 
    }

}
