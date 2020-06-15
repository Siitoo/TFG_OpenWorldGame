using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip walk_clip = null;
    public float volume = 0.2f;
    public float normal_pitch = 0.2f;
    public float run_pitch = 2.5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioSource source = other.gameObject.GetComponent<AudioSource>();
            source.clip = walk_clip;
            source.volume = volume;
            source.pitch = normal_pitch;



        }
    }

}
