using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThetaLog : MonoBehaviour {

	public ComplexControl player;
	private TextMeshProUGUI theta;

	// Use this for initialization
	void Start () {
		theta = GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void Update () {	//show degree log
		
		theta.text = "Theta: " + player.theta.ToString ("##") + " °";
	}
}
