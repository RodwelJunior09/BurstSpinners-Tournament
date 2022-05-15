using UnityEngine;
using UnityEngine.UI;

public class CharacterNavegation : MonoBehaviour
{
    [SerializeField] Sprite[] allCharacters;
    [SerializeField] GameObject characterRendered;
    
    [Header("Panels")]
    [SerializeField] GameObject adsPanel;
    [SerializeField] GameObject blockPanel;

    [Header("Buttons")]
    [SerializeField] Button unlockBtn;
    [SerializeField] Button continueBtn;

    int count = 0;
    Image imageRenderer;

    private void OnEnable() {
        InitPlayerUnlocker();
        adsPanel.gameObject.SetActive(false);   
    }

    private void Start()
    {
        this.imageRenderer = characterRendered.GetComponent<Image>();
        imageRenderer.sprite = allCharacters[0];
        CheckIfUnlocked(count);
    }

    private void Update() {
        CheckIfUnlocked(count);
    }

    public void NextCharacterButton()
    {
        count++;
        if (count < allCharacters.Length)
            imageRenderer.sprite = allCharacters[count];
        else
        {
            count = 0;
            imageRenderer.sprite = allCharacters[count];
        }
    }

    public void PreviousCharacter()
    {
        count--;
        if (count >= 0)
            imageRenderer.sprite = allCharacters[count];
        else
        {
            count = allCharacters.Length - 1;
            imageRenderer.sprite = allCharacters[count];
        }
    }

    void CheckIfUnlocked(int count){
        int isUnlocked = PlayerPrefs.GetInt($"player_{count + 1}_unlocked");
        if (isUnlocked == 1)
            DisableBlockPanel();
        else
            ShowBlockPanel();
    }   

    void InitPlayerUnlocker(){
        PlayerPrefs.SetInt("player_1_unlocked", 1);
        PlayerPrefs.SetInt("player_2_unlocked", 1);
        PlayerPrefs.SetInt("player_3_unlocked", 1);
        PlayerPrefs.SetInt("player_4_unlocked", 1);
        PlayerPrefs.SetInt("player_5_unlocked", 1);
        PlayerPrefs.SetInt("player_6_unlocked", 1);
        PlayerPrefs.SetInt("player_7_unlocked", 1);
    }

    public void SavePlayerChoice()
    {
        PlayerPrefs.SetInt("playerId", count);
        PlayerPrefs.SetInt("enemyId", 0);
    }

    // Use this function for the win tournament, and for loading the correct scene.
    public void SaveTournamentId()
    {
        if (!PlayerPrefs.HasKey("tourmamentId"))
            PlayerPrefs.SetInt("tourmamentId", count + 1);
    }

    public int GetCharacterCount(){
        return count;
    }

    // Show the ads panel
    public void ShowAdsPanel(){
        adsPanel.gameObject.SetActive(true);
    }

    // Remove the ads panel
    public void GoBack(){
        adsPanel.gameObject.SetActive(false);
    }

    public void DisableBlockPanel(){
        blockPanel.gameObject.SetActive(false);
        unlockBtn.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(true);
    }

    public void ShowBlockPanel(){
        blockPanel.gameObject.SetActive(true);
        unlockBtn.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(false);
    }
}
