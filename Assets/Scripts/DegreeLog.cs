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
	void Update () {
		if (player.transform.rotation.eulerAngles.z>=0 && player.transform.rotation.eulerAngles.z<=180) {
			d = player.transform.rotation.eulerAngles.z * -1;
		}
		else if (player.transform.rotation.eulerAngles.z>180 && player.transform.rotation.eulerAngles.z<360) {
			d = player.transform.rotation.eulerAngles.z * -1 + 360;
		}
		degree.text = d.ToString ("###") + " °";
	}
}
