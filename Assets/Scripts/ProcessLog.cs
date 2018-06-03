using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProcessLog : MonoBehaviour {

	public static string iterationText;
	public static string individualText;
	public static string jText;
	public static string nText;

	public bool isLoop;
	public bool isIndividual;
	public bool isJ;
	public bool isN;

	private TextMeshProUGUI theText;

	// Use this for initialization
	void Start () {
		theText = GetComponent<TextMeshProUGUI> ();
		iterationText = "Iteration : 0";
		individualText = "Individual : 0";
		jText = "J : 0";
		nText = "N : 0";
	}
	
	// Update is called once per frame
	void Update () {
		if (isLoop) {
			theText.text = iterationText;
		}
		else if (isIndividual) {
			theText.text = individualText;
		}
		else if (isJ) {
			theText.text = jText;
		}
		else if (isN) {
			theText.text = nText;
		}
	}
}
