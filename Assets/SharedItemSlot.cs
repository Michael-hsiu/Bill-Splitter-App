using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedItemSlot : MonoBehaviour {

	public int index;				// Index of item
	public float price;				// Price as obtained from input field

	public Text itemText;			// Has form of 'Item #_'
	public InputField inputField;	// Where user types in price
	public Button deleteButton;		// Delete selected item

	public GameObject toggleContainer;	// Holds all the toggles
	public GameObject togglePrefab;		// Toggle prefab on a panel
	public List<NameToggle> toggleList;		// All the toggles

	public PersonManager personManager;		// PersonManager singleton
	public SharedItemsScreen sharedItemsScreen;

	void Start() {
		personManager = PersonManager.Instance;		// Get ref to our singleton
	}

	public void DeleteItem() {
		sharedItemsScreen.DeleteItem (index);
	}



}
