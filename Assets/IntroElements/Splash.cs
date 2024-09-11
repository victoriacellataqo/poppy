using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using LitJson;
using System;
using UnityEngine.SceneManagement;

using System.Net;
public class Splash : MonoBehaviour
{
   
    // Start is called before the first frame update

   
    void Start()
    {

        StartCoroutine(statusgame());

    }



    IEnumerator statusgame()
    {
        yield return new WaitForSecondsRealtime(2f);
        AdsManager.instance.HideBanner();
        AdsManager.instance.HideNative();
        if (AdsManager.instance.getIntro())
        {
            if (PlayerPrefs.GetInt("first",0) != 1)
            { 
                SceneManager.LoadScene("Intro");
            }
            else
            { 
                SceneManager.LoadScene(1);

            }
        }


        else
        { 
            SceneManager.LoadScene(1);

        } 
       
    }



}
