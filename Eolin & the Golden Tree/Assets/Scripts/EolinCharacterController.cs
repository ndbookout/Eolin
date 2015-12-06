using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EolinCharacterController : MonoBehaviour 
{
	private Rigidbody2D eolinRigid;
	private Animator eolinAnim;

    public GameObject arrow;
    private GameObject loadedArrow;
    public float arrowSpeed;

	public float maxSpeed;
	public float speed;
    public float jumpSpeed;
	private bool isFlipped; //starts facing to left
    private bool isGrounded;
    private bool hasJumped;

    private bool isAiming;
    private bool goodForm;

    private int groundMask = 1 << 9;

	// Use this for initialization
	void Start() 
	{
		eolinRigid = GetComponent<Rigidbody2D>();
		eolinAnim = GetComponent<Animator>();

		isFlipped = false;
    }

    void Update()
    {
        AimControl();
    }
	
	// Update is called once per frame
	void FixedUpdate() 
	{
		MoveControl();
        CheckForGround();
	}

	void MoveControl()
	{
		if (Input.GetAxis("Horizontal") != 0 && !isAiming)
		{
			if (Input.GetAxis("Horizontal") > 0)
			{
				if (!isFlipped)
					Flip ();
			}
			else if (Input.GetAxis("Horizontal") < 0)
			{
				if (isFlipped)
					Flip();
			}

			if (eolinRigid.velocity.magnitude < maxSpeed)
			{
				eolinRigid.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
				eolinAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
			}
		}

        if (Input.GetAxis("Vertical") > 0 && isGrounded && !isAiming)
        {
            eolinRigid.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            eolinAnim.SetBool("Fall", true);

            hasJumped = true;
        }
	}

	void Flip()
	{
		Vector2 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;

		//if (loadedArrow != null) 
		//	loadedArrow.GetComponent<Arrow> ().Flip ();

		isFlipped = !isFlipped;
	}

    void CheckForGround()
    {
        bool ground = Physics2D.Raycast(eolinRigid.position, Vector2.down, 
            eolinRigid.transform.localScale.y/2, groundMask);
        //Debug.Log(Physics2D.Raycast(eolinRigid.position, Vector2.down, eolinRigid.transform.localScale.y / 2, groundMask).point);

        if (ground && !hasJumped)
        {
            isGrounded = true;
            eolinAnim.SetBool("Fall", false);
        }
        else
        {
            isGrounded = false;
            eolinAnim.SetBool("Fall", true);

            hasJumped = false;
        }
        //Debug.Log("Grounded? " + ground);
    }

    void AimControl()
    {    
        //Controls: D = Good Form, S = Load Arrows (good form only), A = Aim, W = Fire!
        if (Input.GetKeyDown(KeyCode.D))
        {
            goodForm = true;
            eolinAnim.SetBool("GoodForm", true);
            //Debug.Log("D");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            goodForm = false;
            eolinAnim.SetBool("GoodForm", false);

            if (loadedArrow != null && !isAiming)
            {
                //StartCoroutine(LoadArrow());
                Destroy(loadedArrow);
            }
            //Debug.Log("D-Up");
        }

        if (Input.GetKeyDown(KeyCode.S) && goodForm && loadedArrow == null)
        {          
            StartCoroutine(LoadArrow());
  
            //Debug.Log("S");    
        }

        if (Input.GetKey(KeyCode.A) && goodForm && loadedArrow != null)
        {
            goodForm = false;
            eolinAnim.SetBool("GoodForm", false);
            Debug.Log("Such good form!");
            eolinAnim.SetBool("Aim", true);
            isAiming = true;
            AdjustArrow();
        }
        //else if (Input.GetKey(KeyCode.A) && loadedArrow != null)
        //{
        //    Debug.Log("A with Arrow");
        //    isAiming = true;
        //    eolinAnim.SetBool("Aim", true);
        //}
        //else if (Input.GetKey(KeyCode.A) && loadedArrow == null)
        //{
        //    isAiming = true;
        //    eolinAnim.SetBool("Aim", true);
        //    StartCoroutine(LoadArrow());
        //    Debug.Log("A");
        //}
        //else if (Input.GetKeyUp(KeyCode.A) && goodForm)
        //{
        //    isAiming = false;
        //    eolinAnim.SetBool("Aim", false);
        //    goodForm = true;
        //    AdjustArrow();
        //    //Debug.Log("A-Up");
        //}
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isAiming = false;
            eolinAnim.SetBool("Aim", false);

            if (loadedArrow != null)
            {
                //StartCoroutine(LoadArrow());
                Destroy(loadedArrow);
            }
            //Debug.Log("A-Up");
        }

        if (Input.GetKeyDown(KeyCode.W) && isAiming && loadedArrow != null)
        {
            StartCoroutine(Fire());
        }

        Debug.Log(loadedArrow);
    }

    IEnumerator Fire()
    {
        //Fire!
        eolinAnim.SetBool("Fire", true);
        
        Debug.Log("Fire!");

        yield return new WaitForSeconds(0.5f);
        eolinAnim.SetBool("Fire", false);

		loadedArrow.GetComponent<Arrow> ().Fire ();
	
        loadedArrow = null;
        isAiming = false;
    }

    IEnumerator LoadArrow()
    {
        eolinAnim.SetBool("LoadArrow", true);
        yield return new WaitForSeconds(0.5f);
        eolinAnim.SetBool("LoadArrow", false);

        if (loadedArrow == null)
        {
   	       loadedArrow = Instantiate(arrow) as GameObject;
            loadedArrow.transform.parent = this.transform;
            AdjustArrow();
        }  
    }

    void AdjustArrow()
    {
        if (goodForm)
        {
            loadedArrow.transform.localPosition = new Vector2(-0.1f, -0.06f);
            loadedArrow.transform.localRotation = Quaternion.Euler(0, 0, 50);
        }
        else
        {
            loadedArrow.transform.localPosition = new Vector2(-0.1f, 0);
            loadedArrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
