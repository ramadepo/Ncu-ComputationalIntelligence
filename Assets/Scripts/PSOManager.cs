using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PSOManager : MonoBehaviour {

	public List<Data> InputData = new List<Data>();
	public List<Node> AllNode = new List<Node>();

	public static bool canStart;
	public int times, size, J;
	public float localWeight, globalWeight, randomWeight;

	private StreamReader reader;
	private string s;
	private string path;
	private float a,b,c,d;
	private int j;
	private int temp1, temp2, temp3, temp4;
	private bool hasError;
	private System.Random rnd;
	private int signal;

	// Use this for initialization
	private void Start(){
		//store train4D.txt content in variable InputData
		path = Application.dataPath + "/train4dAll.txt";
		reader = new StreamReader (path);
		while (!reader.EndOfStream) {
			s = reader.ReadLine ();
			CutString (s);
			InputData.Add (new Data (a, b, c, d));
		}
		reader.Close ();
		/*******************debug******************/
		//for (int i = 0; i < InputData.Count; i++) {
		//	Debug.Log (i.ToString () + " : " + InputData [i].data [0].ToString () + " " + InputData [i].data [1].ToString () + " " + InputData [i].data [2].ToString () + " " + InputData [i].data [3].ToString ());
		//}
		/*******************debug******************/

		temp1 = temp2 = temp3 = 0;
		temp4 = 1;
		hasError = false;
		canStart = false;
		rnd = new System.Random ();
	}

	private void Update(){
		ProcessLog.iterationText = "Iteration : " + temp1.ToString ();
		ProcessLog.jText = "J : " + temp2.ToString ();
		ProcessLog.nText = "N : " + temp3.ToString ();
		ProcessLog.individualText = "Who : " + (temp4 - 1).ToString ();
		if (hasError) {
			ProcessLog.errorText = "Error : " + AllNode [0].ErrorValue.ToString ();
		}

	}

	public void PSOInit () {
		//get the input and initialize the pso held
		j=J;
		AllNode.Add (new Node (j));
		hasError = true;
		for (int i = 0; i < size; i++) {
			AllNode.Add (new Node (j));
		}
		
		/*******************debug******************/
		//for (int i = 0; i < AllNode.Count; i++) {
		//	Debug.Log (i.ToString () + " : " + AllNode [i].node[5]);
		//	Debug.Log (i.ToString () + "B : " + AllNode [i].nodeB[5]);
		//	Debug.Log (i.ToString () + "'s error : " + AllNode [i].ErrorValue.ToString ());
		//	Debug.Log (i.ToString () + "'s errorB : " + AllNode [i].ErrorValueB.ToString ());
		//}
		/*******************debug******************/

		//init each error value and find the global best
		for (int i = 1; i < AllNode.Count; i++) {
			AllNode [i].ErrorValue = ErrorValueCalculate (i);
		}

		PSOUpdateTheBest ();


		//loop PSO calculate n times
		for (temp1 = 0; temp1 < times; temp1++) {
			PSOCalculate ();
		}

		canStart = true;
	}

	public float ReturnTheta(float forward,float left,float right){
		//use the best result to calculate the answer theta
		//Fitness(forward,right,left)
		float theReturn;
		theReturn = FitnessCalculate (0, forward, right, left);
		theReturn = theReturn * 6f;
		Debug.Log (theReturn);
		if (theReturn > 40f) {
			return 40f;
		}
		else if (theReturn < -40f) {
			return -40f;
		} else {
			return theReturn;
		}
	}

	private float FitnessCalculate(int n,float x1,float x2,float x3){
		//calculate the node's fitness
		//sigma(i=1~j) for wi*GSi(InputData[i].data[0~2])+constant
		float temp;
		temp = 0;
		temp += AllNode [n].node [0];
		Vector3 input;
		Vector3 xx;
		input = new Vector3 (x1, x2, x3);

		for (temp2 = 0; temp2 < j; temp2++) {
			xx = new Vector3 (AllNode [n].node [j + temp2 * 3 + 1], AllNode [n].node [j + temp2 * 3 + 2], AllNode [n].node [j + temp2 * 3 + 3]);
			temp += AllNode [n].node [temp2 + 1] * GS (input, xx, AllNode [n].node [j + 3 * j + temp2 + 1]);
		}

		return temp;
	}

	private float ErrorValueCalculate(int n){
		//sum the node's error value
		//(sigma(i=1~InputData.Count) for pow((InputData[i].data[3]-Fitness(InputData[i].data[0~2])),2))/2
		float temp;
		temp = 0;
		for (temp3 = 0; temp3 < InputData.Count; temp3++) {
			temp += Mathf.Abs (FF (InputData [temp3].data [3], 0f, 40f) - FitnessCalculate (n, FF (InputData [temp3].data [0], 20f, 20f), FF (InputData [temp3].data [1], 20f, 20f), FF (InputData [temp3].data [2], 20f, 20f)));
		}
		temp = temp / InputData.Count;
		return temp;
	}

	private void PSOCalculate(){
		//PSO calculate

		for (temp4 = 1; temp4 < AllNode.Count; temp4++) {
			for (int i = 0; i < AllNode[temp4].node.Count; i++) {
				if (rnd.NextDouble()>0.5) {
					signal = 1;
				} else {
					signal = -1;
				}
				AllNode [temp4].velocity [i] = ((localWeight) * (AllNode [temp4].nodeB [i] - AllNode [temp4].node [i])) + ((globalWeight) * (AllNode [0].node [i] - AllNode [temp4].node [i])) + AllNode [temp4].velocity [i] + (float)((randomWeight) * rnd.NextDouble () * signal);
				AllNode [temp4].node [i] = AllNode [temp4].node [i] + AllNode [temp4].velocity [i];
			}
		}

		//each error value calculate
		for (temp4 = 1; temp4 < AllNode.Count; temp4++) {
			AllNode [temp4].ErrorValue = ErrorValueCalculate (temp4);
		}

		PSOUpdateTheBest ();
	}

	private void PSOUpdateTheBest(){
		//find the best node then store in variable Allnode[0] and find the individual best then store in B
		for (int i = 1; i < AllNode.Count; i++) {
			if (AllNode[i].ErrorValue < AllNode[0].ErrorValue) {
				AllNode [0].node.Clear ();
				for (int k = 0; k < AllNode[i].node.Count; k++) {
					AllNode [0].node.Add (AllNode [i].node [k]);
				}
				AllNode [0].ErrorValue = AllNode [i].ErrorValue;
			}
		}
		for (int i = 1; i < AllNode.Count; i++) {
			if (AllNode[i].ErrorValue < AllNode[i].ErrorValueB) {
				AllNode [i].nodeB.Clear ();
				for (int k = 0; k < AllNode[i].node.Count; k++) {
					AllNode [i].nodeB.Add (AllNode [i].node [k]);
				}
				AllNode [i].ErrorValueB = AllNode [i].ErrorValue;
			}

			//held random transport to somewhere with 1/1000 probability	

			if (rnd.NextDouble()<=0.05) {
				int x = rnd.Next (1, AllNode.Count);
				AllNode [x].node.Clear ();
				if (rnd.NextDouble()>0.5) {
					signal = 1;
				} else {
					signal = -1;
				}
				AllNode [x].node.Add ((float)(rnd.NextDouble()*signal));
				for (int w = 0; w < j; w++) {
					if (rnd.NextDouble()>0.5) {
						signal = 1;
					} else {
						signal = -1;
					}
					AllNode [x].node.Add ((float)(rnd.NextDouble()*signal));
				}
				for (int w = 0; w < 3*j; w++) {
					if (rnd.NextDouble()>0.5) {
						signal = 1;
					} else {
						signal = -1;
					}
					AllNode [x].node.Add ((float)(rnd.NextDouble()*signal));
				}
				for (int w = 0; w < j; w++) {
					AllNode [x].node.Add ((float)(rnd.NextDouble()));
				}
				//after the transport re-calculate the error
				AllNode [x].ErrorValue = ErrorValueCalculate (x);
			}
		}

		//after transport re-udate the best
		for (int i = 1; i < AllNode.Count; i++) {
			if (AllNode[i].ErrorValue < AllNode[0].ErrorValue) {
				AllNode [0].node.Clear ();
				for (int k = 0; k < AllNode[i].node.Count; k++) {
					AllNode [0].node.Add (AllNode [i].node [k]);
				}
				AllNode [0].ErrorValue = AllNode [i].ErrorValue;
			}
		}
		for (int i = 1; i < AllNode.Count; i++) {
			if (AllNode [i].ErrorValue < AllNode [i].ErrorValueB) {
				AllNode [i].nodeB.Clear ();
				for (int k = 0; k < AllNode [i].node.Count; k++) {
					AllNode [i].nodeB.Add (AllNode [i].node [k]);
				}
				AllNode [i].ErrorValueB = AllNode [i].ErrorValue;
			}
		}
	}

	private void CutString(string s){	//cut 4D information into a,b,c,d
		int start = 0;
		string temp="";
		for (int i = start; i < s.Length; i++) {
			if (s[i]==' ') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		a = float.Parse (temp);
		for (int i = start; i < s.Length; i++) {
			if (s[i]==' ') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		b = float.Parse (temp);
		for (int i = start; i < s.Length; i++) {
			if (s[i]==' ') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		c = float.Parse (temp);
		temp = s.Substring (start, s.Length - start);
		d = float.Parse (temp);
	}

	private float GS(Vector3 x, Vector3 x0,float roo){
		//Gauss function for min(Left,Right)
		float result;
		result = Mathf.Exp ((-1f) * (Mathf.Pow (Vector3.Distance (x, x0), 2)) / (2f * (Mathf.Pow (roo, 2))));
		return result;
	}

	private float FF(float x,float center,float range){
		return (x - center) / range;
	}
}

public class Data
{
	public float[] data = new float[4];
	public Data(float a,float b,float c,float d){
		data [0] = a;
		data [1] = b;
		data [2] = c;
		data [3] = d;
	}
}

public class Node
{
	public List<float> node = new List<float>();
	public List<float> nodeB = new List<float> ();
	public List<float> velocity = new List<float> ();
	public float ErrorValue;
	public float ErrorValueB;
	public System.Random rnd;
	public int signal;
	public Node(int j){
		rnd = new System.Random ();
		if (rnd.NextDouble()>0.5) {
			signal = 1;
		} else {
			signal = -1;
		}
		node.Add ((float)(rnd.NextDouble()*signal));
		for (int i = 0; i < j; i++) {
			if (rnd.NextDouble()>0.5) {
				signal = 1;
			} else {
				signal = -1;
			}
			node.Add ((float)(rnd.NextDouble()*signal));
		}
		for (int i = 0; i < 3*j; i++) {
			if (rnd.NextDouble()>0.5) {
				signal = 1;
			} else {
				signal = -1;
			}
			node.Add ((float)(rnd.NextDouble()*signal));
		}
		for (int i = 0; i < j; i++) {
			node.Add ((float)(rnd.NextDouble()*10));
		}

		for (int i = 0; i < node.Count; i++) {
			nodeB.Add (node [i]);
			velocity.Add (0f);
		}
		ErrorValue = 999999f;
		ErrorValueB = 999999f;
	}
}
