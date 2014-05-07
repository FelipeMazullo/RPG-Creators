using UnityEngine;
using System.Collections;

public class Action{
	public GameObject subject;
	public GameObject target;
	public actionNames description;
}

public enum actionNames {
	Attack,
	Super,
	Energy,
	Run,
	WaitingForInput
}
