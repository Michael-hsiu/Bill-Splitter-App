using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour {

	public List<PersonSlot> persons = new List<PersonSlot>();		// Ref. to slots corresponding to each person
	public GameObject startScreen;			// Takes care of making slots
	public int numPersons = 1;					// Cache total # of ppl; always start w/ 1 person


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

	// Deletes person by ID of their slot
	// Use button listener with wrapped delegate to target its PersonSlot index
	public void DeletePerson(int id) {

		PersonSlot target = persons [id];
		persons.RemoveAt (id);				// Remove PersonSlot at targetted index

		// Update IDs of all other persons coming after this one

	}


	
}
