using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform target;
	public float moveSpeed;
	public int rotationSpeed;
	public float minDistance;
	public int ID;
	public int turningDistance;//After walking this much it turns
	public int direction;//-1 or 1
	int steps;//counts frames
	Vector2 vectorToPlayer;
	public float distanceToPlayer;
	private Transform cacheTransform;
	PlayerOverworldController player;
	
	void Awake() {
			

	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		player = (PlayerOverworldController)GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOverworldController>();

		target = go.transform;
		cacheTransform = transform;
		turningDistance = 40;
		moveSpeed = 0.5f;
		rotationSpeed = 5;
		minDistance = 5.0f;
		steps = 0;
		direction = -1;
	}

	void FixedUpdate () {

		if (!player.getState ().Equals ("Paused")) {
			//Move towards the player if it's close enough, minDistance
			if (distanceToPlayer <= minDistance) {
				rigidbody2D.velocity = new Vector2 (moveSpeed * vectorToPlayer.x, moveSpeed * vectorToPlayer.y);
			
			}
		    //Otherwise follows movement pattern-----------------------
		    else {
			
				rigidbody2D.velocity = new Vector2 (moveSpeed * 2 * direction, 0);
				
			}
			//-----------------------------------------------------------------
		
		}
		
		Debug.DrawLine (target.position, cacheTransform.position, Color.red);
	}
	// Update is called once per frame
	void Update () {
		vectorToPlayer = (target.position - cacheTransform.position);
		distanceToPlayer = Vector2.Distance (target.position, cacheTransform.position);
		if (!player.getState ().Equals ("Paused")) {
			if (steps >= turningDistance) {
				steps = 0;
				direction = direction * (-1);
			}
			steps++;
		}
	}

	
}
