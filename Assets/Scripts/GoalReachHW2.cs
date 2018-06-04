using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalReachHW2 : MonoBehaviour {

	public FileManagerHW2 fileManager;

	void OnTriggerEnter(Collider other){
		//happy end condition
		if (other.CompareTag("EndPoint")) {
			Debug.Log ("Success !");
			fileManager.EndProgram ();
			SceneManager.LoadScene ("HW2");
		}
	}
}
