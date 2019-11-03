using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGenerator : MonoBehaviour {

	public GameObject apple;
	public GameObject enemy;
	public float enemyDelayLow = 12;
	public float enemyDelayHigh = 16;

	private float appleDelay;
	private float enemyDelay;
	private float enemyAccelDelay = 8;
	private float xPosApple;
	private float yPosApple;
	private float xPosEnemy;
	private float yPosEnemy;
	private int timePassed;
	private int appleCount;
	private GameObject generatedApple;
	private GameObject generatedEnemy;
	private GameObject objectsFolder;

	// Use this for initialization
	void Start () {
		print("game resetatr");
		appleDelay = Random.Range(5f,8f);
		enemyDelay = Random.Range(enemyDelayLow,enemyDelayHigh);
		enemyDelayLow = 9;
		enemyDelayHigh = 12;
		enemyAccelDelay = 8;

		objectsFolder = GameObject.Find("Objects");
	}
	
	// Update is called once per frame
	void Update () {
		GenerateApples();
		EnemyAccelerator();
		EnemyGenerator();
	}

	void GenerateApples() {
		appleDelay -= Time.deltaTime;

		if (appleDelay <= 0) {
			xPosApple = Random.Range(-7.5f,7.5f);
			yPosApple = Random.Range(-4.3f,4.3f);
			generatedApple = Instantiate(apple, new Vector3(xPosApple,yPosApple,0), transform.rotation);
			generatedApple.transform.parent = objectsFolder.transform;
			appleDelay = Random.Range(5f,8f);
		}
	}

	void EnemyAccelerator() {
		enemyAccelDelay -= Time.deltaTime;

		if(enemyAccelDelay <= 0) {
			if (enemyDelayLow >= 5.6f) {
				enemyDelayLow -= 0.2f;
			}
				
			if (enemyDelayHigh >= 10) {
				enemyDelayHigh -= 0.2f;
			}
			enemyAccelDelay = 8;
		}
	}

	void EnemyGenerator() {
		enemyDelay -= Time.deltaTime;

		if(enemyDelay <= 0) {
			xPosApple = Random.Range(-7.5f,7.5f);
			yPosApple = Random.Range(-4.3f,4.3f);
			generatedEnemy = Instantiate(enemy,new Vector3(xPosApple,yPosApple,0),transform.rotation);
			generatedEnemy.transform.parent = objectsFolder.transform;
			enemyDelay = Random.Range(enemyDelayLow,enemyDelayHigh);
		}
	}

}
