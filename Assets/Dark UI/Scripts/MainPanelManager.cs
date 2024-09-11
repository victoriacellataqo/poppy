using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
namespace Michsky.UI.Dark
{
    public class MainPanelManager : MonoBehaviour
    {
        [Header("PANEL LIST")]
        public List<GameObject> panels = new List<GameObject>();

        [Header("RESOURCES")]
        public BlurManager homeBlurManager;

        [Header("SETTINGS")]
        public int currentPanelIndex = 0;
        public bool enableBrushAnimation = true;
        public bool enableHomeBlur = true;
         
        private GameObject currentPanel;
        private GameObject nextPanel;
        private Animator currentPanelAnimator;
        private Animator nextPanelAnimator;

        string panelFadeIn = "Panel In";
        string panelFadeOut = "Panel Out";

        PanelBrushManager currentBrush;
        PanelBrushManager nextBrush;

        void Start()
        {
            AdsManager.instance.create();

            AdsManager.instance.InitializeNative(Yodo1U3dNativeAdPosition.NativeLeft | Yodo1U3dNativeAdPosition.NativeVerticalCenter);
            AdsManager.instance.ShowBanner();


            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);

            if (enableHomeBlur == true)
                homeBlurManager.BlurInAnim();
        }

        public void OpenFirstTab()
        {
            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);

            if (enableHomeBlur == true)
                homeBlurManager.BlurInAnim();
        }

        public void PanelAnim(int newPanel)
        {
            AdsManager.instance.HideNative(); 
            if(newPanel == 1)
if(AdsManager.instance!=null)            AdsManager.instance.ShowInterstitial(); 
            if (newPanel != currentPanelIndex)
            {
                currentPanel = panels[currentPanelIndex];

                currentPanelIndex = newPanel;
                nextPanel = panels[currentPanelIndex];

                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                currentPanelAnimator.Play(panelFadeOut);
                nextPanelAnimator.Play(panelFadeIn);

                if (enableBrushAnimation == true)
                {
                    currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                    if (currentBrush.brushAnimator != null)
                        currentBrush.BrushSplashOut();
                    nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                    if (nextBrush.brushAnimator != null)
                        nextBrush.BrushSplashIn();
                }

                if (currentPanelIndex == 0 && enableHomeBlur == true)
                    homeBlurManager.BlurInAnim();
                else if (currentPanelIndex != 0 && enableHomeBlur == true)
                    homeBlurManager.BlurOutAnim();
            }
        }

        public void NextPage()
        {
            if (currentPanelIndex <= panels.Count - 2)
            {
                currentPanel = panels[currentPanelIndex];
                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                currentPanelAnimator.Play(panelFadeOut);

                currentPanelIndex += 1;
                nextPanel = panels[currentPanelIndex];

                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                nextPanelAnimator.Play(panelFadeIn);

                if (enableBrushAnimation == true)
                {
                    currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                    if (currentBrush.brushAnimator != null)
                        currentBrush.BrushSplashOut();
                    nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                    if (nextBrush.brushAnimator != null)
                        nextBrush.BrushSplashIn();
                }

                if (currentPanelIndex == 0 && enableHomeBlur == true)
                    homeBlurManager.BlurInAnim();
                else if (currentPanelIndex != 0 && enableHomeBlur == true)
                    homeBlurManager.BlurOutAnim();
            }
        }

        public void PrevPage()
        {
            if (currentPanelIndex >= 1)
            {
                currentPanel = panels[currentPanelIndex];
                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                currentPanelAnimator.Play(panelFadeOut);

                currentPanelIndex -= 1;
                nextPanel = panels[currentPanelIndex];

                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                nextPanelAnimator.Play(panelFadeIn);

                if (enableBrushAnimation == true)
                {
                    currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                    if (currentBrush.brushAnimator != null)
                        currentBrush.BrushSplashOut();
                    nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                    if (nextBrush.brushAnimator != null)
                        nextBrush.BrushSplashIn();
                }

                if (currentPanelIndex == 0 && enableHomeBlur == true)
                    homeBlurManager.BlurInAnim();
                else if (currentPanelIndex != 0 && enableHomeBlur == true)
                    homeBlurManager.BlurOutAnim();
            }
        }
    }
}