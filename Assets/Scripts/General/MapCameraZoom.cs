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
	public float speed = 10f;

    // Update is called once per frame
    // Update will check for mouse scroll, and will update accordingly
    private void Update()
    {
		CheckZoom();
		ProcessInputs();
    }
	
	private void CheckZoom()
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
	
	private void ProcessInputs()
	{
		// Get the current transform position
		Vector3 cameraPosition = transform.position;
		
		// Right movement
		if(Input.GetKey(KeyCode.RightArrow))
		{
			if (cameraPosition.x <= 1100)
			{
				transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
			}
		}
		
		// Left movement
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			if (cameraPosition.x >= 910)
			{
				transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
			}
		}

		// Up movement
		if(Input.GetKey(KeyCode.UpArrow))
		{
			if (cameraPosition.y <= 630)
			{
				transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
			}
		}
		
		// Down movement
		if(Input.GetKey(KeyCode.DownArrow))
		{
			if (cameraPosition.y >= 470)
			{
				transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
			}
		}
	}
}