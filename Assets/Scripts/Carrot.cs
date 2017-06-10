using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Bombs {
	public float speed; 

	IEnumerator destroyLater() {
		yield return new WaitForSeconds (3.0f);
		Destroy (this.gameObject);
	}

	public void launch(float direction){
		Rigidbody2D myBody = this.GetComponent<Rigidbody2D> ();		
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector2 vel = myBody.velocity;
		vel.x = direction * speed;	
		myBody.velocity = vel;	
		
		sr.flipX = direction > 0 ? false: true;	 
		this.transform.position += new Vector3 (direction > 0 ? 1:-1 , 0.5f, 0);
	}

	void Start() {
		StartCoroutine(destroyLater());
	}
	
}