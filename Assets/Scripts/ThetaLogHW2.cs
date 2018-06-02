using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThetaLogHW2 : MonoBehaviour {

	public ComplexControlHW2 player;
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
