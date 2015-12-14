using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D arrowRigid;
    public float arrowSpeed;
    private bool isFlipped; //starts facing left

    void Start()
    {
        arrowRigid = GetComponent<Rigidbody2D>();
        isFlipped = false;
    }

    void OnCollisionEnter(Collision collide)
    {
        transform.parent = collide.transform;
        arrowRigid.velocity = Vector2.zero;
        arrowRigid.gravityScale = 0;
    }

    public void Fire()
    {
        if (EolinCharacterController.Eolin.isFlipped)
            arrowRigid.velocity = new Vector2(arrowSpeed, 0);
        else
            arrowRigid.velocity = new Vector2(-arrowSpeed, 0);

        new Task(DestroyArrow());
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

	public void Flip()
	{
		Vector2 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
    }
}
