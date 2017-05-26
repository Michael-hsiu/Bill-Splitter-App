using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonManager : MonoBehaviour {

	public List<PersonSlot> persons = new List<PersonSlot>();		// Ref. to slots corresponding to each person
	public GameObject startScreen;										// Takes care of making slots
	//public List<GameObject> personScreens = new List<GameObject>();
	public int numPersons = 1;										// Cache total # of ppl; always start w/ 1 person
	public int currPerson = 0;										// Start index for person screens

	// UI logic for Person Screens
	public Text personName;
	public GameObject itemContainer;
	public Button backButton;
	public Button nextButton;

	public GameObject itemSlot;


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

	public void LoadPersonScreen() {

		// Populate fields
		PersonSlot activePerson = persons [currPerson];


	}

	/*public void LoadFirstPersonScreen() {
		startScreen.SetActive (false);
		//personScreens [0].SetActive (true);
	}*/


	
}
