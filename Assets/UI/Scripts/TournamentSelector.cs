using UnityEngine;
using UnityEngine.UI;

public class TournamentSelector : MonoBehaviour
{
    int count = 0;
    [SerializeField] Image imageRender;
    [SerializeField] Sprite[] _allTournamentSprites;

    void Awake(){
        if (PlayerPrefs.HasKey("tournamentId")){
            PlayerPrefs.DeleteKey("tournamentId");
        }
    }

    void Start(){
        imageRender.sprite = _allTournamentSprites[0];
    }

    public void SaveTournamentId()
    {
        if (!PlayerPrefs.HasKey("tournamentId"))
            PlayerPrefs.SetInt("tournamentId", count + 1);
        
        Debug.Log(PlayerPrefs.GetInt("tournamentId"));
    }

    public void NextCharacterButton()
    {
        count++;
        if (count < _allTournamentSprites.Length)
            imageRender.sprite = _allTournamentSprites[count];
        else
        {
            count = 0;
            imageRender.sprite = _allTournamentSprites[count];
        }
    }

    public void PreviousCharacter()
    {
        count--;
        if (count >= 0)
            imageRender.sprite = _allTournamentSprites[count];
        else
        {
            count = _allTournamentSprites.Length - 1;
            imageRender.sprite = _allTournamentSprites[count];
        }
    }
}
