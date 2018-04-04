using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceLog : MonoBehaviour {

	public bool forward = false;
	public bool left = false;
	public bool right = false;

	public LineSensor forwardDistance;
	public LineSensor leftDistance;
	public LineSensor rightDistance;

	private TextMeshProUGUI distanceText;


	// Use this for initialization
	void Start () {
		distanceText = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (forward) {
			distanceText.text = "forward: " + forwardDistance.distance.ToString("###.##");
		}
		else if (left) {
			distanceText.text = "left: " + leftDistance.distance.ToString("###.##");
		}
		else if (right) {
			distanceText.text = "right: " + rightDistance.distance.ToString("###.##");
		}
	}
}
