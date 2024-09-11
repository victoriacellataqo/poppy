using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambmanager : MonoBehaviour
{
    AudioSource ad;
    public AudioClip[] a;
    int b;
    public GameObject audioObj2;
    AudioSource audio1, audio2;
    // Start is called before the first frame update
    void Start()
    {
        ad = GetComponent<AudioSource>();
        ad.clip = a[b];
        ad.Play();
        audio1 = GetComponent<AudioSource>();
        audio2 = audioObj2.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio1.volume = PlayerPrefs.GetFloat("Volume", 1f);
        audio2.volume = PlayerPrefs.GetFloat("Volume", 1f);
        if (!ad.isPlaying)
        {
            print("playing");
            b++;
            if(b<5)
            {
                ad.clip = a[b];
                ad.Play();
            }
            else
            {
                b = 0;
                ad.clip = a[b];
                ad.Play();
            }
        }
    }
}
