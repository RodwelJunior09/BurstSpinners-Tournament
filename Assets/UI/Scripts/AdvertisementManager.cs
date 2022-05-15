using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_ANDROID
    string gameId = "4261519";
    string rewaredVideoId = "Rewarded_Android";
    string interAdsId = "Interstitial_Android";
#endif

    bool unlockSpinner = false;
    CharacterNavegation characterNavegation;

    private void OnEnable() {
        characterNavegation = FindObjectOfType<CharacterNavegation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    private void Update() {
        if (unlockSpinner)
        {
            unlockSpinner = false;
            UnlockSpinner();
        }
    }

    void UnlockSpinner(){
        int characterCount = characterNavegation.GetCharacterCount();
        PlayerPrefs.SetInt($"player_{characterCount + 1}_unlocked", 1);
    }

    public void ShowInterAds()
    {
        Advertisement.Show(interAdsId, this);
    }

    public void ShowRewaredAd(){
        Advertisement.Show(rewaredVideoId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad has started.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad is beeing click");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(rewaredVideoId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            unlockSpinner = true;
    }
}
