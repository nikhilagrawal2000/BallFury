using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacles : MonoBehaviour {

    public AudioSource gameover_sound;

    private void Start()
    {
        gameover_sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameover_sound.Play();
        }
    }
}
