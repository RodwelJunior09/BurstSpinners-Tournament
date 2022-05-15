using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("music");

        if (musicObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals("LoseScreen") || sceneName.Contains("WinScreen"))
        {
            Destroy(this.gameObject);
        }
    }
}
