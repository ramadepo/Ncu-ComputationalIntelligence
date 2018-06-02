using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleControlHW2 : MonoBehaviour {

	public static bool canControl;
	public Camera cam;
	public GameObject pathBall;

	private Vector3 gasp;
	private float horizontal;
	private float vertical;
	private float thisTime;

	// Use this for initialization
	void Start () {
		thisTime = Time.time;
		gasp = cam.transform.position - transform.position;
		canControl = false;
	}

	// Update is called once per frame
	void Update () {
		//show the car path
		if (canControl) {
			cam.transform.position = transform.position + gasp;	//camera follow
			if (ComplexControlHW2.canControl || SimpleControlHW2.canControl ) {
				if (Time.time-thisTime >= 0.5f && FileManagerHW2.mapComplete) {
					thisTime = Time.time;
					Instantiate (pathBall, transform.position, transform.rotation);
				}
			}
		}

	}

	void OnTriggerEnter(Collider other){
		//bad end condition
		if (other.CompareTag("Wall")) {
			FileManagerHW2.FailedEnd ();
			Debug.Log ("Failed !");
			SceneManager.LoadScene ("HW2");
		}
	}
}
