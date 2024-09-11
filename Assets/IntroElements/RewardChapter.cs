using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChapter : MonoBehaviour
{

    public void showad()
    {
        AdsManager.instance.ShowRewardedInterstitial(completed);
        void completed(bool complete)
        {
            if (complete)
            {
                gameObject.SetActive(false);
            }

        }

    }
    
}
