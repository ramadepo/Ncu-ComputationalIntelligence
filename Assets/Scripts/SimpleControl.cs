using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleControl : MonoBehaviour {

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
		canControl = false;
		gasp = cam.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (canControl) {
			if (Input.GetKey(KeyCode.RightArrow)) {
				transform.Rotate (new Vector3 (0f, 0f, -0.5f));	//rotate
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				transform.Rotate (new Vector3 (0f, 0f, 0.5f));	//rotate
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				transform.Translate (new Vector3 (0f,0.3f,0f));
			}
		}
		cam.transform.position = transform.position + gasp;	//camera follow
		if (ComplexControl.canControl || SimpleControl.canControl) {
			if (Time.time-thisTime >= 0.5f) {
				thisTime = Time.time;
				Instantiate (pathBall, transform.position, transform.rotation);
			}
		}
	}

	void OnTriggerEnter(Collider other){	//end condition
		if (other.CompareTag("Wall")) {
			FileManager.EndProgram ();
			FileManager.FailedEnd ();
			Debug.Log ("Failed !");
			SceneManager.LoadScene ("HW1");
		}
	}

}
