using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonScreen : MonoBehaviour {

	public List<ItemSlot> itemSlots;		// All itemSlots for curr person

	public PersonManager personManager;		// PersonManager singleton
	public GameObject itemSlotContainer;	// Container for itemSlots
	public GameObject itemSlot;				// Item Slot fab

	public void PopulateSlots() {
		
		for (int i = 0; i < itemSlots.Count; i++) {

			// Nest the item slot under container
			GameObject currItemSlot = Instantiate (itemSlot);
			currItemSlot.GetComponent<PersonSlot>().personName = "ITEM #" + (i+1);
			itemSlots [i].index = i;
			currItemSlot.transform.SetParent (itemSlotContainer.transform);

			// Populate fields
			itemSlots [i].itemText.text = "ITEM #" + i;
		}
	}


}
