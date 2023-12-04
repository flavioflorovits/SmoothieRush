using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour,IUnityAdsInitializationListener
{

    public string androidGameId;
    public string iosGameId;

    public bool isTestingMode;

    string gameId;

    private void Awake()
    {
        InitAds();
    }
    void InitAds()
    {


#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId;
#endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }

    }
    public void OnInitializationComplete()
    {
        Debug.Log("Ads initialized!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads failed to initialize");
    }

}
