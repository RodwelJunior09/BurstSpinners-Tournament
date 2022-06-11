using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool isPause;

    public void SetSurvivorMode() {
        if (!PlayerPrefs.HasKey("survivor_mode"))
            PlayerPrefs.SetInt("survivor_mode", 1);
    }

    public void SetBRMode(){
        if (!PlayerPrefs.HasKey("br_mode"))
            PlayerPrefs.SetInt("br_mode", 1);
    }

    public void SetTournamentMode() {
        if (!PlayerPrefs.HasKey("tournament_mode"))
            PlayerPrefs.SetInt("tournament_mode", 1);
    }

    public IEnumerator WinSurvivalMode(){
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SurvivalWinScreen");
        DeleteMatchData();
    }

    public IEnumerator LoadWinScreen()
    {
        switch (PlayerPrefs.GetInt("tournamentId"))
        {
            case 1:
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("WinScreen#1");
                break;
            case 2:
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("WinScreen#2");
                break;
            case 3:
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("WinScreen#3");
                break;
            case 4:
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("WinScreen#4");
                break;
            default:
                break;
        }
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadHomePage()
    {
        if (Time.timeScale == 0)
            ResumeGame();
        DeleteMatchData();
        SceneManager.LoadScene("HomePage");
    }

    void DeleteMatchData(){
        PlayerPrefs.DeleteKey("enemyId");
        PlayerPrefs.DeleteKey("br_mode");
        PlayerPrefs.DeleteKey("tournamentId");
        PlayerPrefs.DeleteKey("survivor_mode");
        PlayerPrefs.DeleteKey("tournament_mode");
        PlayerPrefs.DeleteKey("player_won_match");
        PlayerPrefs.DeleteKey("rounds_won_by_enemy");
        PlayerPrefs.DeleteKey("rounds_won_by_player");
    }

    public void DeleteRoundData()
    {
        PlayerPrefs.DeleteKey("rounds_won_by_player");
        PlayerPrefs.DeleteKey("rounds_won_by_enemy");
    }

    public void DeleteEnemyId(){
        PlayerPrefs.DeleteKey("enemyId");
    }
    
    public void LoadTournament()
    {
        StartCoroutine(TournamentManager());
    }

    public IEnumerator TournamentManager()
    {
        switch (PlayerPrefs.GetInt("tournamentId"))
        {
            case 1:
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Tournament#1");
                break;
            case 2:
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Tournament#2");
                break;
            case 3:
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Tournament#3");
                break;
            case 4:
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Tournament#4");
                break;
            default:
                break;
        }
    }

    public void LoadPlayerSelectScreen() => SceneManager.LoadScene("SpinnerSelect");

    public IEnumerator PlayerLoseScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LoseScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGamePause() => isPause;

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
    }
}
