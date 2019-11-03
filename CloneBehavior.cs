using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : MonoBehaviour {

	public int moveSpeed;

	private Rigidbody2D rgBody;
	private bool isWalking;
	private int directionNum;
	private float idleTime;
	private float walkTime;
	private Animator anim;
	private Vector3 moveVector;
	
	// Use this for initialization
	void Start () {
		idleTime = Random.Range(0.5f,1.2f);
		isWalking = false;
		anim = GetComponent<Animator>();
		rgBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWalking == false) {
			idleTime -= Time.deltaTime;

			if (idleTime <= 0) {
				walkTime = Random.Range(0.5f,0.8f);
				moveVector = GetRandomDirection();
				anim.SetBool("moving",true);
				isWalking = true;
			}

		} else if (isWalking == true) {
			walkTime -= Time.deltaTime;
			MoveNPC();

			if (walkTime < 0) {
				anim.SetBool("moving",false);
				idleTime = Random.Range(0.5f,1.2f);
				isWalking = false;
			}
		}
	}

	Vector3 GetRandomDirection() {
		// Generate a random number for the direction
		directionNum = Random.Range(0,4);

		switch (directionNum) {
		case 0:
			// Move down
			return new Vector3(0,-1,0);
		case 1:
			// Move up
			return new Vector3(0,1,0);
		case 2:
			// Move right
			return new Vector3(1,0,0);
		case 3:
			// Move left
			return new Vector3(0,-1,0);
		default:
			return new Vector3(0,0,0);
		}
	}

	private void MoveNPC() {
		moveVector.Normalize();
		rgBody.MovePosition(transform.position + moveVector * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			anim.SetBool("dead",true);
			Destroy(this.gameObject);
		}
	}
}
