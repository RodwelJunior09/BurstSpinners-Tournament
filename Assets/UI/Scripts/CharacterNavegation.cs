using UnityEngine;
using UnityEngine.UI;

public class CharacterNavegation : MonoBehaviour
{
    [SerializeField] Sprite[] allCharacters;
    
    [Header("Rendering Selection")]
    [SerializeField] GameObject characterRendered;
    [SerializeField] GameObject br_character_rendered;

    [Header("Battle Royale Team Renders")]
    [SerializeField] GameObject[] allTeamRenders;

    [Header("Panels")]
    [SerializeField] GameObject adsPanel;
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject br_blockPanel;

    [Header("Buttons")]
    [SerializeField] Button unlockBtn;
    [SerializeField] Button continueBtn;

    int count = 0;
    Image imageRenderer;
    int team_member = 0;
    Sprite nothingSprite;
    GameObject[] allBorderSelectors;

    private void OnEnable() {
        if (PlayerPrefs.GetInt("br_mode") == 1)
            RemoveAllBorder();
        InitPlayerUnlocker();
        adsPanel.gameObject.SetActive(false);   
        nothingSprite = allTeamRenders[0].GetComponentInChildren<Image>().sprite;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("br_mode") == 1)
            this.imageRenderer = br_character_rendered.GetComponent<Image>();
        else
            this.imageRenderer = characterRendered.GetComponent<Image>();
        imageRenderer.sprite = allCharacters[0];
        CheckIfUnlocked(count);
    }

    private void Update() {
        CheckIfUnlocked(count);
    }

    void RemoveAllBorder(){
        allBorderSelectors = GameObject.FindGameObjectsWithTag("Border_Selection");

        for (int i = 1; i < allBorderSelectors.Length; i++)
        {
            allBorderSelectors[i].gameObject.SetActive(false);
        }
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
        if (PlayerPrefs.GetInt("br_mode") == 1)
            br_blockPanel.gameObject.SetActive(false);
        else
            blockPanel.gameObject.SetActive(false);
        unlockBtn.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(true);
    }

    public void ShowBlockPanel(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
            br_blockPanel.gameObject.SetActive(true);
        else
            blockPanel.gameObject.SetActive(true);
        unlockBtn.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(false);
    }

    public void AddCharacterToTeam(){
        if (team_member < 3)
        {
            allBorderSelectors[team_member].SetActive(false);
            allTeamRenders[team_member].GetComponentInChildren<Image>().sprite = allCharacters[count];
            team_member++;
            if (team_member < allBorderSelectors.Length)
                allBorderSelectors[team_member].SetActive(true);
        }
    }

    public void RemoveCharacterFromTeam(){
        if (team_member > 0)
        {
            team_member--;
            allTeamRenders[team_member].GetComponentInChildren<Image>().sprite = nothingSprite;
            allBorderSelectors[team_member].SetActive(true);
            if (team_member < 2)
                allBorderSelectors[team_member + 1].SetActive(false);
        }
    }
}
