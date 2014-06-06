using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
