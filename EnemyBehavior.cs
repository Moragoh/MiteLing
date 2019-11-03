using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	private Transform target;
	private float moveSpeed = 1.5f;
	private Animator anim;

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform != null) {
			transform.position = Vector3.MoveTowards(transform.position,target.position,moveSpeed * Time.deltaTime);
			anim.SetBool("moving",true);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Clone")) {
			//anim.SetBool("dead",true);
			Destroy(this.gameObject);
		}
	}
}
