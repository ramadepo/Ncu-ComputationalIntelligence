using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour {

	public GameObject player;
	public GameObject greenArea;
	public GameObject wall;

	private float x;
	private float y;
	private float ro;
	private float xNext;
	private float yNext;
	private float xInit;
	private float yInit;
	private float gRx;
	private float gLx;
	private float gRy;
	private float gLy;
	private string s;
	private string path;
	private StreamReader reader;

	// Use this for initialization
	void Start () {
		path=Application.dataPath + "/case01.txt";
		reader = new StreamReader (path);

		s = reader.ReadLine ();
		CutStringPlayer (s);
		s = reader.ReadLine ();
		CutString (s);
		gLx = xNext;
		gRy = yNext;
		s = reader.ReadLine ();
		CutString (s);
		gRx = xNext;
		gLy = yNext;
		greenArea.transform.SetPositionAndRotation (new Vector3 ((gLx + gRx) / 2, (gLy + gRy) / 2, 10f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)));
		greenArea.transform.localScale = new Vector3 (gRx - gLx, gRy - gLy, 0.5f);


		while (!reader.EndOfStream) {
			s = reader.ReadLine ();

		}


		reader.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CutStringPlayer(string s){
		int start = 0;
		string temp="";
		for (int i = start; i < s.Length; i++) {
			if (s[i]==',') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		x = float.Parse (temp);
		for (int i = start; i < s.Length; i++) {
			if (s[i]==',') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		y = float.Parse (temp);
		temp = s.Substring (start, s.Length - start);
		ro = float.Parse (temp);

		player.transform.SetPositionAndRotation(new Vector3(x,y,10f),Quaternion.Euler(new Vector3(0f,0f,ro-90f)));
	}

	private void CutString(string s){
		int start = 0;
		string temp="";
		for (int i = start; i < s.Length; i++) {
			if (s[i]==',') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		xNext = float.Parse (temp);
		temp = s.Substring (start, s.Length - start);
		yNext = float.Parse (temp);
	}
}
