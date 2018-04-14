using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour {

	public ComplexControl com;

	public TMP_InputField forwardDistanceSmall;
	public TMP_InputField forwardDistanceMedium;
	public TMP_InputField forwardDistanceLarge;
	public TMP_InputField forwardSmallWeight;
	public TMP_InputField forwardMediumWeight;
	public TMP_InputField forwardLargeWeight;
	public TMP_InputField LRDistanceSmall;
	public TMP_InputField LRDistanceMedium;
	public TMP_InputField LRDistanceLarge;
	public TMP_InputField LRSmallWeight;
	public TMP_InputField LRMediumWeight;
	public TMP_InputField LRLargeWeight;

	private TextMeshProUGUI switchtext;

	// Use this for initialization
	void Start () {
		switchtext = GetComponent<TextMeshProUGUI> ();
		switchtext.text = "Manual";
		ComplexControl.canControl = false;
		SimpleControl.canControl = true;

		forwardDistanceSmall.text = com.forwardSmallValue.ToString ();
		forwardDistanceMedium.text = com.forwardMediumValue.ToString ();
		forwardDistanceLarge.text = com.forwardLargeValue.ToString ();
		forwardSmallWeight.text = com.forwardSmall.ToString ();
		forwardMediumWeight.text = com.forwardMedium.ToString ();
		forwardLargeWeight.text = com.forwardLarge.ToString ();
		LRDistanceSmall.text = com.RLSmallValue.ToString ();
		LRDistanceMedium.text = com.RLMediumValue.ToString ();
		LRDistanceLarge.text = com.RLLargeValue.ToString ();
		LRSmallWeight.text = com.RLSmall.ToString ();
		LRMediumWeight.text = com.RLMedium.ToString ();
		LRLargeWeight.text = com.RLLarge.ToString ();

	}


	public void ControlSwitch(){
		if (SimpleControl.canControl) {	//work by ComplexControl (CPU)
			switchtext.text = "Auto";

			com.forwardSmallValue = float.Parse (forwardDistanceSmall.text);
			com.forwardMediumValue = float.Parse (forwardDistanceMedium.text);
			com.forwardLargeValue = float.Parse (forwardDistanceLarge.text);
			com.forwardSmall = float.Parse (forwardSmallWeight.text);
			com.forwardMedium = float.Parse (forwardMediumWeight.text);
			com.forwardLarge = float.Parse (forwardLargeWeight.text);
			com.RLSmallValue = float.Parse (LRDistanceSmall.text);
			com.RLMediumValue = float.Parse (LRDistanceMedium.text);
			com.RLLargeValue = float.Parse (LRDistanceLarge.text);
			com.RLSmall = float.Parse (LRSmallWeight.text);
			com.RLMedium = float.Parse (LRMediumWeight.text);
			com.RLLarge = float.Parse (LRLargeWeight.text);



			SimpleControl.canControl = false;
			ComplexControl.canControl = true;
		} else {
			switchtext.text = "Manual";	//work by SimpleControl (people)
			SimpleControl.canControl = true;
			ComplexControl.canControl = false;
		}
	}


}
