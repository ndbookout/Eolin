using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//For bow tutorial
public class TutorialTrigger : MonoBehaviour 
{
	private bool tutorialFinished;
	private bool tutorialActive;
	private bool pressedD;
	private bool pressedS;
	private bool pressedA;
	private bool pressedW;

	public GameObject drawBow;
	private Image drawIcon;
	public GameObject setArrow;
	private Image setIcon;
	public GameObject aimBow;
	private Image aimIcon;
	public GameObject releaseArrow;
	private Image releaseIcon;
	public GameObject and;

	public Sprite openKey;
	public Sprite pressedKey;

	private Task holdKeyTask;
	private Task pressKeyTask;

	void Start()
	{
		drawIcon = drawBow.GetComponentInChildren<Image> ();
		setIcon = setArrow.GetComponentInChildren<Image> ();
		aimIcon = aimBow.GetComponentInChildren<Image> ();
		releaseIcon = releaseArrow.GetComponentInChildren<Image> ();
	}

	void OnTriggerEnter2D(Collider2D collide)
	{
		if (!tutorialFinished) 
		{
			if (collide.gameObject.tag == "Player")
				StartTutorial ();		
		}
	}

	void OnTriggerExit2D(Collider2D collide)
	{
		if (!tutorialFinished) {
			if (collide.gameObject.tag == "Player") {
				ResetBools ();
				SetInactive ();
			}
		}
	}

	void StartTutorial()
	{
		tutorialActive = true;
		drawBow.SetActive (true);
		holdKeyTask = new Task (HoldKey (drawIcon));
	}

	void SetArrow()
	{
		if (holdKeyTask.Running)
			holdKeyTask.Stop();

		drawIcon.sprite = pressedKey;
		and.SetActive (true);
		setArrow.SetActive (true);
		pressKeyTask = new Task (PressKey (setIcon));
	}

	void Aim()
	{
		if (pressKeyTask.Running)
			pressKeyTask.Stop();
		
		setArrow.SetActive (false);
		aimBow.SetActive (true);
		holdKeyTask = new Task (HoldKey (aimIcon));
	}

	void Release()
	{
		if (holdKeyTask.Running)
			holdKeyTask.Stop();

		drawBow.SetActive (false);
		releaseArrow.SetActive (true);
		pressKeyTask = new Task (PressKey (releaseIcon));
	}

	void EndTutorial()
	{
		if (pressKeyTask.Running)
			pressKeyTask.Stop();

		and.SetActive (false);
		aimBow.SetActive (false);
		releaseArrow.SetActive (false);
		tutorialActive = false;
		tutorialFinished = true;
	}

	IEnumerator HoldKey(Image key)
	{
		yield return new WaitForSeconds (1f);
		key.sprite = pressedKey;
		yield return new WaitForSeconds (2f);
		key.sprite = openKey;
	}

	IEnumerator PressKey(Image key)
	{
		yield return new WaitForSeconds (1f);
		key.sprite = pressedKey;
		yield return new WaitForSeconds (0.5f);
		key.sprite = openKey;
	}

	void Update()
	{
		if (!tutorialFinished)
			DetectInputs ();

		if (tutorialActive) {
			if (!pressedD) {
				if (!holdKeyTask.Running)
					holdKeyTask = new Task (HoldKey (drawIcon));
			}
			if (pressedD && !pressedS) {
				if (!pressKeyTask.Running)
					pressKeyTask = new Task (PressKey (setIcon));	
			}
			if (pressedS && !pressedA) {
				Debug.Log ("S");
				if (!holdKeyTask.Running)
					holdKeyTask = new Task (HoldKey (aimIcon));
			}
			if (pressedA) {
				if (!pressKeyTask.Running)
					pressKeyTask = new Task (PressKey (releaseIcon));
			}
		}
	}

	void DetectInputs()
	{
		if (Input.GetKeyDown (KeyCode.D)) {
			pressedD = true;
			SetArrow ();
		} else if (pressedD && Input.GetKeyDown (KeyCode.S)) {
			pressedS = true;
			Aim ();
		} else if (pressedD && pressedS && Input.GetKeyDown (KeyCode.A)) {
			pressedA = true;
			Release ();
		} else if (pressedD && pressedS && pressedA && Input.GetKeyDown (KeyCode.W)) {
			pressedW = true;
			EndTutorial ();
		}
			
		if (Input.GetKeyUp (KeyCode.D) && !pressedA)
			ResetBools ();
		else if (Input.GetKeyUp (KeyCode.A) && !pressedW) 
			ResetBools ();
	}

	void ResetBools()
	{
		tutorialActive = false;
		pressedD = false;
		pressedS = false;
		pressedA = false;
	}

	void SetInactive()
	{
		drawBow.SetActive (false);
		setArrow.SetActive (false);
		aimBow.SetActive (false);
		releaseArrow.SetActive (false);
		and.SetActive (false);
	}
}
