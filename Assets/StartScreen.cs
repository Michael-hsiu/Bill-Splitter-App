using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

	public Button addPersonButton;		// Adds another person
	public GameObject slotContainer;	// Contains all PersonSlots
	public GameObject personSlot;		// Person slot prefab


	// Called when addPersonButton is pressed
	public void AddPerson() {

		// UI logic
		GameObject newPersonSlot = Instantiate (personSlot);			// Make new Person Slot
		newPersonSlot.transform.SetParent (slotContainer.transform);	// Put Person Slot in Container

		// Logistics
		PersonManager.Instance.persons.Add (newPersonSlot.GetComponent<PersonSlot>());		
		newPersonSlot.GetComponentInChildren<Text>().text = "PERSON #" + (PersonManager.Instance.persons.Count + 1);
		PersonManager.Instance.numPersons += 1;

	}

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
