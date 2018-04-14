using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DegreeLog : MonoBehaviour {

	public GameObject player;

	private TextMeshProUGUI degree;
	private float d;

	// Use this for initialization
	void Start () {
		degree = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {	//show degree log
		if (player.transform.rotation.eulerAngles.z>=0 && player.transform.rotation.eulerAngles.z<=180) {	//euler:0~180 unity:0~180  left
			d = player.transform.rotation.eulerAngles.z +90;
		}
		else if (player.transform.rotation.eulerAngles.z>180 && player.transform.rotation.eulerAngles.z<360) {	//euler:180~360 unity:-0~-180  right
			d = player.transform.rotation.eulerAngles.z - 270f;
		}
		degree.text = "Degree: " + d.ToString ("###") + " °";
	}
}
