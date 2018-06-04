using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class SwitchHw2 : MonoBehaviour {

	private TextMeshProUGUI switchtext;
	public static bool haveStarted;
	public PSOManager psoManager;
	public Thread thread;

	// Use this for initialization
	void Start () {
		switchtext = GetComponent<TextMeshProUGUI> ();
		switchtext.text = "Compute";
		haveStarted = false;
		thread = new Thread(psoManager.PSOInit);
	}

	void Update(){
		if (PSOManager.canStart) {
			PSOManager.canStart = false;
			switchtext.text = "Start";
			haveStarted = true;
		}
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

		psoManager.times = int.Parse(GameObject.Find("LoopInput").GetComponent<TMP_InputField>().text);
		psoManager.size = int.Parse(GameObject.Find("SizeInput").GetComponent<TMP_InputField>().text);
		psoManager.J = int.Parse(GameObject.Find("JInput").GetComponent<TMP_InputField>().text);

		thread.Start ();
	}

	public void ControlSwitch(){
		if (haveStarted) {
			haveStarted = false;
			ClickStart ();		//click after pso computation
		} else if (switchtext.text == "Compute") {
			if (GameObject.Find("LoopInput").GetComponent<TMP_InputField>().text!="") {
				if (GameObject.Find("SizeInput").GetComponent<TMP_InputField>().text!="") {
					if (GameObject.Find("JInput").GetComponent<TMP_InputField>().text!="") {
						ClickCompute ();		//first click
					}
				}
			}
		}
	}

	void OnDestory(){
		thread.Abort ();
	}

	void OnApplicationQuit(){
		thread.Abort ();
	}
}
