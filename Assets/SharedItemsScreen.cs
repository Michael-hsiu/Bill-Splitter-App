using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedItemsScreen : MonoBehaviour {

	public GameObject itemSlotContainer;		// Holds all shared items
	public List<SharedItemSlot> existingItems;
	public int currItem = 1;

	public GameObject sharedItemSlot;

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
		} else {

			// Add slots for each of the items for the person
			// Currently there are multiple repeat calculations, but optimize later if time
			foreach (SharedItemSlot slot in existingItems) {
				AddExistingItem ();
			}

		}

	}

	public void AddExistingItem() {

		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		firstItem.GetComponent<SharedItemSlot>().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<SharedItemSlot> ().index = currItem;
		firstItem.GetComponent<SharedItemSlot>().inputField.text = existingItems[currItem].price.ToString ();

		currItem += 1;		// Num of items
	}

	public void AddNewItem() {

		GameObject firstItem = Instantiate (sharedItemSlot);
		firstItem.transform.SetParent (itemSlotContainer.transform);

		firstItem.GetComponent<SharedItemSlot>().itemText.text = "ITEM #: " + currItem;
		firstItem.GetComponent<SharedItemSlot> ().index = currItem;

		existingItems.Add (firstItem.GetComponent<SharedItemSlot>());
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
