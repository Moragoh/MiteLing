using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public GameObject clone;
	public static bool gameOver;

	private Vector3 change;
	private Animator anim;
	private Rigidbody2D myRigidBody;
	private int eatenNum;
	private GameObject objectsFolder;
	private GameObject generatedClone;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("moving",false);
		eatenNum = 0;
		myRigidBody = GetComponent<Rigidbody2D>();
		objectsFolder = GameObject.Find("Objects");
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		CloneSelf();

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void MoveCharacter() {
		change.Normalize();
		myRigidBody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
	}

	void AnimateAndMove() {
		if (change != Vector3.zero) {
			MoveCharacter();
			anim.SetBool("moving",true);
		} else {
			anim.SetBool("moving",false);
		}
	}

	private void HandleMovement() {
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		AnimateAndMove();
	}

	private void CloneSelf() {
		// For every three apples, create a clone
		if (eatenNum > 1) {
			// instantiate prefab
			float xPos = this.gameObject.transform.position.x;
			float yPos = this.gameObject.transform.position.y;
			generatedClone = Instantiate(clone, new Vector3(xPos, yPos, 0),transform.rotation);
			generatedClone.transform.parent = objectsFolder.transform;
			// Play sound
			eatenNum = 0;

		}

	}

	private void ResetGame() {
		foreach (Transform child in objectsFolder.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}


	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Food")) {
			eatenNum += 1;
		} else if (other.gameObject.CompareTag("Enemy")) {
			// Game over
			ResetGame();
			Destroy(this.gameObject);
			SceneManager.LoadScene("GameOver");
		}
	}

}
