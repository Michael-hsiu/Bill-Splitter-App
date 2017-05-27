﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SharedItemsScreen : MonoBehaviour {

	public GameObject itemSlotContainer;		// Holds all shared items
	public List<SharedItemSlot> existingItems;
	public int currItem = 1;

	public GameObject sharedItemSlot;			// This is a prefab
	public GameObject toggleSlot;				// This is a prefab
	public GameObject toggleSlotContainerFab;		// This is a prefab

	public GameObject slotHolder;	// In hierarchy

	public PersonManager personManager;
	public GameObject taxGratScreen;
	public GameObject personScreen;

	void Start() {
		//personManager = PersonManager.Instance;		// Get ref to our singleton

	}

	// Called from PersonManager
	public void LoadSharedItemsScreen() {

		// Create list of all shared prices
		List<float> prices = new List<float> ();
		foreach (Transform child in itemSlotContainer.transform) {
			if (child.GetComponent<SharedItemSlot>() != null) {
				prices.Add(child.GetComponent<SharedItemSlot>().price);		// Add all prices, since we're destroying GO
			}
		}

		// Clear items from last time
		foreach (Transform child in itemSlotContainer.transform) {
			Debug.Log ("DESTROYING ITEM");
			Destroy (child.gameObject);
		}

		// Only if the person hasn't been given any items
		if (existingItems.Count == 0) {
			AddNewItem ();
			//AddNewItem ();
			//AddNewItem ();
			//AddNewItem ();
		} else {

			// Add slots for each of the items for the person
			// Currently there are multiple repeat calculations, but optimize later if time
			currItem = 0;
			//List<SharedItemSlot> newList = new List<SharedItemSlot> ();
			foreach (int price in prices) {
				AddExistingItem (price);
			}
			//existingItems = newList;

		}

		personScreen.SetActive (false);
		taxGratScreen.SetActive (false);

	}

	public void AddNewItem() {

		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		SharedItemSlot currSharedItemSlot = firstItem.GetComponent<SharedItemSlot> ();
		currSharedItemSlot.itemText.text = "ITEM #: " + (currItem+1);
		currSharedItemSlot.index = currItem;

		existingItems.Add (firstItem.GetComponent<SharedItemSlot> ());		// Add this SharedItemSlot to our master list

		// Add associated ToggleSlot, which holds names of every person
		// Remember that we need to persist boolean values!
		GameObject toggleSlotContainer = Instantiate (toggleSlotContainerFab);
		toggleSlotContainer.transform.SetParent (itemSlotContainer.transform);

		float numToggles = PopulateToggles (currItem, toggleSlotContainer);		// Populates toggles and gives us number of toggles populated
		float numSlotsWide = Screen.width / toggleSlot.GetComponent<RectTransform> ().rect.width;
		int numLevels = Mathf.CeilToInt (numToggles / numSlotsWide);

		// This uses slot fab height to force a distance btwn 2 SharedItemSlots; also adds a constant fudge factor
		toggleSlotContainer.GetComponent<RectTransform> ().sizeDelta = new Vector2	 (Screen.width, (numLevels * toggleSlot.GetComponent<RectTransform>().rect.height) + 20.0f);

		currItem += 1;		// Num of items
	}

	public void AddExistingItem(float price) {

		// Add SharedItemSlot
		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		Debug.Log ("CURRITEM: " + currItem);
		firstItem.GetComponent<SharedItemSlot>().itemText.text = "ITEM #: " + (currItem+1);
		firstItem.GetComponent<SharedItemSlot> ().index = currItem;
		firstItem.GetComponent<SharedItemSlot>().inputField.text = price.ToString ();

		// Add associated ToggleSlot, which holds names of every person
		// Remember that we need to persist boolean values!
		GameObject toggleSlotContainer = Instantiate (toggleSlotContainerFab);
		firstItem.GetComponent<SharedItemSlot> ().toggleContainer = toggleSlotContainer;	// The toggles we're associated with
		//PopulateToggles (currItem, toggleSlotContainer);


		toggleSlotContainer.transform.SetParent (itemSlotContainer.transform, false);

		float numToggles = PopulateToggles (currItem, toggleSlotContainer);		// Populates toggles and gives us number of toggles populated
		float numSlotsWide = Screen.width / toggleSlot.GetComponent<RectTransform> ().rect.width;
		int numLevels = Mathf.CeilToInt (numToggles / numSlotsWide);

		// This uses slot fab height to force a distance btwn 2 SharedItemSlots; also adds a constant fudge factor
		toggleSlotContainer.GetComponent<RectTransform> ().sizeDelta = new Vector2	 (Screen.width, (numLevels * toggleSlot.GetComponent<RectTransform>().rect.height) + 20.0f);

		currItem += 1;		// Num of items
	}

	// Creates toggles for 'ALL' and each person.
	// A marked toggle means that the person shared this item.
	public int PopulateToggles(int currItem, GameObject toggleContainer) {
		//Debug.Log (toggleContainer == null);

		//Debug.Log (PersonManager.Instance == null);
		foreach (Transform child in toggleContainer.transform) {
			Debug.Log ("DESTROYING TOGGLE");
			Destroy (child.gameObject);
		}

		List<PersonSlot> personList = PersonManager.Instance.persons;
		SharedItemSlot currSlot = existingItems [currItem];		// The current slot; we're going to add the toggles to its list!

		int numToggles = 0;
		currSlot.toggleList.Clear ();		// Since we're making all new toggles
		for (int i = 0; i < personList.Count; i++) {
		//foreach(PersonSlot slot in personList) {
			GameObject toggleFab = Instantiate (toggleSlot);
			toggleFab.transform.SetParent (toggleContainer.transform);

			PersonSlot currPerson = personList [i];
			NameToggle currToggle = toggleFab.GetComponent<NameToggle> ();

			currSlot.toggleList.Add (currToggle);
			currToggle.personName.text = currPerson.personName;
			currToggle.id = i;

			numToggles += 1;
		}

		return numToggles;
	}


	// Called when we save and move on to Tax/Gratuity page!
	public void RecordItems() {

		// Reset all shared prices
		foreach (PersonSlot personSlot in PersonManager.Instance.persons) {
			personSlot.sharedPrice = 0;
		}

		// Loop thru all shared item slots
		// Make sure we're modifying these slots!
		foreach (SharedItemSlot slot in existingItems) {

			try {
				// Make sure we need to update ItemSlots, then update Items! <copied>
				string itemPriceStr = slot.inputField.text;
				// Checks for invalid price
				if (string.IsNullOrEmpty (itemPriceStr)) {
					slot.price = 0.0f;
					continue;
				}

				float itemPrice = float.Parse (itemPriceStr);

				// Record the price
				slot.price = itemPrice;

				List<PersonSlot> personsSharing = new List<PersonSlot>();

				// Split the price of the current shared item among those sharing it
				for (int i = 0; i < PersonManager.Instance.persons.Count; i++) {
					
					// If this person is sharing the price for this item
					if (slot.toggleList[i].isSharing) {
						// There is direct corresondence btwn order of toggleList and order of PersonNames
						personsSharing.Add(PersonManager.Instance.persons[i]);
					}
				}

				// Now split item price among all those sharing
				foreach (PersonSlot personSlot in personsSharing) {
					
					personSlot.sharedPrice += (itemPrice / personsSharing.Count);
				}

			} catch (FormatException fe) {
				Debug.Log ("IMPROPER PRICE FORMAT!");
			}
		}
	}

	public void ClearSharedPrices() {
		// Clear all shared prices, since we're going to calculate them again.
		foreach (PersonSlot ps in PersonManager.Instance.persons) {
			if (ps.sharedPrice > 0) {
				ps.sharedPrice = 0.0f;
			}
		}
	}

	public void NextPage() {
		currItem = 0;
		ClearSharedPrices ();
		RecordItems ();

		PersonManager.Instance.startScreen.SetActive (false);
		// Load Tax/Gratuity page
		taxGratScreen.SetActive (true);
		taxGratScreen.GetComponent<TaxGratuityScreen>().LoadTaxGratuityScreen();
	}

	public void BackPage() {
		currItem = 0;
		ClearSharedPrices ();
		RecordItems ();

		PersonManager.Instance.startScreen.SetActive (false);
		personScreen.SetActive (true);
		PersonManager.Instance.LoadPersonScreen ();
	}
}
