using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchHw2 : MonoBehaviour {

	private TextMeshProUGUI switchtext;
	private bool haveStarted;

	// Use this for initialization
	void Start () {
		switchtext = GetComponent<TextMeshProUGUI> ();
		switchtext.text = "Compute";
		haveStarted = false;

	}

	private void ClickStart(){
		//把PSO最後結果 丟給complex controller
		Debug.Log("Start");
		SimpleControlHW2.canControl = true;


	}

	private void ClickCompute(){
		haveStarted = true;

		switchtext.text = "Start";

		//把輸入部分：迭代次數、族群大小、網路J值丟給PSO manager

	}

	public void ControlSwitch(){
		if (haveStarted) {
			ClickStart ();
		} else {
			ClickCompute ();
		}
	}
}
