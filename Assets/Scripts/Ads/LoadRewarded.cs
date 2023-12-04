using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class LoadRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button adButton;

    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    string adUnitId = null;

    private void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif

        adButton.interactable = false;
    }

    public void LoadAd()
    {
        Debug.Log("Loading rewarded");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(adUnitId))
        {
            Debug.Log("Rewarded loaded");
            adButton.interactable = true;
            ShowAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Rewarded failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Rewarded failed to show");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded started to show");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded clicked");

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("Rewarded complete, do reward");
        }
    }

    public void ShowAd()
    {
        adButton.interactable = true;
        Advertisement.Show(adUnitId, this);
    }
}
