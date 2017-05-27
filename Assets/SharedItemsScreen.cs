﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedItemsScreen : MonoBehaviour {

	public GameObject itemSlotContainer;		// Holds all shared items
	public List<SharedItemSlot> existingItems;
	public int currItem = 1;

	public GameObject sharedItemSlot;			// This is a prefab
	public GameObject toggleSlot;				// This is a prefab
	public GameObject toggleSlotContainerFab;		// This is a prefab

	public GameObject slotHolder;	// In hierarchy

	public PersonManager personManager;

	void Start() {
		personManager = PersonManager.Instance;		// Get ref to our singleton

	}

	// Called from PersonManager
	public void LoadSharedItemsScreen() {

		// Clear items from last time
		foreach (Transform child in itemSlotContainer.transform) {
			Debug.Log ("DESTROYING ITEM");
			Destroy (child.gameObject);
		}

		// Only if the person hasn't been given any items
		if (existingItems.Count == 0) {
			AddNewItem ();
			AddNewItem ();
			AddNewItem ();
		} else {

			// Add slots for each of the items for the person
			// Currently there are multiple repeat calculations, but optimize later if time
			foreach (SharedItemSlot slot in existingItems) {
				AddExistingItem ();
			}

		}

	}

	public void AddExistingItem() {

		// Add SharedItemSlot
		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		firstItem.GetComponent<SharedItemSlot>().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<SharedItemSlot> ().index = currItem;
		firstItem.GetComponent<SharedItemSlot>().inputField.text = existingItems[currItem].price.ToString ();

		// Add associated ToggleSlot, which holds names of every person
		// Remember that we need to persist boolean values!
		GameObject toggleSlotContainer = Instantiate (toggleSlotContainerFab);
		PopulateToggles (toggleSlotContainer);
		toggleSlotContainer.transform.SetParent (itemSlotContainer.transform, false);

		currItem += 1;		// Num of items
	}

	// Creates toggles for 'ALL' and each person.
	// A marked toggle means that the person shared this item.
	public int PopulateToggles(GameObject toggleContainer) {
		//Debug.Log (toggleContainer == null);

		//Debug.Log (PersonManager.Instance == null);

		List<PersonSlot> personList = PersonManager.Instance.persons;

		int numToggles = 0;
		foreach(PersonSlot slot in personList) {
			GameObject toggleFab = Instantiate (toggleSlot);
			toggleFab.transform.SetParent (toggleContainer.transform);

			toggleFab.GetComponent<NameToggle> ().personName.text = slot.personName;

			numToggles += 1;
		}

		return numToggles;
	}

	public void AddNewItem() {

		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		firstItem.GetComponent<SharedItemSlot> ().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<SharedItemSlot> ().index = currItem;

		existingItems.Add (firstItem.GetComponent<SharedItemSlot> ());

		// Add associated ToggleSlot, which holds names of every person
		// Remember that we need to persist boolean values!
		GameObject toggleSlotContainer = Instantiate (toggleSlotContainerFab);
		toggleSlotContainer.transform.SetParent (itemSlotContainer.transform);

		float numToggles = PopulateToggles (toggleSlotContainer);		// Populates toggles and gives us number of toggles populated
		Debug.Log ("NUMTOGGLES: " + numToggles);

		float numSlotsWide = Screen.width / toggleSlot.GetComponent<RectTransform> ().rect.width;
		Debug.Log ("NUMSLOTSWIDE: " + numSlotsWide);

		int numLevels = Mathf.CeilToInt (numToggles / numSlotsWide);
		Debug.Log ("NUMLVLS: " + numLevels);

		Debug.Log ("SLOT_HEIGHT" + toggleSlot.GetComponent<RectTransform> ().rect.height);

		// This uses slot fab height to force a distance btwn 2 SharedItemSlots; also adds a constant fudge factor
		toggleSlotContainer.GetComponent<RectTransform> ().sizeDelta = new Vector2	 (Screen.width, (numLevels * toggleSlot.GetComponent<RectTransform>().rect.height) + 20.0f);

		//GameObject holder = Instantiate (slotHolder);
		//slotHolder.transform.SetParent (itemSlotContainer.transform);

		currItem += 1;		// Num of items
	}

	// Called when we save and move on to next Person page!
	public void RecordItems() {}

	public void NextPage() {
		currItem = 0;
		RecordItems ();

		// Load Tax/Gratuity page
	}

	public void BackPage() {
		currItem = 0;
		RecordItems ();

		PersonManager.Instance.LoadPersonScreen ();
	}
}
