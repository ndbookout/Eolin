using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour
{
    public float floatSpeed;

    private Transform platformTrans;

	// Use this for initialization
	void Start ()
    {
        platformTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
