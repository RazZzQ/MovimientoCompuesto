using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeVista : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    public CinemachineVirtualCamera virtualCamera3D;
    public CinemachineVirtualCamera virtualCamera2D;

    private bool is3DView = true;

    private void Start()
    {
        // Inicia con la vista 3D activada
        virtualCamera3D.Priority = 10;
        virtualCamera2D.Priority = 0;
        is3DView = true;
    }

    public void CambiarVista()
    {
        is3DView = !is3DView;

        // Cambia la prioridad de las cámaras virtuales según la vista
        virtualCamera3D.Priority = is3DView ? 10 : 0;
        virtualCamera2D.Priority = is3DView ? 0 : 10;

        // Reinicia Cinemachine para aplicar los cambios de prioridad
        cinemachineBrain.enabled = false;
        cinemachineBrain.enabled = true;
    }
}
