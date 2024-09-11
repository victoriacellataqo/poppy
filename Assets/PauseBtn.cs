using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showInter()
    {
        if(AdsManager.instance!=null)
        AdsManager.instance.ShowInterstitial();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
