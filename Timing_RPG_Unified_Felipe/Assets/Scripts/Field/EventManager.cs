using UnityEngine;
using System.Collections;

//Class responsible for managing various gameplay events.
public class EventManager : Singleton<EventManager> {
	public delegate void InteractionHandler (GameObject obj);
	public static event InteractionHandler onInteract;
	public void Interact (GameObject obj) { if (onInteract != null) onInteract (obj);}

}
