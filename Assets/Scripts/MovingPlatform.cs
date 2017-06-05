using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	// Use this for initialization
	public Vector3 MoveBy;
	public float speed;
	Vector3 pointB;
	Vector3 pointA;
	public bool going_to_a;
	public Vector3 target;
	float time_to_wait;
	bool wait = false;
	
	// Use this for initialization
	
	void Start () {
		this.time_to_wait = 2;
		this.wait = false;
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;		
	}	
	
	public bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}

	void Update(){
		if(wait){
			time_to_wait -= Time.deltaTime;
			if(time_to_wait <= 0) {
				wait = false;
			}
		} else {
		Vector3 my_pos = this.transform.position;

		if(going_to_a) {
			target = this.pointA;
		} else {
			target = this.pointB;
		}
		
		Vector3 destination = target - my_pos;
		destination.z = 0;

		if(isArrived(my_pos,target)){
			going_to_a = !going_to_a;
			wait = true;
		}
		
		my_pos += destination * speed;  
		this.transform.position = my_pos;
		}
	}

	public static void checkParentPlatform(Transform transform,Transform parent,RaycastHit2D hit){
	
		if(hit) {
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null){
				SetNewParent(transform, hit.transform);
			}
		} else {
			SetNewParent(transform, parent);
		}
	}
	
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
}
