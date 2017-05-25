using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonSlot : MonoBehaviour {

	public int index;		// Index of person
	public string personName;		// Name of person
	public List<Item> items = new List<Item>();		// List of all items they bought

	public GameObject deleteButton;
	public InputField inputField;
	public GameObject personPage;		// Ref to its page


	public class Item {
		
		public int id;			// Item #_
		public float price;		// Actual cost

	}

	public void UpdateIDs() {

		// Assigns all items in list to their position in list, in event of a deletion of an item
		for (int i = 0; i < items.Count; i++) {
			items [i].id = i;
		}
	}

}
