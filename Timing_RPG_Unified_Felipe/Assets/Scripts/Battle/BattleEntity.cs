using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleEntity : MonoBehaviour {
	public BaseStat[] baseStats;
	public EnergyStat[] energyStats;
	public List<statusConditionNames> status;

	//When the action points fill up the bar, the player can act.
	public float actionPoints;
	
	public BattleEntityType type;

	//References.
	private BattleGUI BGUI;
	private PlayerController pCon;
	protected BattleController bc;
	private HealthBar HPBar;
	protected KiBar KPBar;

	public GameObject deathExplosion;

	public virtual void Awake() {
		baseStats = new BaseStat[Enum.GetValues(typeof (BaseStatNames)).Length];
		energyStats = new EnergyStat[Enum.GetValues(typeof (EnergyStatNames)).Length];
		status = new List<statusConditionNames> ();
		SetupBaseStats();
		SetupEnergyStats();
	}

	private void SetupBaseStats () {
		for (int i = 0; i < baseStats.Length; i++) {
			baseStats[i] = new BaseStat();
			baseStats[i].Name = ((BaseStatNames)i).ToString();
		}
	}
	
	private void SetupEnergyStats () {
		for (int i = 0; i < energyStats.Length; i++) {
			energyStats[i] = new EnergyStat();
			energyStats[i].Name = ((EnergyStatNames)i).ToString();
		}
	}


	// Use this for initialization
	public virtual void Start () {
		//Reference to the battle GUI.
		BGUI = (BattleGUI) (GameObject.FindGameObjectWithTag("BattleGUI")).GetComponent<BattleGUI>();
		
		//Reference to the player controller.
		pCon = (PlayerController) (GameObject.FindGameObjectWithTag("BattleController")).GetComponent<PlayerController>();
		
		//Reference to the battle controller.
		bc = (BattleController) (GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController> ());
		
		//Reference to the health bar.
		HPBar = (HealthBar) gameObject.GetComponent<HealthBar>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void ListenTick() {
		Debug.LogWarning ("ListenTick called from a \"pure\" BattleEntity!");
	}

	public void SendAction (Action act) {
		bc.actionPool.Add (act);
	}

	public void AdjustCurrentHP (float adj) {

		Debug.Log(adj.ToString() + " damage!");
		energyStats[(int) EnergyStatNames.HP].BaseValue += adj;
		
		if (energyStats[(int) EnergyStatNames.HP].BaseValue <= 0) {
			energyStats[(int) EnergyStatNames.HP].BaseValue = 0;
			//Death.
			if (!status.Contains(statusConditionNames.Dead)) {
				status.Add(statusConditionNames.Dead);
				//Death animation.
				Instantiate (deathExplosion, transform.position, Quaternion.identity);
				gameObject.renderer.enabled = false;
			}
		}
		
		if (energyStats[(int) EnergyStatNames.HP].BaseValue > baseStats[(int) BaseStatNames.MaxHP].AdjustedBaseValue) {
			energyStats[(int) EnergyStatNames.HP].BaseValue = baseStats[(int) BaseStatNames.MaxHP].AdjustedBaseValue;
		}
		
		if (baseStats[(int) BaseStatNames.MaxHP].BaseValue < 1f) {
			baseStats[(int) BaseStatNames.MaxHP].BaseValue = 1f;
		}
		
		HPBar.AdjustLength();
	}

	public void AdjustCurrentKP (float adj) {
		energyStats[(int) EnergyStatNames.KP].BaseValue += adj;
		
		if (energyStats[(int) EnergyStatNames.KP].BaseValue <= 0) {
			energyStats[(int) EnergyStatNames.KP].BaseValue = 0;
		}
		
		if (energyStats[(int) EnergyStatNames.KP].BaseValue > baseStats[(int) BaseStatNames.MaxKP].AdjustedBaseValue) {
			energyStats[(int) EnergyStatNames.KP].BaseValue = baseStats[(int) BaseStatNames.MaxKP].AdjustedBaseValue;
		}
		
		if (baseStats[(int) BaseStatNames.MaxKP].BaseValue < 1f) {
			baseStats[(int) BaseStatNames.MaxKP].BaseValue = 1f;
		}

		if (type == BattleEntityType.Player) {
			KPBar.AdjustLength();
		}
	}
}

public enum BattleEntityType {
	Player,
	Enemy,
	None
}
