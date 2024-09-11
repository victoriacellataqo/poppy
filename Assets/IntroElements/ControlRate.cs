using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRate : MonoBehaviour
{
    public GameObject ratepop;
    // Start is called before the first frame update
    void OnEnable()
    {
      if(PlayerPrefs.GetInt("NumRate",0)%2==0)  if(ratepop!=null)ratepop.SetActive(true);
        else if (ratepop != null) ratepop.SetActive(false);
        PlayerPrefs.SetInt("NumRate", PlayerPrefs.GetInt("NumRate", 0)+1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
