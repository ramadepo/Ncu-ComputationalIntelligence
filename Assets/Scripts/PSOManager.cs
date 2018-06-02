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
		path = Application.dataPath + "/train4dAll.txt";
		reader = new StreamReader (path);
		while (!reader.EndOfStream) {
			s = reader.ReadLine ();
			CutString (s);
			InputData.Add (new Data (a, b, c, d));
		}
		reader.Close ();
	}

	public void PSOInit (int times,int size,int j) {
		AllNode.Add (new Node (j));
		for (int i = 0; i < size; i++) {
			AllNode.Add (new Node (j));
		}

		for (int i = 0; i < times; i++) {
			PSOCalculate ();
		}
	}

	public float ReturnTheta(float forward,float left,float right){
		//use the best result to calculate the answer theta
		return 0;
	}

	private void PSOCalculate(){
		//pso calculate



		PSOUpdateTheBest ();
	}

	private void PSOUpdateTheBest(){
		for (int i = 1; i < AllNode.Count; i++) {
			if (AllNode[i].ErrorValue < AllNode[0].ErrorValue) {
				AllNode [0].node.Clear ();
				for (int k = 0; k < AllNode[i].node.Count; k++) {
					AllNode [0].node.Add (AllNode [i].node [k]);
				}
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
	public Node(int j){
		node.Add (Random.Range (-1f, 1f));
		for (int i = 0; i < j; i++) {
			node.Add (Random.Range (-1f, 1f));
		}
		for (int i = 0; i < 3*j; i++) {
			node.Add (Random.Range (0f, 40f));
		}
		for (int i = 0; i < j; i++) {
			node.Add (Random.Range (0f, 1f));
		}

		for (int i = 0; i < node.Count; i++) {
			nodeB.Add (node [i]);
		}
		ErrorValue = 999999f;
	}
}
