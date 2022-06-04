using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    [SerializeField] GameObject ads_br_panel;
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject br_blockPanel;

    [Header("Buttons")]
    [SerializeField] Button unlockBtn;
    [SerializeField] Button continueBtn;

    [Header("Button BR team")]
    [SerializeField] Button adUnlockerBrButton;
    [SerializeField] Button continueBrModeBtn;
    [SerializeField] Button addToTheTeamButton;

    int count = 0;
    Image imageRenderer;
    int team_member_count = 0;
    Sprite nothingSprite;
    int currentMemberId;
    SoundFxManager _soundFxManager;
    GameObject[] allBorderSelectors;
    List<int> teamMembersId = new List<int>();

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
        _soundFxManager = FindObjectOfType<SoundFxManager>();
    }

    private void Update() {
        HasAllTeamMembers();
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

    void HasAllTeamMembers(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            if (teamMembersId.Count() == 3)
            {
                if (addToTheTeamButton.gameObject.activeInHierarchy)
                    addToTheTeamButton.gameObject.SetActive(false);
                if (!continueBrModeBtn.gameObject.activeInHierarchy)
                    continueBrModeBtn.gameObject.SetActive(true);
            }
            else
            {
                if (!addToTheTeamButton.gameObject.activeInHierarchy)
                    addToTheTeamButton.gameObject.SetActive(true);
                if (continueBrModeBtn.gameObject.activeInHierarchy)
                    continueBrModeBtn.gameObject.SetActive(false);
            }
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

    public int GetCharacterCount(){
        return count;
    }

    // Show the ads panel
    public void ShowAdsPanel(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
            ads_br_panel.gameObject.SetActive(true);
        else
            adsPanel.gameObject.SetActive(true);
    }

    // Remove the ads panel
    public void GoBack(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
            ads_br_panel.gameObject.SetActive(false);
        else
            adsPanel.gameObject.SetActive(false);
    }

    public void DisableBlockPanel(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            br_blockPanel.gameObject.SetActive(false);
            adUnlockerBrButton.gameObject.SetActive(false);
        }
        else
        {
            blockPanel.gameObject.SetActive(false);
            unlockBtn.gameObject.SetActive(false);
        }
    }

    // Disable the add team and continue buttons, and enable ad unlock button
    public void ShowBlockPanel(){
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            br_blockPanel.gameObject.SetActive(true);
            adUnlockerBrButton.gameObject.SetActive(true);
        }
        else
        {
            blockPanel.gameObject.SetActive(true);
            unlockBtn.gameObject.SetActive(true);
        }
    }

    public void AddCharacterToTeam(){
        if (team_member_count < 3)
        {
            _soundFxManager.PlayAcceptSoundFx();
            allBorderSelectors[team_member_count].SetActive(false);
            allTeamRenders[team_member_count].GetComponentInChildren<Image>().sprite = allCharacters[count];
            team_member_count++;
            PlayerPrefs.SetInt($"team_member_{team_member_count}", count);
            teamMembersId.Add(count);
            if (team_member_count < allBorderSelectors.Length)
                allBorderSelectors[team_member_count].SetActive(true);
        }
    }

    public void RemoveCharacterFromTeam(){
        if (team_member_count > 0)
        {
            team_member_count--;
            _soundFxManager.PlayDeclineSoundFx();
            allTeamRenders[team_member_count].GetComponentInChildren<Image>().sprite = nothingSprite;
            allBorderSelectors[team_member_count].SetActive(true);
            PlayerPrefs.DeleteKey($"team_member_{team_member_count}");
            teamMembersId.Remove(count);
            if (team_member_count < 2)
                allBorderSelectors[team_member_count + 1].SetActive(false);
        }
    }
}
