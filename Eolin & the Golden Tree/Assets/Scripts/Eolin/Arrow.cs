using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D arrowRigid;
    private SpriteRenderer arrowRender;
    public float arrowSpeed;
	private bool isFlipped; //starts facing left
	public bool isFired; 

    void Start()
    {
        arrowRigid = GetComponent<Rigidbody2D>();
        arrowRender = GetComponent<SpriteRenderer>();
		isFlipped = false;
		isFired = false;
    }

	void FixedUpdate()
	{
		if (!isFired)
			AdjustArrow (EolinCharacterController.Eolin.goodForm, EolinCharacterController.Eolin.isFlipped);
	}

    void OnCollisionEnter2D(Collision2D collide)
    {
        transform.parent = collide.transform;
        arrowRigid.velocity = Vector2.zero;
        arrowRigid.gravityScale = 0;
    }

    public void Fire()
    {
		isFired = true;
		//arrowRigid.gravityScale = 0.5f;

		if (EolinCharacterController.Eolin.isFlipped)
            arrowRigid.velocity = new Vector2(arrowSpeed, 0);
        else
            arrowRigid.velocity = new Vector2(-arrowSpeed, 0);

        new Task(DestroyArrow());
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    public void AdjustArrow(bool rotated, bool flipped)
    {
        if (rotated)
        {
            if (flipped)
            {
                transform.localPosition = new Vector2(0.1f, -0.06f);
                transform.rotation = Quaternion.Euler(0, 0, -50);
            }                
            else
            {
                transform.localPosition = new Vector2(-0.1f, -0.06f);
                transform.rotation = Quaternion.Euler(0, 0, 50);
            }               
        }
        else
        {            
            transform.localRotation = Quaternion.Euler(0, 0, 90);

			if (flipped)
            {
                transform.localPosition = new Vector2(0.1f, 0);

				if(!isFlipped)
					Flip ();
            }    
			else if (!flipped)
            {
                transform.localPosition = new Vector2(-0.1f, 0);	

				if(isFlipped)
					Flip ();
            }               
        }
    }

	void Flip()
	{
		Vector3 scaler = transform.localScale;
		scaler *= -1;
		transform.localScale = scaler;
		isFlipped = !isFlipped;
	}
}
