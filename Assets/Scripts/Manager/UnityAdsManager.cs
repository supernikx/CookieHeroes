using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
    public static UnityAdsManager instance;

    [Header("ADS Settings")]
    [SerializeField]
    private bool enableAds;
    [SerializeField]
    private string gameID;
    [SerializeField]
    private bool testMode;
    [SerializeField]
    private string rewardedVideoPlacementID;
    [SerializeField]
    private string regularPlacementID;

    private void Awake()
    {
        instance = this;
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowRegularAD(Action<ShowResult> _callback)
    {
        if (CheckCanShowAD())
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = _callback;
            Advertisement.Show(regularPlacementID, so);
        }
        else
        {
            _callback.Invoke(ShowResult.Skipped);
            Debug.Log("AD Not Ready");
        }
    }

    public void ShowRewardedAD(Action<ShowResult> _callback)
    {
        if (CheckCanShowAD())
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = _callback;
            Advertisement.Show(rewardedVideoPlacementID, so);
        }
        else
        {
            _callback.Invoke(ShowResult.Skipped);
            Debug.Log("AD Not Ready");
        }
    }

    public bool CheckCanShowAD()
    {
        return (enableAds && Advertisement.IsReady());
    }
}
