using TMPro;
using UnityEngine;

public class MsgHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI celebrationText;

    private void OnEnable() {
        if(PlayerPrefs.GetInt("br_mode") == 1){
            celebrationText.text = "Congratulations! Your team managed to win";
        }    
    }
}
