using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;

using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{
    public delegate void RewarderVideoAdFailedHandle();
    public static event RewarderVideoAdFailedHandle OnRewarderVideoAdFailed;

    public delegate void InterstitialVideoAdFailedHandle();
    public static event InterstitialVideoAdFailedHandle OnInterstitialVideoAdFailed;

    public delegate void RewarderVideoAdSkippedHandle();
    public static event RewarderVideoAdSkippedHandle OnRewarderVideoAdSkipped;

    public delegate void InterstitialVideoAdSkippedHandle();
    public static event InterstitialVideoAdSkippedHandle OnInterstitialVideoAdSkipped;

    public delegate void RewarderVideoAdCompletedHandle();
    public static event RewarderVideoAdCompletedHandle OnRewarderVideoAdCompleted;

    public delegate void InterstitialVideoAdCompletedHandle();
    public static event InterstitialVideoAdCompletedHandle OnInterstitialVideoAdCompleted;


    [Header("Platform Settings")]
    public string playStoreGameId;
    public string appStoreGameId;
    public bool isTargetPlayStore = true;
    public bool isTestAds = false;

    [Header("Ads Placement Ids")]
    public string interstitialAdsPlacement;
    public string rewardedAdsPlacement;
    public string bannerAdsPlacement;
    public BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    public static UnityAdsManager Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Advertisement.Initialize(isTargetPlayStore ? playStoreGameId : appStoreGameId, isTestAds);
        Advertisement.AddListener(this);
        Advertisement.Banner.SetPosition(bannerPosition);
    }

    public void PlayInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAdsPlacement) == true)
            Advertisement.Show(interstitialAdsPlacement);

    }

    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady(rewardedAdsPlacement) == true)
            Advertisement.Show(rewardedAdsPlacement);

    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenReady());
    }
    private IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerAdsPlacement))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerAdsPlacement);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide(true);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("OnUnityAdsDidError " + message);
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                {
                    if (placementId == rewardedAdsPlacement)
                        OnRewarderVideoAdFailed?.Invoke();

                    if (placementId == interstitialAdsPlacement)
                        OnInterstitialVideoAdFailed?.Invoke();

                    break;
                }

            case ShowResult.Skipped:
                {
                    if (placementId == rewardedAdsPlacement)
                        OnRewarderVideoAdSkipped?.Invoke();

                    if (placementId == interstitialAdsPlacement)
                        OnInterstitialVideoAdSkipped?.Invoke();
                    break;
                }

            case ShowResult.Finished:
                {
                    if (placementId == rewardedAdsPlacement)
                        OnRewarderVideoAdCompleted?.Invoke();

                    if (placementId == interstitialAdsPlacement)
                        OnInterstitialVideoAdCompleted?.Invoke();
                    break;
                }
        }
    }
    public void OnUnityAdsDidStart(string placementId)
{
        Debug.Log("OnUnityAdsDidStart " + placementId);
    }
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("OnUnityAdsDidError " + placementId);
    }
}
