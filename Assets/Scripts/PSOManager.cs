﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PSOManager : MonoBehaviour {

	public List<Data> InputData = new List<Data>();
	public List<Node> AllNode = new List<Node>();

	private StreamReader reader;
	private string s;
	private string path;
	private float a,b,c,d;

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
		//PSOInit (5, 3, 2);
		/*******************debug******************/

	}

	public bool PSOInit (int times,int size,int j) {
		//get the input and initialize the pso held
		AllNode.Add (new Node (j));
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



		//loop PSO calculate n times
		for (int i = 0; i < times; i++) {
			PSOCalculate (j);
			ProcessLog.iterationText = "Iteration : " + (i + 1).ToString ();
		}

		return true;
	}

	public float ReturnTheta(float forward,float left,float right){
		//use the best result to calculate the answer theta
		//Fitness(forward,right,left)

		//return FitnessCalculate(0,j,forward,right,left);
		return 0;
	}

	private float FitnessCalculate(int n,int j,float x1,float x2,float x3){
		//calculate the node's fitness
		//sigma(i=1~j) for wi*GSi(InputData[i].data[0~2])+constant
		float temp;
		temp = 0;
		temp += AllNode [n].node [0];

	}

	private float ErrorValueCalculate(int n,int j){
		//sum the node's error value
		//(sigma(i=1~InputData.Count) for pow((InputData[i].data[3]-Fitness(InputData[i].data[0~2])),2))/2
		float temp;
		temp = 0;
		for (int i = 0; i < InputData.Count; i++) {
			temp += Mathf.Pow ((FF (InputData [i].data [3], 0f, 40f) - FitnessCalculate (n, j, FF (InputData [i].data [0], 20f, 20f), FF (InputData [i].data [1], 15f, 15f), FF (InputData [i].data [2], 15f, 15f))), 2f);
			temp = temp / 2;
		}
		return temp;
	}

	private void PSOCalculate(int j){
		//PSO calculate



		//each error value calculate
		for (int i = 1; i < AllNode.Count; i++) {
			AllNode [i].ErrorValue = ErrorValueCalculate (i, j);
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

	private float GS(float x, float x0,float roo){
		//Gauss function for min(Left,Right)
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (roo, 2))));
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
	public float ErrorValue;
	public float ErrorValueB;
	public Node(int j){
		node.Add (Random.Range (-1f, 1f));
		for (int i = 0; i < j; i++) {
			node.Add (Random.Range (-1f, 1f));
		}
		for (int i = 0; i < 3*j; i++) {
			node.Add (Random.Range (-1f, 1f));
		}
		for (int i = 0; i < j; i++) {
			node.Add (Random.Range (0f, 1f));
		}

		for (int i = 0; i < node.Count; i++) {
			nodeB.Add (node [i]);
		}
		ErrorValue = 999999f;
		ErrorValueB = 999999f;
	}
}