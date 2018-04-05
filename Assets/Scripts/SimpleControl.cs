using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleControl : MonoBehaviour {

	public static bool canControl;
	public Camera cam;

	private Vector3 gasp;
	private float horizontal;
	private float vertical;

	// Use this for initialization
	void Start () {
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
	}

	void OnTriggerEnter(Collider other){	//end condition
		if (other.CompareTag("Wall")) {
			Debug.Log ("Failed !");
			SceneManager.LoadScene ("HW1");
		}
		else if (other.CompareTag("EndPoint")) {
			Debug.Log ("Success !");
			SceneManager.LoadScene ("HW1");
		}
	}

}
