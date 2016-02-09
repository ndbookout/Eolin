using UnityEngine;
using System.Collections;

public class ArrowKeysTimer : MonoBehaviour 
{
	public float time;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= time)
			gameObject.SetActive (false);
		else
			timer += Time.deltaTime;
	}
}
