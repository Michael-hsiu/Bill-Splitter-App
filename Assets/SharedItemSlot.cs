using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedItemSlot : MonoBehaviour {

	public int index;				// Index of item

	public Text itemText;			// Has form of 'Item #_'
	public InputField inputField;	// Where user types in price
	public Button deleteButton;		// Delete selected item

	public GameObject toggleContainer;	// Holds all the toggles
	public List<Toggle> toggleList;		// All the toggles

	// Creates toggles for 'ALL' and each person.
	// A marked toggle means that the person shared this item.
	void PopulateToggles() {
		
	}


}
