using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSensor : MonoBehaviour {

	private Ray lineRay = new Ray ();	//ray
	private RaycastHit hit;	//store hit information
	private int hitableMask;	//raycast mask
	private LineRenderer theLine;
	private float range = 100f;

	public float distance;

	// Use this for initialization
	void Start () {
		hitableMask = LayerMask.GetMask ("Wall");
		theLine = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		theLine.SetPosition (0, transform.position);	//???
		lineRay.origin = transform.position;	//set the ray origin
		lineRay.direction = transform.forward;	//set the ray direction
		if (Physics.Raycast(lineRay,out hit,range,hitableMask)) {	//if ray hit
			theLine.SetPosition (1, hit.point);	//draw the line
			distance = Vector3.Distance (transform.position, hit.point);
		}
	}
}
