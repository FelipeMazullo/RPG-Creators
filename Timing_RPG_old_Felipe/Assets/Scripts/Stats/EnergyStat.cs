using System;

public class EnergyStat {
	private float _baseValue;      //
	private string _name;
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="EnergyStat"/> class.
	/// </summary>
	public EnergyStat () {
		_baseValue = 0f;
		_name = "";
	}
	
	
	#region Basic setters and getters	
	//Methods to determine (get) and modify (set) private EnergyStat values. 
	
	/// <summary>
	/// Gets or sets the base value.
	/// </summary>
	/// <value>The base value.</value>
	public float BaseValue {
		get { return _baseValue;}
		set { _baseValue = value;}
	}

	public string Name {
		get { return _name;}
		set { _name = value;}
	}
	
	#endregion

}

public enum EnergyStatNames {
	HP,
	KP
}
