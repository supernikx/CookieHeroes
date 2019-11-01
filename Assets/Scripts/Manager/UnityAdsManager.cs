using System;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAdsManager : MonoBehaviour
{
    public static UnityAdsManager instance;

    [Header("ADS Settings")]
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
#if UNITY_ADS
        if (Advertisement.IsReady(regularPlacementID))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = _callback;
            Advertisement.Show(regularPlacementID, so);
        }
        else
            Debug.Log("AD Not Ready");
#endif
    }

    public void ShowRewardedAD(Action<ShowResult> _callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(rewardedVideoPlacementID))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = _callback;
            Advertisement.Show(rewardedVideoPlacementID, so);
        }
        else
            Debug.Log("AD Not Ready");
#endif
    }
}
