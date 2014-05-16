using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {
	public float barLength;
	
	protected BattleController bc;
	protected BattleEntity BE;
	
	protected EnergyBarType type;
	
	// Use this for initialization
	void Start () {
		type = EnergyBarType.None;
		
		//Reference to the battle controller.
		bc = (BattleController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController> ());
		//Determines if this health bar is from a player character or from an enemy character.
		BE = (PlayerCharacter) gameObject.GetComponent<PlayerCharacter>();
		if (BE != null) {
			type = EnergyBarType.Player;
		} else {
			BE = (EnemyCharacter) gameObject.GetComponent<EnemyCharacter>();
			if (BE != null) {
				type = EnergyBarType.Enemy;
			}
		}
		
		//Initial length.
		AdjustLength();
		
		if (type == EnergyBarType.None) {
			Debug.LogWarning ("Health bar has no type!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public virtual void OnGUI () {

	}
	
	public virtual void AdjustLength () {
		Debug.LogWarning ("AdjustLength called from a \"pure\" EnergyBar");		
	}
}

public enum EnergyBarType {
	Player,
	Enemy,
	None
}
