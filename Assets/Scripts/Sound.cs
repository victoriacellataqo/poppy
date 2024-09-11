﻿using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sounds
{
    public string name;
    public float volume;
    public AudioClip clip;
    public float pitch;
    [HideInInspector]
    public AudioSource audio;

}
