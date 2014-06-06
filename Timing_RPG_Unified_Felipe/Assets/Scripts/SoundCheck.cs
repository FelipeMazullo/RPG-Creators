using UnityEngine;
using System.Collections;

public class SoundCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Awake(){
		AudioSource[] musics = this.GetComponents<AudioSource> ();
		foreach (AudioSource s in musics) {
			s.volume = GameManager.Instance.MusicVolume;		
		}
	}
}
