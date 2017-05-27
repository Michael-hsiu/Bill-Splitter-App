﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

	public Button addPersonButton;		// Adds another person
	public GameObject slotContainer;	// Contains all PersonSlots
	public GameObject personSlot;		// Person slot prefab

	public GameObject personPageContainer;	// Container for person pages
	//public GameObject personScreen;		// Page for each person
	public GameObject personScreenBkgrnd;	// Background for person screens
	public GameObject splitBillsScreen;

	public PersonManager personManager;		// PersonManager singleton
	public List<GameObject> personSlots;

	void Start() {
		personManager = PersonManager.Instance;		// Get ref to our singleton
		AddPerson ();
	}

	public void LoadStartScreen() {

		personManager.numPersons = 0;
		personSlots.Clear ();
		int currIndex = 0;

		// Clear items from panel container
		foreach (Transform child in slotContainer.transform) {
			Debug.Log ("DESTROYING OLD PERSON SLOT!");
			Destroy (child.gameObject);
		}

		// Re-populate all person slots
		foreach (PersonSlot ps in personManager.persons) {
			AddExistingPerson (currIndex);
			currIndex += 1;
		}

		splitBillsScreen.SetActive (false);
	}

	public void AddExistingPerson(int currIndex) {
		// UI logic
		GameObject newPersonSlot = Instantiate (personSlot);			// Make new Person Slot
		newPersonSlot.transform.SetParent (slotContainer.transform);	// Put Person Slot in Container
		personSlots.Add (newPersonSlot);

		// Logistics
		newPersonSlot.GetComponent<PersonSlot> ().id = currIndex;	// Index of slot in vert hierarchy
		//personManager.persons.Add (newPersonSlot.GetComponent<PersonSlot>());			// Add reference to the slot
		newPersonSlot.GetComponentInChildren<Text>().text = "PERSON #" + (currIndex + 1);	// CS indexing vs. normal person indexing
		newPersonSlot.GetComponent<PersonSlot>().inputField.text = personManager.persons[currIndex].personName;
		//personManager.numPersons += 1;

		Debug.Log ("NUMPERSONS (should stay constant): " + personManager.numPersons);
	}

	public void LowerIndices(int startID) {
		GameObject target = personSlots [startID];
		personSlots.Remove (target);
		Destroy (target);
		personManager.numPersons -= 1;

		// Target has been removed, start at startID since everything was shifted
		for (int i = startID; i < personSlots.Count; i++) {
			personSlots [i].GetComponent<PersonSlot> ().id -= 1;
			personSlots [i].GetComponentInChildren<Text>().text = "PERSON #" + (personSlots [i].GetComponent<PersonSlot> ().id + 1);	// CS indexing vs. normal person indexing
		}
	}

	// Called when addPersonButton is pressed
	public void AddPerson() {

		// UI logic
		GameObject newPersonSlot = Instantiate (personSlot);			// Make new Person Slot
		newPersonSlot.transform.SetParent (slotContainer.transform);	// Put Person Slot in Container
		personSlots.Add (newPersonSlot);

		// Logistics
		newPersonSlot.GetComponent<PersonSlot> ().id = personManager.numPersons;	// Index of slot in vert hierarchy
		personManager.persons.Add (newPersonSlot.GetComponent<PersonSlot>());			// Add reference to the slot
		newPersonSlot.GetComponentInChildren<Text>().text = "PERSON #" + personManager.persons.Count;
		personManager.numPersons += 1;

		Debug.Log ("NUMPERSONS: " + personManager.numPersons);

	}

	// Called when Save&Next is pressed
	public void SaveAndNext() {

		// Save names of all bill splitters
		int numPersons = personManager.persons.Count;
		List<PersonSlot> personList = personManager.persons;
		for (int i = 0; i < numPersons; i++) {

			// Using current list of persons, set names
			personList [i].personName = personList [i].inputField.text;
		}

		// Load the first person page, which will populate the template Person Screen with saved fields
		personScreenBkgrnd.SetActive (true);
		personManager.LoadPersonScreen();

	}

	// TODO: for when a person is deleted
	public void ClearPages() {}



/*	ist<UpgradableScriptableObject> upgradesList = activePowerupHolder.powerup.GetComponent <Powerup>().powerupData.upgradeList;
	int numSlots = upgradesList.Count;

	for (int i = 0; i < numSlots; i++) {
		// Create a new slot
		GameObject currSlot = Instantiate (shopSlot);	
		activeSlots.Add (currSlot);						// So we know which slots to destroy upon leaving screen
		currSlot.GetComponent <ShopSlot> ().id = i;		// Set the slot's position

		// Set event listener that will update Shop Info panel
		Button currButton = currSlot.GetComponent<Button>();
		currButton.onClick.AddListener(() => UpgradePressed(currSlot));

		// Disable upgrade slot if max lvl achieved
		UpgradableScriptableObject currUpgrade = currSlot.GetComponent <ShopSlot> ().upgrade;
		currUpgrade = upgradesList [i];
		if (currUpgrade.currLvl >= currUpgrade.MAX_LEVEL) {
			DisableSlot (currButton);
		}

		// Populate slot info in list view
		currSlot.GetComponentInChildren <Text>().text = upgradesList[i].powerupName.ToString();		// Set name of upgrade in the list view on left (panel will be set when clicked)

		// Assign its parent
		currSlot.transform.SetParent (upgradesListPanel.transform);



		//if (currButton.GetCom)

	}*/

}
