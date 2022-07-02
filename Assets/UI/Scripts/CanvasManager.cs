using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject br_canvas;
    [SerializeField] GameObject nr_canvas;

    LevelManager lvlManager;

    private void OnEnable() {
        if (PlayerPrefs.GetInt("br_mode") == 1)
        {
            nr_canvas.gameObject.SetActive(false);
            br_canvas.gameObject.SetActive(true);
        }
        else {
            nr_canvas.gameObject.SetActive(true);
            br_canvas.gameObject.SetActive(false);
        }
    }
}
