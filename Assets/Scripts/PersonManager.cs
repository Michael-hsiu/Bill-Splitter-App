using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour {

	public List<PersonSlot> persons = new List<PersonSlot>();		// Ref. to slots corresponding to each person
	public GameObject startScreen;			// Takes care of making slots
	public int numPersons;					// Cache total # of ppl


	private static PersonManager instance;
	public static PersonManager Instance {
		get {
			if (instance == null) {
				instance = new PersonManager();
			}
			return instance;
		}
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

	
}
