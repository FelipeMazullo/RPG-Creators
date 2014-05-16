using System;

public class BaseStat {
	private float _baseValue;      //
	private float _buffValue;      //
	private string _name;


	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat () {
		_baseValue = 0f;
		_buffValue = 0f;
		_name = "";
	}
	
	
#region Basic setters and getters	
	//Methods to determine (get) and modify (set) private BaseStat values. 

	/// <summary>
	/// Gets or sets the base value.
	/// </summary>
	/// <value>The base value.</value>
	public float BaseValue {
		get { return _baseValue;}
		set { _baseValue = value;}
	}

	/// <summary>
	/// Gets or sets the buff value.
	/// </summary>
	/// <value>The buff value.</value>
	public float BuffValue {
		get { return _buffValue;}
		set { _buffValue = value;}
	}

	public string Name {
		get { return _name;}
		set { _name = value;}
	}

#endregion

	/// <summary>
	/// Recalculate the adjusted base value and return it.
	/// </summary>
	/// <value>The adjusted base value.</value>
	public float AdjustedBaseValue {
		get { return _baseValue + _buffValue;}	
	}

}

public enum BaseStatNames {
	Attack,
	Defense,
	Speed,
	Willpower,
	Spirit,
	MaxHP,
	MaxKP
}
