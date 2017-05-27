using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameToggle : MonoBehaviour {

	public int id;				// Corresponds to position of person in 'persons' list of PersonManager
	public Text personName;		// Name of person
	public bool isSharing = false;		// Whether or not this person is sharing the price of this item; new toggles for each person for ea item

	public void ToggleSharingItem(bool isSharing) {
		//Debug.Log (isSharing);
		this.isSharing = !this.isSharing;		// Person is now (not) sharing the item
	}
}
