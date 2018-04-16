using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalReach : MonoBehaviour {

	void OnTriggerEnter(Collider other){	//end condition
		if (other.CompareTag("EndPoint")) {
			FileManager.EndProgram ();
			Debug.Log ("Success !");
			SceneManager.LoadScene ("HW1");
		}
	}
}
