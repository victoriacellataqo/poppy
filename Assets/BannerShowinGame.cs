using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerShowinGame : MonoBehaviour
{
    public bool hide;
    // Start is called before the first frame update
    void Start()
    {
        
        if (!hide)
        {
            if (AdsManager.instance != null) AdsManager.instance.ShowBanner();

        }
        else if (AdsManager.instance != null) AdsManager.instance.HideBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
