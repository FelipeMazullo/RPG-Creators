using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {
	public GameObject enemy;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Generates enemies (positioning them in the battlefield) and adds them to the list to be returned.
	public List<GameObject> Generate () {
		GameObject enAuxObj;
		EnemyCharacter eCharAux;
		List<GameObject> enemies = null;

		//Initialize list (important!).
		enemies = new List<GameObject> ();

		enAuxObj = (GameObject) Instantiate (enemy, new Vector3 (1.1f, -1f, 0), Quaternion.identity);
		eCharAux = (EnemyCharacter) enAuxObj.GetComponent<EnemyCharacter>();
		enAuxObj.renderer.material.color = Color.blue;
		//Determines the stats of enemies.
		eCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 105f;
		eCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 10f;
		eCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 30f;
		eCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 10f;
		eCharAux.energyStats[(int) EnergyStatNames.HP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue;
		eCharAux.energyStats[(int) EnergyStatNames.KP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue;
		//Action points start to fill up now.
		eCharAux.actionPoints = 0f;
		enemies.Add(enAuxObj);	

		enAuxObj = (GameObject) Instantiate (enemy, new Vector3 (4.7f, -2.4f, 0), Quaternion.identity);
		eCharAux = (EnemyCharacter) enAuxObj.GetComponent<EnemyCharacter>();
		enAuxObj.renderer.material.color = Color.blue;
		//Determines the stats of enemies.
		eCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 105f;
		eCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 10f;
		eCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 30f;
		eCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 10f;
		eCharAux.energyStats[(int) EnergyStatNames.HP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue;
		eCharAux.energyStats[(int) EnergyStatNames.KP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue;
		//Action points start to fill up now.
		eCharAux.actionPoints = 0f;
		enemies.Add(enAuxObj);	

		enAuxObj = (GameObject) Instantiate (enemy, new Vector3 (8.8f, -3.5f, 0), Quaternion.identity);
		eCharAux = (EnemyCharacter) enAuxObj.GetComponent<EnemyCharacter>();
		enAuxObj.renderer.material.color = Color.blue;
		//Determines the stats of enemies.
		eCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 105f;
		eCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 10f;
		eCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 30f;
		eCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 50f;
		eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 10f;
		eCharAux.energyStats[(int) EnergyStatNames.HP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue;
		eCharAux.energyStats[(int) EnergyStatNames.KP].BaseValue = eCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue;
		//Action points start to fill up now.
		eCharAux.actionPoints = 0f;
		enemies.Add(enAuxObj);	
		
		return enemies;
	}
}
