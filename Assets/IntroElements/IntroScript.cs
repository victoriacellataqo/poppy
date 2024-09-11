using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Yodo1.MAS;

using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{

    public float seconds;
    public string textToType1;
    public string textToType2;
    public string textToType3;
    public GameObject image1, image2, image3;
    public Text mytext;
    public Button nextbtn;
    int x=0;
    public GameObject loadimage;
    int runInter = 0;
    public GameObject huggy, mommy;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        AdsManager.instance.create();

        AdsManager.instance.InitializeNative(Yodo1U3dNativeAdPosition.NativeBottom);
        AdsManager.instance.ShowBanner();
        Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickbtn()
    {

        if (x == 3) {

            AdsManager.instance.HideBanner();
            AdsManager.instance.HideNative();
            PlayerPrefs.SetInt("first", 1);
            SceneManager.LoadScene("Main Menu 1");
        }
        else {
            
            Run(); 
        
        }

    }
    string textlaunch()
    {
        if (x == 0) { 
            huggy.SetActive(false);
            mommy.SetActive(false);

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            return textToType1;
        }
        if (x == 1) {
            AdsManager.instance.ShowInterstitialIntro();
            huggy.SetActive(true);
            mommy.SetActive(false);
            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(false);
            return textToType2;
        }
        if (x == 2) {
            AdsManager.instance.ShowInterstitialIntro();
            huggy.SetActive(false);
            mommy.SetActive(true);
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            text.text = "START";
            return textToType3;
        }
        return textToType1;

    }
    public void Run()
    {
        
        StartCoroutine(TypeText(textlaunch()));


    }

    private IEnumerator TypeText(string textToType)
    {

       
        mytext.text = "";
        nextbtn.gameObject.SetActive(false);
        loadimage.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        loadimage.gameObject.SetActive(false);
        for (int i = 0; i <= textToType.Length; i++)
        {


            yield return new WaitForSecondsRealtime(seconds);

            mytext.text = textToType.Substring(0, i) ;
        }
        x++;
        nextbtn.gameObject.SetActive(true);


    }


  

}
