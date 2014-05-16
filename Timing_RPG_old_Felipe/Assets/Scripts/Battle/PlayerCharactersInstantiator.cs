using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharactersInstantiator : MonoBehaviour {
	public GameObject playerCharacter;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<GameObject> GetCharacters () {
		GameObject charAuxObj;
		PlayerCharacter pCharAux;
		List<GameObject> chars;

		//Initialize list (important!).
		chars = new List<GameObject> ();

		charAuxObj = (GameObject) Instantiate (playerCharacter, new Vector3 (-9f, -3.5f, 0), Quaternion.identity);
		pCharAux = (PlayerCharacter) charAuxObj.GetComponent<PlayerCharacter> ();
		charAuxObj.renderer.material.color = Color.green;
		pCharAux.Initialization (characterNames.A);
		//Determines the stats of characters.
		pCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 100f;
		pCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 85f;
		pCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 15f;
		pCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 110f;
		pCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 95f;
		pCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		pCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 25f;
		//Action points start to fill up now.
		pCharAux.actionPoints = 0f;
		chars.Add(charAuxObj);

		charAuxObj = (GameObject) Instantiate (playerCharacter, new Vector3 (-5.5f, -1.6f, 0), Quaternion.identity);
		pCharAux = (PlayerCharacter) charAuxObj.GetComponent<PlayerCharacter> ();
		charAuxObj.renderer.material.color = Color.green;
		pCharAux.Initialization (characterNames.S);
		//Determines the stats of characters.
		pCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 130f;
		pCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 85f;
		pCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 11f;
		pCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 110f;
		pCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 95f;
		pCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		pCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 25f;
		//Action points start to fill up now.
		pCharAux.actionPoints = 0f;
		chars.Add(charAuxObj);

		charAuxObj = (GameObject) Instantiate (playerCharacter, new Vector3 (-2.5f, 0.2f, 0), Quaternion.identity);
		pCharAux = (PlayerCharacter) charAuxObj.GetComponent<PlayerCharacter> ();
		charAuxObj.renderer.material.color = Color.green;
		pCharAux.Initialization (characterNames.D);
		//Determines the stats of characters.
		pCharAux.baseStats[(int) BaseStatNames.Attack].BaseValue = 80f;
		pCharAux.baseStats[(int) BaseStatNames.Defense].BaseValue = 85f;
		pCharAux.baseStats[(int) BaseStatNames.Speed].BaseValue = 17f;
		pCharAux.baseStats[(int) BaseStatNames.Willpower].BaseValue = 130f;
		pCharAux.baseStats[(int) BaseStatNames.Spirit].BaseValue = 95f;
		pCharAux.baseStats[(int) BaseStatNames.MaxHP].BaseValue = 100f;
		pCharAux.baseStats[(int) BaseStatNames.MaxKP].BaseValue = 25f;
		//Action points start to fill up now.
		pCharAux.actionPoints = 0f;
		chars.Add(charAuxObj);

		return chars;
	}
}