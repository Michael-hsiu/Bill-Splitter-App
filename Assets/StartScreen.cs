using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

	public Button addPersonButton;		// Adds another person
	public GameObject slotContainer;	// Contains all PersonSlots
	public GameObject personSlot;		// Person slot prefab

	public GameObject personPageContainer;	// Container for person pages
	public GameObject personScreen;		// Page for each person

	public PersonManager personManager;		// PersonManager singleton

	void Start() {
		personManager = PersonManager.Instance;		// Get ref to our singleton
	}


	// Called when addPersonButton is pressed
	public void AddPerson() {

		// UI logic
		GameObject newPersonSlot = Instantiate (personSlot);			// Make new Person Slot
		newPersonSlot.transform.SetParent (slotContainer.transform);	// Put Person Slot in Container

		// Logistics
		newPersonSlot.GetComponent<PersonSlot> ().index = personManager.numPersons;	// Index of slot in vert hierarchy
		personManager.persons.Add (newPersonSlot.GetComponent<PersonSlot>());			// Add reference to the slot
		newPersonSlot.GetComponentInChildren<Text>().text = "PERSON #" + (personManager.persons.Count + 1);
		personManager.numPersons += 1;

		Debug.Log ("NUMPERSONS: " + personManager.numPersons);

	}

	// Called when Save&Next is pressed
	public void SaveAndNext() {

		// Instantiate pages for each of the saved PersonSlots
		int numPersons = personManager.numPersons;
		List<PersonSlot> personList = personManager.persons;
		for (int i = 0; i < numPersons; i++) {

			// Using current list of persons, create PersonSlot objects
			personList [i].personName = personList [i].inputField.text;

			// Nest the person page under container
			GameObject currPage = Instantiate (personScreen);
			currPage.name = personList [i].name;				// Since 'numPersons' starts out as 1, but we start at 0
			currPage.transform.SetParent (personPageContainer.transform);

			// Next, populate the fields
			currPage.GetComponent<PersonScreen> ().PopulateSlots ();	// This fills up all item slots for that person
		
			// Give curr pg ref to PersonManager
			personManager.personScreens.Add (currPage);
			currPage.SetActive (false);
		}

		// Load the first person page
		personManager.LoadFirstPersonScreen ();

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
