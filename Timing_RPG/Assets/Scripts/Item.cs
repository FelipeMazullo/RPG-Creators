using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum Type {
		Equipment,
		Important,
		Other
	}

	public string name;
	public Type type;
	public string description;
	//File name for the item's texture.
	public string texture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
