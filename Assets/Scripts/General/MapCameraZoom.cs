// This script allows movement for the player to zoom the camera in and out
//
// Author: Robot and I Team
// Last modification date: 2-2-2022

using UnityEngine;
using Cinemachine;

public class MapCameraZoom : MonoBehaviour
{
    // Public variable that holds the camera
    public CinemachineVirtualCamera MapView;

    // Update is called once per frame
    // Update will check for mouse scroll, and will update accordingly
    void Update()
    {
        // Check if the scroll move forward (zoom in)
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && MapView.m_Lens.OrthographicSize > 1)
        {
            MapView.m_Lens.OrthographicSize -= 0.2f;
        }

        // Check if the scroll moved backward (zoom out)
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && MapView.m_Lens.OrthographicSize < 15)
        {
            MapView.m_Lens.OrthographicSize += 0.2f;
        }
    }
}