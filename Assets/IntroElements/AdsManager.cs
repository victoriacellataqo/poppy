using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;
using Yodo1.MAS;
using LitJson;
using System;
using UnityEngine.SceneManagement;
public class AdsManager : MonoBehaviour
{
    public static AdsManager instance { get; set; }
    private Yodo1U3dBannerAdView bannerAdView;
    private Yodo1U3dNativeAdView nativeAdView;
    UnityAction<bool> CompleteMethod; 
    public GameObject turnInternet,splash;
    bool intros = false;

    bool unlock = true;
    private Yodo1U3dRewardedInterstitialAd rewardedInterstitialAd;
    int introtest = 0;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        { 
               instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

       
    }
    private IEnumerator Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (Application.internetReachability == NetworkReachability.NotReachable) { turnInternet.SetActive(true); splash.SetActive(false); }
        else
        {

             

                string url = "https://myhostgpjson.xyz/apple/apple9/poppy.json";
                WWW www = new WWW(url);
                yield return www;

                if (www.error == null)
                {
                    string jsonString = www.text;
                    JsonData jsonvale = JsonMapper.ToObject(jsonString);

                    try
                    {
                        intros = bool.Parse(jsonvale["testmode"].ToString());
                         unlock = bool.Parse(jsonvale["unlock"].ToString());  
                        // timeInter = int.Parse(jsonvale["timeInter"].ToString());
                        loadads();
                    }
                    catch (Exception e) { }
                }

             
        }
    }
    void loadads()
    {

        Yodo1AdBuildConfig config =
            new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);


        Yodo1U3dMasCallback.OnSdkInitializedEvent += (success, error) =>
        {
            Debug.Log("[Yodo1 Mas] OnSdkInitializedEvent, success:" + success + ", error: " + error.ToString());
            if (success)
            {
                turnInternet.SetActive(false);
                create();

                InitializeNative(Yodo1U3dNativeAdPosition.NativeVerticalCenter | Yodo1U3dNativeAdPosition.NativeVerticalCenter);
                instance.ShowBanner();

                StartCoroutine(statusgame());

                Debug.Log("[Yodo1 Mas] The initialization has succeeded");
            }
            else
            {
                splash.SetActive(true);
                turnInternet.SetActive(true);
                Debug.Log("[Yodo1 Mas] The initialization has failed");
            }
        };

        Yodo1U3dMas.InitializeMasSdk();

    }
    public bool getIntro()
    {

        if (intros) return true;
        else return false;
    }
    public bool getUnlock()
    {

        if (unlock) return true;
        else return false;
    }
    IEnumerator statusgame()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        PlayerPrefs.SetInt("ads", 1);
        turnInternet.SetActive(false); 
        splash.SetActive(true);
         
    }
    

    private void OnInitialized()
    {
        //Show ads only after this method is called
        //This callback is not mandatory if you do not want to show banners as soon as your app starts.
    }
    public void initialize()
    {
        Yodo1U3dMas.InitializeMasSdk();
    }

    public void tryAgain()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable) { turnInternet.SetActive(true); splash.SetActive(false); }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
    public void create()
    {
        InitializeBanner();
        InitializeInterstitial();
        InitializeRewardedAds();
        this.RequestRewardedInterstitial();

    }

    void InitializeInterstitial()
    {
        // Instantiate
        Yodo1U3dInterstitialAd.GetInstance();

        // Ad Events
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadedEvent += OnInterstitialAdLoadedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdLoadFailedEvent += OnInterstitialAdLoadFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenedEvent += OnInterstitialAdOpenedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdOpenFailedEvent += OnInterstitialAdOpenFailedEvent;
        Yodo1U3dInterstitialAd.GetInstance().OnAdClosedEvent += OnInterstitialAdClosedEvent;

        // Load an ad
        LoadInterstitial();
    }
    public void LoadInterstitial()
    {
        Yodo1U3dInterstitialAd.GetInstance().LoadAd();
    }

    public void ShowInterstitial()
    {
        if (Yodo1U3dInterstitialAd.GetInstance().IsLoaded())
        {

            Yodo1U3dInterstitialAd.GetInstance().ShowAd();
            Debug.Log("[Yodo1 Mas] Interstitial ad showed.");
        }
    }

    public void ShowInterstitialIntro()
    {
        if (Yodo1U3dInterstitialAd.GetInstance().IsLoaded() && introtest == 0)
        {

            Yodo1U3dInterstitialAd.GetInstance().ShowAd();
            Debug.Log("[Yodo1 Mas] Interstitial ad showed.");
        }


    }
    private void OnInterstitialAdLoadedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdLoadedEvent event received" + ad.GetHashCode());

    }

    private void OnInterstitialAdLoadFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdLoadFailedEvent event received with error: " + adError.ToString());
    }

    private void OnInterstitialAdOpenedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdOpenedEvent event received");
    }

    private void OnInterstitialAdOpenFailedEvent(Yodo1U3dInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdOpenFailedEvent event received with error: " + adError.ToString());
        LoadInterstitial();
    }

    private void OnInterstitialAdClosedEvent(Yodo1U3dInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnInterstitialAdClosedEvent event received");
        introtest++;
        LoadInterstitial();
    }
    /**************** Rewarded ads code ****************/



    /**************** Rewarded ads code ****************/

    public void InitializeRewardedAds()
    {
        Yodo1U3dRewardAd.GetInstance();

        // Ad Events
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;

        // Load an ad
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }
    public bool isRewardedAvailable()
    {

        if (Yodo1U3dRewardAd.GetInstance().IsLoaded()) return true;
        else return false;

    }
    public void ShowRewarded(UnityAction<bool> CompleteMethod)
    {

        if (Yodo1U3dRewardAd.GetInstance().IsLoaded())
        {
            this.CompleteMethod = CompleteMethod;
            Yodo1U3dRewardAd.GetInstance().ShowAd();
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
        }
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdLoadedEvent event received" + ad.GetHashCode());
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdLoadFailedEvent event received with error: " + adError.ToString());
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdOpenedEvent event received");
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdOpenFailedEvent event received with error: " + adError.ToString());
        LoadRewardedAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardAdClosedEvent event received");
        LoadRewardedAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        if (CompleteMethod != null)
        {
            CompleteMethod.Invoke(true);
        }
        Debug.Log("[Yodo1 Mas] OnRewardAdEarnedEvent event received");
    }






    /**************** Banner ads code ****************/



    public void InitializeBanner()
    {

        // Clean up banner before reusing
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
        }

        //Create a banner ad
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerTop | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);

        // Add Events
        bannerAdView.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        bannerAdView.OnAdFailedToLoadEvent += OnBannerAdFailedToLoadEvent;
        bannerAdView.OnAdOpenedEvent += OnBannerAdOpenedEvent;
        bannerAdView.OnAdFailedToOpenEvent += OnBannerAdFailedToOpenEvent;
        bannerAdView.OnAdClosedEvent += OnBannerAdClosedEvent;

        // Load banner ads, the banner ad will be displayed automatically after loaded
        bannerAdView.LoadAd();

    }

    public void ShowBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Show();
        }
    }

    public void HideBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Hide();
        }
    }

    public void DestroyBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
            bannerAdView = null;
        }
    }

    private void OnBannerAdLoadedEvent(Yodo1U3dBannerAdView adView)
    {
        // Banner ad is ready to be shown.
        Debug.Log("[Yodo1 Mas] OnBannerAdLoadedEvent event received");
    }

    private void OnBannerAdFailedToLoadEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToLoadEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdOpenedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdOpenedEvent event received");
    }

    private void OnBannerAdFailedToOpenEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToOpenEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdClosedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdClosedEvent event received");
    }


    // native


    public void InitializeNative(Yodo1U3dNativeAdPosition yodoposition)
    {
        // Clean up native before reusing
        if (nativeAdView != null)
        {
            nativeAdView.Destroy();
        }

        // Create a 375x200 native at top of the screen
        nativeAdView = new Yodo1U3dNativeAdView(yodoposition, 0, 0, 375, 200);

        // Ad Events
        nativeAdView.OnAdLoadedEvent += OnNativeAdLoadedEvent;
        nativeAdView.OnAdFailedToLoadEvent += OnNativeAdFailedToLoadEvent;

        // Load native ads, the native ad will be displayed automatically after loaded
        nativeAdView.LoadAd();
    }

    public void ShowNative()
    {
        if (nativeAdView != null)
        {
            nativeAdView.Show();
        }
    }

    public void HideNative()
    {
        if (nativeAdView != null)
        {
            nativeAdView.Hide();
        }
    }

    public void DestroyNative()
    {
        if (nativeAdView != null)
        {
            nativeAdView.Destroy();
            nativeAdView = null;
        }
    }

    private void OnNativeAdLoadedEvent(Yodo1U3dNativeAdView adView)
    {
        // Native ad is ready to be shown.
        Debug.Log("[Yodo1 Mas] OnNativeAdLoadedEvent event received");
    }

    private void OnNativeAdFailedToLoadEvent(Yodo1U3dNativeAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnNativeAdFailedToLoadEvent event received with error: " + adError.ToString());
    }

    // ----- Rewarded Interstitials





     
    private void RequestRewardedInterstitial()
    {
        rewardedInterstitialAd = Yodo1U3dRewardedInterstitialAd.GetInstance();

        // Ad Events
        rewardedInterstitialAd.OnAdLoadedEvent += OnRewardedInterstitialAdLoadedEvent;
        rewardedInterstitialAd.OnAdLoadFailedEvent += OnRewardedInterstitialAdLoadFailedEvent;
        rewardedInterstitialAd.OnAdOpenedEvent += OnRewardedInterstitialAdOpenedEvent;
        rewardedInterstitialAd.OnAdOpenFailedEvent += OnRewardedInterstitialAdOpenFailedEvent;
        rewardedInterstitialAd.OnAdClosedEvent += OnRewardedInterstitialAdClosedEvent;
        rewardedInterstitialAd.OnAdEarnedEvent += OnRewardedInterstitialAdEarnedEvent;

        rewardedInterstitialAd.LoadAd();
    }


    public void ShowRewardedInterstitial(UnityAction<bool> CompleteMethod)
    {
        this.CompleteMethod = CompleteMethod;
        bool isLoaded = rewardedInterstitialAd.IsLoaded();

        if (isLoaded) rewardedInterstitialAd.ShowAd();
        else ShowRewarded(CompleteMethod);
    }

    private void OnRewardedInterstitialAdLoadedEvent(Yodo1U3dRewardedInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdLoadedEvent event received");
    }

    private void OnRewardedInterstitialAdLoadFailedEvent(Yodo1U3dRewardedInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdLoadFailedEvent event received with error: " + adError.ToString());
        rewardedInterstitialAd.LoadAd();
    }

    private void OnRewardedInterstitialAdOpenedEvent(Yodo1U3dRewardedInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdOpenedEvent event received");
    }

    private void OnRewardedInterstitialAdOpenFailedEvent(Yodo1U3dRewardedInterstitialAd ad, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdOpenFailedEvent event received with error: " + adError.ToString());
    }

    private void OnRewardedInterstitialAdClosedEvent(Yodo1U3dRewardedInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdClosedEvent event received");
        rewardedInterstitialAd.LoadAd();
    }

    private void OnRewardedInterstitialAdEarnedEvent(Yodo1U3dRewardedInterstitialAd ad)
    {
        Debug.Log("[Yodo1 Mas] OnRewardedInterstitialAdEarnedEvent event received");
        // Add your reward code here
        if (CompleteMethod != null)
        {
            CompleteMethod.Invoke(true);
        }
    }

}