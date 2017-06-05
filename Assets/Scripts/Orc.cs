using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {
	public enum Mode {
		GoToA,
		GoToB,
		Attack
		//...
	}
	
	public float speed = 1;
	RaycastHit2D hit;
	Animator animator;
	public Vector3 MoveBy;
	Vector3 to;
	Vector3 from;
	Rigidbody2D myBody = null;
	Mode mode;	
	int layer_id;
	Vector3 pointB;
	Vector3 pointA;
	Transform heroParent = null;
	Vector3 target;
	
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
		mode = Mode.GoToA;
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();	
	}

	float getDirection() {
		Vector3 my_pos = this.transform.position;
		
		if(mode == Mode.GoToA) {
			if(my_pos.x < pointA.x) {
				return 1;
			} else {
				return -1;
			}
		}
		else if(mode == Mode.GoToB) {
			if(my_pos.x < pointB.x) {
				return 1;
			} else {
				return -1;
			}
		}
		return 0;
	}

	public bool isArrived(Vector3 pos, Vector3 target) {
		return Mathf.Abs(pos.x - target.x) < 0.02f;
	}

	void run(){
		float value = getDirection();
		
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(value < 0) {
			sr.flipX = false;
		} else if(value > 0) {
			sr.flipX = true;
		}
		
		if(Mathf.Abs(value) > 0) {
			animator.SetBool("walk", true);
		} else {
			animator.SetBool("walk", false);
		}
	}
	
	void Update () {
		from = transform.position + Vector3.up * 0.3f;
		to = transform.position + Vector3.down * 0.1f;
		layer_id = 1 << LayerMask.NameToLayer("Ground");
		
		hit = Physics2D.Linecast(from, to, layer_id);
		run();

		Vector3 my_pos = this.transform.position;
		 
		if(mode == Mode.GoToA) {
			target = pointA;
		} else if(mode == Mode.GoToB) {
			target = pointB;
		}
	
		if(isArrived(my_pos,target)){
			if(mode == Mode.GoToA) {
				mode = Mode.GoToB;	
			} else if(mode == Mode.GoToB) {
				mode = Mode.GoToA;
			}
		}
		

		MovingPlatform.checkParentPlatform(this.transform,this.heroParent,hit);
	}
}
