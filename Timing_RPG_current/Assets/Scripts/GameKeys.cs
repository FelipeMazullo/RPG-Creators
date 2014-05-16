using UnityEngine;
using System.Collections;

public class GameKeys {
	private bool testChamberEastClear;
	private bool testChamberWestChestAppears;

	public bool TestChamberEastClear {
		get {
			return testChamberEastClear;
		}
		set {
			testChamberEastClear = value;
		}
	}
	public bool TestChamberWestChestAppears {
		get {
			return testChamberWestChestAppears;
		}
		set {
			testChamberWestChestAppears = value;
		}
	}

}
