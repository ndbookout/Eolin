using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour 
{
	public Vector3 camPosition;

	void OnTriggerEnter2D(Collider2D collide)
	{
		if (collide.tag == "Player") 
			CameraController.cam.MoveCam (camPosition);	
	}
}
