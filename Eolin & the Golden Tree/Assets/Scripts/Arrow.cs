using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D arrowRigid;
    public float arrowSpeed;

    void Start()
    {
        arrowRigid = GetComponent<Rigidbody2D>();
        //Fire();
    }

    void OnCollisionEnter(Collision collide)
    {
        transform.parent = collide.transform;
        arrowRigid.velocity = Vector2.zero;
        arrowRigid.gravityScale = 0;
    }

    public void Fire()
    {
        arrowRigid.velocity = new Vector2(-arrowSpeed, 0);
    }

	public void Flip()
	{
		Vector2 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}
}
