using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonSlot : MonoBehaviour {

	public int index;		// Index of person
	public string personName;		// Name of person
	public List<Item> items = new List<Item>();		// List of all items they bought
	public List<ItemSlot> itemSlots = new List<ItemSlot> ();
	public int numItems;		// Current num of items
	public float individualPrice;	// Total price of all items
	public float sharedPrice;	// Price from shared items
	public float taxGratPrice;	// Price from weighted tax and gratuity
	public float totalPrice;	// FINAL total price

	public GameObject deleteButton;
	public InputField inputField;
	public GameObject personPage;		// Ref to its page1


	public class Item {
		
		public int id;			// Item #_
		public float price;		// Actual cost

		public Item(int id, float price) {
			this.id = id;
			this.price = price;
		}

	}

	public void UpdateTotalPrice(float price) {
		// Currently duplicates all values
		individualPrice += price;
	}

	public void UpdateIDs() {

		// Assigns all items in list to their position in list, in event of a deletion of an item
		for (int i = 0; i < items.Count; i++) {
			items [i].id = i;
		}
	}

}
