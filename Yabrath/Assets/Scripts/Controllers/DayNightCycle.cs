using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    private Material skybox_material = null;
    private Light main_light = null;

    public Gradient night_day_color;

    public float max_intensity = 3.0f;
    public float min_intensity = 0.0f;
    public float min_intesity_point = -0.2f;

    public float max_ambient = 1.0f;
    public float min_ambient = 0.0f;
    public float min_ambient_point = -0.2f;

    public float day_atmosphere_thickness = 0.4f;
    public float night_atmosphere_thickness = 0.87f;

    public Vector3 day_rotate_speed = Vector3.zero;
    public Vector3 night_rotate_speed = Vector3.zero;

    public float sky_speed = 1.0f;

    private bool last_frame_day = true;
    public bool actual_frame_day = false;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = -1;
        Application.targetFrameRate = 30;
        main_light = GetComponent<Light>();
        skybox_material = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        float range = 1 - min_intesity_point;
        float dot = Mathf.Clamp01((Vector3.Dot(main_light.transform.forward, Vector3.down)-min_intesity_point) / range);
        float new_intensity = ((max_intensity - min_intensity) * dot) + min_intensity;

        main_light.intensity = new_intensity;

        range = 1 - min_ambient_point;
        dot = Mathf.Clamp01((Vector3.Dot(main_light.transform.forward, Vector3.down) - min_ambient_point) / range);
        new_intensity = ((max_ambient - min_ambient) * dot) + min_ambient;

        RenderSettings.ambientIntensity = new_intensity;

        main_light.color = night_day_color.Evaluate(dot);
        RenderSettings.ambientLight = main_light.color;

        float atmosphere_stickness = ((day_atmosphere_thickness - night_atmosphere_thickness) * dot) + night_atmosphere_thickness;
        skybox_material.SetFloat("_AtmosphereThickness", atmosphere_stickness);

        if (dot > 0)
        {
            transform.Rotate(day_rotate_speed * Time.deltaTime * sky_speed);
            actual_frame_day = true;
        }
        else
        {
            transform.Rotate(night_rotate_speed * Time.deltaTime * sky_speed);
            actual_frame_day = false;
        }

        if (!last_frame_day)
        {
            if (actual_frame_day)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<WorldManager>().setNewDay();
            }
            last_frame_day = actual_frame_day;
        }
        else
            last_frame_day = actual_frame_day;

    }
}
