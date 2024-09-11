using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Text LevelText,secondmessage;
    public bool skinEnter;
    public GameObject ingamepanel;
    public GameObject playerSelectionPanel;
    public GameObject startpanel,gameplaypanel,losepanel,winpanel;
    public GameObject bullet;
    public Transform t1, t2;
    public float timad=60;
    public GameObject revivebtn;
    private void Awake()
    {

        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {    
        timad = 60;
        //Advertisements.Instance.Initialize();
        //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        //LevelText.text = "Level " + (gamemanager.instance.getLevel() + 1);
    }

    void Update()
    {
       if (AdsManager.instance != null && AdsManager.instance.isRewardedAvailable()) { revivebtn.SetActive(true); }
            else { revivebtn.SetActive(false); }
        
        
        timad -= Time.deltaTime;
       
    }

    //public void skinmenu()
    //{
    //    // sound
    //    SoundManager.instance.Play("click");
    //    skinEnter = true;
    //    playerSelectionPanel.SetActive(true);
    //    ingamepanel.SetActive(false);
    //}

    public void btn_retry()
    {

        // sound
        //SoundManager.instance.Play("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    } 
    public void nxtlvlgame()
    {
        
        // sound
        //SoundManager.instance.Play("click");
        SceneManager.LoadScene(1);
    }

    public void nextlvl()
    {
      
        gamemanager.instance.setLevel(gamemanager.instance.getLevel() + 1);
        if (gamemanager.instance.LevelsContenu.Length <= gamemanager.instance.getLevel())
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void shootpower()
    {
        Instantiate(bullet, t1.position, t1.rotation, t2);
    }
}
