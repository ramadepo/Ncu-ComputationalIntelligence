using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour {

	private TextMeshProUGUI switchtext;

	// Use this for initialization
	void Start () {
		switchtext = GetComponent<TextMeshProUGUI> ();
		switchtext.text = "Auto";
		ComplexControl.canControl = true;
		SimpleControl.canControl = false;
	}


	public void ControlSwitch(){
		if (SimpleControl.canControl) {	//work by ComplexControl (CPU)
			switchtext.text = "Auto";
			SimpleControl.canControl = false;
			ComplexControl.canControl = true;
		} else {
			switchtext.text = "Manual";	//work by SimpleControl (people)
			SimpleControl.canControl = true;
			ComplexControl.canControl = false;
		}
	}


}
