using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBrown : MonoBehaviour {
	public enum Mode {
		GoToA,
		GoToB,
		Attack,
		Run,
		Idle,
		Die
	}
	
	public float speed = 1;
	float distanceToHitX = 1.3f;
	float distanceToHitScaleX = 1.6f;
	RaycastHit2D hit;
	Animator animator;
	public Vector3 MoveBy;
	Vector3 to;
	Vector3 from;
	Rigidbody2D myBody = null;
	Mode mode;	
	int layer_id;
	Vector3 my_pos;
	Vector3 pointB;
	Vector3 pointA;
	Transform heroParent = null;
	Vector3 target;
	float value,last_carrot = 0;
	Vector3 rabit_pos;
	public GameObject prefabCarrot;
	
	void Start () {
		this.pointA = this.transform.position - MoveBy;
		this.pointB = this.pointA + 3 * MoveBy;
		mode = Mode.GoToB;
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();	
	}

	float getDirection() {
		if(mode == Mode.Attack) {
			if(my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
		} else if (mode == Mode.GoToA){
             return -1; 
        }
        else if (mode == Mode.GoToB){
            return 1; 
        }
		return 0;
	}

	public bool isArrived(Vector3 pos) {
		return (mode == Mode.GoToA && pos.x <= pointA.x) || (mode == Mode.GoToB && pos.x >= pointB.x);
	}

	IEnumerator die(float duration) {
		mode = Mode.Die;
		yield return new WaitForSeconds (duration);
		Destroy(this.gameObject);
	}

	void move(){
		if(mode != Mode.Die ){
			if(Mathf.Abs(value) > 0 && mode != Mode.Attack ) {
				animator.SetBool("walk", true);
			} else {
				animator.SetBool("walk", false);
			}
			
			if(mode == Mode.Run) {
				animator.SetBool("run", true);
			} else {
				animator.SetBool("run", false);
			}

			if(mode == Mode.Idle) {
				animator.SetBool("idle", true);
			} else {
				animator.SetBool("idle", false);
			}
		}
	}
	
	void launchCarrot(float direction){		
		if(Time.time - last_carrot > 2.0f) {
			last_carrot = Time.time;
			GameObject obj = GameObject.Instantiate(this.prefabCarrot);
			obj.transform.position = this.transform.position;
			Carrot carrot = obj.GetComponent<Carrot> ();
			carrot.launch(direction);
			animator.SetTrigger("attack1");
		}
	}

	void run(){	
		if(Mathf.Abs(rabit_pos.x - my_pos.x) < 5.0f) {
			this.launchCarrot(rabit_pos.x - my_pos.x);
		mode = Mode.Attack;
		} else if(value < 0){
			mode = Mode.GoToA;
		} else {
			mode = Mode.GoToB;
		}
		
		float distanceX = Mathf.Abs(rabit_pos.x - my_pos.x);
		float distanceY = Mathf.Abs(rabit_pos.y - my_pos.y);
		float distanceToHit = HeroRabit.lastRabit.isScaled() ? distanceToHitScaleX : distanceToHitX;

		if(distanceX < distanceToHit && distanceY > 1.3f && distanceY < 2){
			animator.SetTrigger("die");
			StartCoroutine(die(1));	
		}
	}

	void Update () {
		rabit_pos = HeroRabit.lastRabit.transform.position;
		my_pos = this.transform.position;
	
		if(isArrived(my_pos)){
			if(mode == Mode.GoToA) {
				mode = Mode.GoToB;	
			} else if(mode == Mode.GoToB) {
				mode = Mode.GoToA;
			}
		}

		value = getDirection();
		
		if (Mathf.Abs(value) > 0 && mode != Mode.Attack) {
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
		from = transform.position + Vector3.up * 0.3f;
		to = transform.position + Vector3.down * 0.1f;
		layer_id = 1 << LayerMask.NameToLayer("Ground");
		
		hit = Physics2D.Linecast(from, to, layer_id);

		move();
		run();
		MovingPlatform.checkParentPlatform(this.transform,this.heroParent,hit);
	}
}
