using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitBillScreen : MonoBehaviour {

	public GameObject taxGratScreen;
	public GameObject splitBillPanel;				// One for each person!
	public GameObject splitBillPanelContainer;		// Parent of all panels
	public GameObject startScreen;

	public float grandTotal;						// Total cost of the meal

	public PersonManager personManager;

	void Start() {
		personManager = PersonManager.Instance;
	}

	public void LoadSplitBillScreen() {

		// Disable last screen
		taxGratScreen.SetActive (false);
		grandTotal = 0.0f;		// Reset GRAND_TOTAL

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
			float roundedPrice = (float) System.Math.Round (ps.totalPrice, 2);
			grandTotal += roundedPrice;
			billPanel.GetComponent<SplitBillPanel> ().priceText.text = "$" + roundedPrice.ToString ();
		}

		// Instantiate TOTAL panel
		GameObject grantTotalPanel = Instantiate (splitBillPanel);
		grantTotalPanel.transform.SetParent (splitBillPanelContainer.transform);

		grantTotalPanel.GetComponent<SplitBillPanel>().nameText.text = "GRAND TOTAL: ";
		grantTotalPanel.GetComponent<SplitBillPanel> ().priceText.text = "$" + grandTotal.ToString ();


	}

	public void BackPage() {
		
		// Load Tax/Gratuity page
		taxGratScreen.SetActive (true);
		taxGratScreen.GetComponent<TaxGratuityScreen>().LoadTaxGratuityScreen();
		startScreen.SetActive (false);
	}

	public void MainMenu() {

		PersonManager.Instance.currPerson = 0;		// Since we effectively looped around the app

		startScreen.SetActive (true);	
		startScreen.GetComponent<StartScreen>().LoadStartScreen ();
	}

}
