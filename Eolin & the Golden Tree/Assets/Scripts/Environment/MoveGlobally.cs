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
	public Vector3 startPosition;
	public Vector3 endPosition;

	private Transform objTrans;
	private bool moveDown;

    void Start()
    {
        objTrans = this.transform;
		moveDown = false;
    }

	void Update () 
	{
		Move ();
		CheckLimits ();
	}

	void Move()
	{
        //include Axis.x later
		if (moveDown) 
		{
			objTrans.position += new Vector3 (0, -1, 0) * speed * Time.deltaTime;
			//Debug.Log ("Down");
		} 
		else if (!moveDown)
		{
			objTrans.position += new Vector3 (0, 1, 0) * speed * Time.deltaTime;
			//Debug.Log ("Up");
		}
	}

	void CheckLimits()
	{
		if (axis == Axis.x)
		{
			if (objTrans.position.x >= endPosition.x)
			{
				Switch ();
			} else if (objTrans.position.x <= startPosition.x)
			{
				Switch ();
			}
		}
		else if (axis == Axis.y)
		{
			if (objTrans.position.y >= endPosition.y && !moveDown)
			{
				Switch ();
				//Debug.Log ("Switch");
			} 
			else if (objTrans.position.y <= startPosition.y && moveDown)
			{
				Switch ();
				//Debug.Log ("Switch2");
			}
			//Debug.Log ("Y");
		} else if (axis == Axis.both)
		{
			//not implemented
		}

		//Debug.Log (objTrans.position.y);
	}

	void Switch()
	{
		moveDown = !moveDown;
		//Debug.Log ("Move down: " + moveDown);
	}
}
