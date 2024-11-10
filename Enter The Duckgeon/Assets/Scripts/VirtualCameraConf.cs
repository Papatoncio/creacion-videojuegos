using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraConf : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Encuentra al jugador y la cámara virtual
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Si existen ambos, establece el follow en el transform del jugador
        if (player != null && virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("Player o CinemachineVirtualCamera no encontrados");
        }
    }
}
