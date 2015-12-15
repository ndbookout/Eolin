using UnityEngine;
using System.Collections;
using MinMax;

public class MoveGlobally : MonoBehaviour 
{
	public enum Axis
	{
		x,
		y,
		both
	}

	public Axis axis;
	public float speed;
	public Range range;

	private Transform objTrans;
	private bool moveDown = true;
	
	// Update is called once per frame
	void Update () 
	{
		if (axis == Axis.x)
        {
			if (objTrans.position.x >= range.max)
            {
				Switch ();
			} else if (objTrans.position.x <= range.min)
            {
				Switch ();
			}
		}
        else if (axis == Axis.y)
        {
			if (objTrans.position.y >= range.max)
            {
				Switch ();
			} else if (objTrans.position.y <= range.min)
            {
				Switch ();
			}
		} else if (axis == Axis.both)
        {
			Debug.Log ("Fuck you, you greedy bastard.");
		}

		Move ();
	}

	void Move()
	{
        //include Axis.x later
        if (moveDown) 
		{
			objTrans.position -= new Vector3 (0, -1, 0) * speed * Time.deltaTime;
		} 
		else 
		{
			objTrans.position -= new Vector3 (0, 1, 0) * speed * Time.deltaTime;
		}
	}

	void Switch()
	{
		moveDown = !moveDown;
	}
}
