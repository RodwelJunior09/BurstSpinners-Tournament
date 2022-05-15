using UnityEngine;
using Cinemachine;
public class InicializeCamera : MonoBehaviour
{
    Player player;
    CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<Player>();
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.LookAt = player.transform;
    }
}
