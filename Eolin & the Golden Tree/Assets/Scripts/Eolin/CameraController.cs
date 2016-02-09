using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public static CameraController cam;

	void Start()
	{
		cam = this;
	}

	public void MoveCam(Vector3 newPosition)
	{
		Vector3 startPosition = transform.position;
		if (startPosition != newPosition)
			transform.position = newPosition;
	}
}
