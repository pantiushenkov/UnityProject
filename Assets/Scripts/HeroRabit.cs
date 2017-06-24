using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroRabit : MonoBehaviour {

	public AudioClip jumpSound = null,walkSound = null, dieSound = null;
    AudioSource jumpSource = null,walkSource = null, dieSource = null;
	
	public static HeroRabit lastRabit = null;
	public float speed = 1;
	bool isGrounded = false,JumpActive=false,dieAnimation = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f,JumpSpeed=2f;
	Transform heroParent = null;
	RaycastHit2D hit;
	Animator animator;
	Vector3 to,from;
	static HeroRabit current;
	bool scaledTwice = false,isFalling = false,isDying = false;
	int layer_id;
	SpriteRenderer sr = null;
	
	public Rigidbody2D myBody = null;
	
	public bool isScaled(){
		return scaledTwice;
	}

	void Awake() {
		lastRabit = this;
	}
	
	void Start () {
		walkSource = gameObject.AddComponent<AudioSource> ();
		walkSource.clip = walkSound;
		dieSource = gameObject.AddComponent<AudioSource> ();
		dieSource.clip = dieSound;
		jumpSource = gameObject.AddComponent<AudioSource> ();
		jumpSource.clip = jumpSound;
		
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D>();
		LevelController.current.setStartPosition(transform.position);
		animator = GetComponent<Animator> ();	
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
			if(SoundManager.Instance.isSoundOn()) {
				walkSource.Play();
			}
		} else {
			animator.SetBool("run", false);
		}

		sr = GetComponent<SpriteRenderer>();
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
			if(SoundManager.Instance.isSoundOn()) {
				jumpSource.Play();
			}
			animator.SetBool("jump", true);
		}
		}

	void FixedUpdate () {
		if(SceneManager.GetActiveScene().name == "MainMenu") return;
	 	from = transform.position + Vector3.up * 0.3f;
		to = transform.position + Vector3.down * 0.1f;
		layer_id = 1 << LayerMask.NameToLayer("Ground");
		
		hit = Physics2D.Linecast(from, to, layer_id);
		
		if(hit) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		jump();
		run();
		
		
		MovingPlatform.checkParentPlatform(this.transform,this.heroParent,hit);

		if(dieAnimation){
			triggerDie();
		}
	}
	
	public void triggerDie(){
		if(!isDying){
			StartCoroutine(die());
		}
	}
	
	public void fall(){
		isFalling = true;
		triggerDie();
	}
	
	IEnumerator die(){
		if(isFalling || !scaledTwice){
			if(SoundManager.Instance.isSoundOn()) {
				dieSource.Play();
			}
			isDying = true;
			dieAnimation = true;
			if(!isFalling){
				animator.SetBool("die", true);
			}
			yield return new WaitForSeconds(1);
			animator.SetBool("die", false);
			sr.enabled = false;
			yield return new WaitForSeconds(0.5f);
			sr.enabled = true;
			LevelController.current.onRabitDeath(GetComponent<HeroRabit>());
			dieAnimation = false;
			isDying = false;
			isFalling = false;
			
		} else {
			this.scale(-1);
		}
	}
	
	public void scale(int scale){
			this.transform.localScale += new Vector3(scale,scale,scale);
			scaledTwice = !scaledTwice;
	}	
}
