using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchHw2 : MonoBehaviour {

	private TextMeshProUGUI switchtext;
	public static bool haveStarted;
	public PSOManager psoManager;

	// Use this for initialization
	void Start () {
		switchtext = GetComponent<TextMeshProUGUI> ();
		switchtext.text = "Compute";
		haveStarted = false;

	}

	private void ClickStart(){
		//car run
		Debug.Log("Start");
		SimpleControlHW2.canControl = true;
		ComplexControlHW2.canControl = true;


	}

	private void ClickCompute(){
		Debug.Log ("Compute");
		switchtext.text = "Computing";
		//把輸入部分：迭代次數、族群大小、網路J值丟給PSO manager
		//if(psoManager.PSOInit(times,size,j){switchtext.text = "Start"; haveStarted = true;}

	}

	public void ControlSwitch(){
		if (haveStarted) {
			ClickStart ();		//click after pso computation
		} else if (switchtext.text == "Compute") {
			ClickCompute ();		//first click
		}
	}
}
