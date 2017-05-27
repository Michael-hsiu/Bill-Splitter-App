using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PersonManager : MonoBehaviour {

	public List<PersonSlot> persons = new List<PersonSlot>();		// Ref. to slots corresponding to each person
	public GameObject startScreen;										// Takes care of making slots
	public GameObject sharedItemsScreen;								// Screen for shared items
	//public List<GameObject> personScreens = new List<GameObject>();
	public int numPersons = 1;										// Cache total # of ppl; always start w/ 1 person
	public int currPerson = 0;										// Start index for person screens
	public int currItem = 0;

	// UI logic for Person Screens
	public Text personName;
	public GameObject itemContainer;
	public Button backButton;
	public Button nextButton;

	public GameObject itemSlot;
	public PersonSlot activePerson;


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
		// Make sure correct screens will be displayed
	}

	public void LoadPersonScreen() {

		// Populate fields
		activePerson = persons [currPerson];
		personName.text = "PERSON #" + (currPerson + 1) + ": " + activePerson.personName;

		// Clear items from last person
		foreach (Transform child in itemContainer.transform) {
			Debug.Log ("DESTROYING ITEM!");
			Destroy (child.gameObject);
		}

		// Only if the person hasn't been given any items
		if (activePerson.numItems == 0) {
			AddNewItem ();
			activePerson.numItems += 1;		// We're adding an empty ItemSlot
		} else {

			// Add slots for each of the items for the person
			// Currently there are multiple repeat calculations, but optimize later if time
			//currItem = 0;
			//Debug.Log (activePerson == null);
			//Debug.Log (activePerson.itemSlots == null);

			try {
				foreach (ItemSlot slot in activePerson.itemSlots) {
					//Debug.Log("HI");
					AddExistingItem ();
				}
					
			} catch (System.Exception e) {
				//Debug.Break ();
				//Debug.Log ("CURRITEM: " + currItem);
				Debug.Log ("CAUGHT: " + e);
			}

		}

		sharedItemsScreen.SetActive (false);
		startScreen.SetActive (false);

	}

	public void AddExistingItem() {
		
		GameObject firstItem = Instantiate (itemSlot);
		firstItem.transform.SetParent (itemContainer.transform);

		firstItem.GetComponent<ItemSlot>().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<ItemSlot> ().index = currItem;
		firstItem.GetComponent<ItemSlot>().inputField.text = activePerson.items[currItem].price.ToString ();

		Debug.Log ("CURRITEM: " + currItem);
		Debug.Log (activePerson.items[currItem].price.ToString ());
		this.currItem += 1;		// Num of items
	}

	public void AddNewItem() {
		
		GameObject firstItem = Instantiate (itemSlot);
		firstItem.transform.SetParent (itemContainer.transform);

		firstItem.GetComponent<ItemSlot>().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<ItemSlot> ().index = currItem;

		activePerson.itemSlots.Add (firstItem.GetComponent<ItemSlot>());
		currItem += 1;		// Num of items
	}

	// Called when we save and move on to next Person page!
	public void RecordItems() {

		// Reset price to prevent duplicate counts
		if (activePerson.individualPrice > 0) {
			activePerson.individualPrice = 0.0f;
		}

		// Reset list of Items
		activePerson.items.Clear ();
		foreach (ItemSlot slot in activePerson.itemSlots) {
			
			try {
				// Make sure we need to update ItemSlots, then update Items!
				string itemPriceStr = slot.inputField.text;
				Debug.Log (itemPriceStr);
				// Checks for invalid price
				if (string.IsNullOrEmpty (itemPriceStr)) {
					continue;
				}

				float itemPrice = float.Parse (itemPriceStr);

				// Add new item to player's list of items
				activePerson.items.Add(new PersonSlot.Item(currItem, itemPrice));
				activePerson.UpdateTotalPrice (itemPrice);

			} catch (FormatException fe) {
				Debug.Log ("IMPROPER PRICE FORMAT!");
			}
		}
	}

	public void NextPage() {
		currItem = 0;
		numPersons = persons.Count;
		RecordItems ();

		 if (currPerson == numPersons - 1) {
			// Go to Shared Items pg
			LoadSharedItemsScreen ();
		} else {
			currPerson += 1;
			LoadPersonScreen ();
		}
	}

	public void BackPage() {
		currItem = 0;
		numPersons = persons.Count;
		if (currPerson == 0) {
			// Return to main pg
			RecordItems ();
			LoadStartScreen ();
		} else {
			currPerson -= 1;
			RecordItems ();
			LoadPersonScreen ();
		}
	}

	public void LoadStartScreen() {
		startScreen.GetComponent<StartScreen>().personScreenBkgrnd.SetActive (false);
		startScreen.SetActive (true);
	}

	public void LoadSharedItemsScreen() {
		sharedItemsScreen.SetActive (true);
		sharedItemsScreen.GetComponent<SharedItemsScreen>().LoadSharedItemsScreen ();
		startScreen.GetComponent<StartScreen>().personScreenBkgrnd.SetActive (false);
	}

	public void ClearSlots() {
		
	}

	/*public void LoadFirstPersonScreen() {
		startScreen.SetActive (false);
		//personScreens [0].SetActive (true);
	}*/


	
}
