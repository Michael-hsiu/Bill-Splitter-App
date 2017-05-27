using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitBillScreen : MonoBehaviour {

	public GameObject taxGratScreen;
	public GameObject splitBillPanel;				// One for each person!
	public GameObject splitBillPanelContainer;		// Parent of all panels

	public PersonManager personManager;

	void Start() {
		personManager = PersonManager.Instance;
	}

	public void LoadSplitBillScreen() {

		// Disable last screen
		taxGratScreen.SetActive (false);

		// Clear items from panel container
		foreach (Transform child in splitBillPanelContainer.transform) {
			Debug.Log ("DESTROYING OLD SPLIT BILLS!");
			Destroy (child.gameObject);
		}

		// Re-populate container w/ updated panels
		foreach (PersonSlot ps in PersonManager.Instance.persons) {

			GameObject billPanel = Instantiate (splitBillPanel);
			billPanel.transform.SetParent (splitBillPanelContainer.transform);

			billPanel.GetComponent<SplitBillPanel>().nameText.text = ps.personName + ": ";
			billPanel.GetComponent<SplitBillPanel> ().priceText.text = "$" + System.Math.Round(ps.totalPrice, 2).ToString ();
		}


	}

}
