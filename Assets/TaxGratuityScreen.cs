using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TaxGratuityScreen : MonoBehaviour {

	public InputField taxInputField;		// Where user types in SALES_TAX ($)
	public InputField gratuityInputField;	// Where user types in GRATUITY (%)
	public float taxRate;
	public float gratuityAmnt;

	public PersonManager personManager;

	void Start() {
		personManager = PersonManager.Instance;
	}

	public void CalculateTaxGratuityPrice() {
		
		try {
			// Store TAX_RATE
			string taxRateStr = taxInputField.text;
			// Checks for invalid string
			if (!string.IsNullOrEmpty (taxRateStr)) {
				taxRate = float.Parse (taxRateStr);
			}

			// STORE GRATUITY_AMNT
			string gratuityAmntStr = gratuityInputField.text;
			// Checks for invalid string
			if (!string.IsNullOrEmpty (gratuityAmntStr)) {
				gratuityAmnt = float.Parse (gratuityAmntStr);
			}

		} catch (FormatException fe) {
			Debug.Log ("IMPROPER TAX/GRATUITY FORMAT!");
		}

		// Get the subtotal (for weighted tax / gratuity)
		float totalSubtotal = 0.0f;
		foreach (PersonSlot ps in personManager.persons) {
			totalSubtotal += (ps.individualPrice + ps.sharedPrice);
		}

		// Calculate SALES_TAX for each person
		foreach (PersonSlot ps in personManager.persons) {

			// Person's individual cost + costs from shared items 
			float currSubtotal = ps.individualPrice + ps.sharedPrice;

			// TAX_RATE dependent on person's subtotal, then add a weighted gratuity amnt
			ps.taxGratPrice = currSubtotal * taxRate + (currSubtotal / totalSubtotal) * gratuityAmnt;
			ps.totalPrice = currSubtotal + ps.taxGratPrice;
		}
	}


	public void NextPage() {

		// Load Split Bill page!

	}

	public void BackPage() {
	
		// Load the last Person Screen for 'persons'
	}

}
