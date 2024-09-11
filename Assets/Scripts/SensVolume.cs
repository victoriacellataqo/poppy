using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensSlider;
    // Start is called before the first frame update
    void Start()
    { 
    }
    
    public void changeVolume()
    {

        PlayerPrefs.SetFloat("Volume", volumeSlider.value); 

    }
    public void changeSens()
    {

        PlayerPrefs.SetFloat("Sensitivity", sensSlider.value);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
