using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip deadSound;
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playDead()
    {
        audioSource.clip = deadSound;
        audioSource.Play();
    }
    public void playFire()
    {
        audioSource.clip = fireSound;
        audioSource.Play();
    }
}
