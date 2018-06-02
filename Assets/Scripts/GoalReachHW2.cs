using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalReachHW2 : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		//happy end condition
		if (other.CompareTag("EndPoint")) {
			FileManagerHW2.EndProgram ();
			Debug.Log ("Success !");
			SceneManager.LoadScene ("HW2");
		}
	}
}
