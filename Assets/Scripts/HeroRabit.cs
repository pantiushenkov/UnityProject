using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

	// Use this for initialization
	public float speed = 1;bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	Transform heroParent = null;
	RaycastHit2D hit;
	Animator animator;
	Vector3 to;
	bool dieAnimation = false;
	public static HeroRabit current;
	static bool scaledTwice = false;
	Vector3 from;
	int layer_id;

	Rigidbody2D myBody = null;
	// Use this for initialization
	void Start () {
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D>();
		LevelController.current.setStartPosition(transform.position);
	}

	void run(){
		float value = Input.GetAxis("Horizontal");
			
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		if(this.isGrounded && Mathf.Abs(value) > 0) {
			animator.SetBool("run", true);
		} else {
			animator.SetBool("run", false);
		}

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}

	}

	void jump(){
		
		if(hit) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
		//Намалювати лінію (для розробника)
		Debug.DrawLine (from, to, Color.red);

		if(Input.GetButtonDown("Jump") && isGrounded) {
			this.JumpActive = true;
		}
		if(this.JumpActive) {
		//Якщо кнопку ще тримають
		if(Input.GetButton("Jump")) {
			this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}
		if(this.isGrounded) {
			animator.SetBool("jump", false);
		} else {
			animator.SetBool("jump", true);
		}
		checkParentPlatform(from, to, layer_id);
	}
	// Update is called once per frame
	void FixedUpdate () {
	 	animator = GetComponent<Animator> ();	
		from = transform.position + Vector3.up * 0.3f;
		to = transform.position + Vector3.down * 0.1f;
		layer_id = 1 << LayerMask.NameToLayer("Ground");
		
		hit = Physics2D.Linecast(from, to, layer_id);
		
		if(hit) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		run();
		jump();
		
		if(dieAnimation){
			die();
		}
	}
	float time_to_wait = 1;
	
	public void die(){
		if(!scaledTwice){
			dieAnimation = true;
			if(this.isGrounded) {
				Debug.Log("die is grounded");
				animator.SetBool("die", true);
				time_to_wait -= Time.deltaTime;
				Debug.Log(time_to_wait);
				if(time_to_wait <= 0) {
					Debug.Log("die finished");
					LevelController.current.onRabitDeath(GetComponent<HeroRabit>());
					animator.SetBool("die", false);
					dieAnimation = false;
				}
			}
		} else {
			this.scale(-1);
		}
	}
	
	public void scale(int scale){
			this.transform.localScale += new Vector3(scale,scale,scale);
			scaledTwice = !scaledTwice;
	}

	void checkParentPlatform(Vector3 from,Vector3 to,int layer_id){
	
		if(hit) {
		//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null){
			//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			//Ми в повітрі відліпаємо під платформи
			SetNewParent(this.transform, this.heroParent);
		}
	}
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}
}
