using UnityEngine;
using System.Collections;

public class ActionTimer : MonoBehaviour {
	private bool timerOn;
	public float timeLeft;
	private float totalActionTime;
	TimerModes tMode;
	//The percentage of the total action time that had passed
	//when the player (first) pressed the action button.
	public float percentageWhenFirstPressed;
	//The number of times the player managed to press
	//the button during the allotted time.
	public int timesPressed;

	// Use this for initialization
	void Start () {
		timerOn = false;
		percentageWhenFirstPressed = 0f;
		timesPressed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		 if (timerOn) {
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0f) {
				timerOn = false;
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (tMode == TimerModes.FirstPress) {
					percentageWhenFirstPressed = (totalActionTime - timeLeft) / totalActionTime;
					timerOn = false;
				} else if (tMode == TimerModes.MostPresses) {
					timesPressed++;
				}
			}
		}
	}

	public void InitializeTimer (float total, TimerModes mode) {
		timerOn = true;
		timeLeft = total;
		totalActionTime = total;
		percentageWhenFirstPressed = 0f;
		timesPressed = 0;
		tMode = mode;
	}
}

public enum TimerModes {
	FirstPress,
	MostPresses
}