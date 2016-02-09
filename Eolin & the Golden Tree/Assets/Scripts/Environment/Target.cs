using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
	public ParticleSystem stars;
	public Material starMat;
	public Texture starSprite;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.layer == 11) //Arrow Layer
			Response();
	}

	void Response()
	{
		starMat.mainTexture = starSprite;
		stars.Play ();
	}
}
