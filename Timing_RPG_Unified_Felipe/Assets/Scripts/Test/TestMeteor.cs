using UnityEngine;
using System.Collections;

public class TestMeteor : MonoBehaviour {
	public Animator anim;
	public AnimationState state;
	public bool sentMessage;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		sentMessage = false;
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo info = anim.GetNextAnimatorStateInfo (1);
		float integerNormalized = (int) info.normalizedTime;
		float decimalNormalized = info.normalizedTime - (float) integerNormalized;

		/*Debug.Log ("length " + info.length.ToString());
		Debug.Log (info.normalizedTime.ToString());
		Debug.Log (integerNormalized.ToString());
		Debug.Log (decimalNormalized.ToString());*/

		if (decimalNormalized < 0.5f) {
			sentMessage = false;
		}

		if	(decimalNormalized >= 0.5f && !sentMessage) {
			Debug.Log ("Press Now!");
			sentMessage = true;
		}
		
	}
}
