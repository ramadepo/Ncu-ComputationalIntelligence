using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class FileManager : MonoBehaviour {

	public GameObject player;
	public LineSensor forward;
	public LineSensor left;
	public LineSensor right;
	public ComplexControl com;
	public GameObject greenArea;
	public GameObject wall;
	public Transform wallParent;


	private float x;
	private float y;
	private float ro;
	private float scale;
	private float xNext;
	private float yNext;
	private float gRx;
	private float gLx;
	private float gRy;
	private float gLy;
	private string s;
	private string path;
	private StreamReader reader;

	private float thisTime;
	public static StreamWriter writer;
	private static string path2;

	// Use this for initialization
	void Start () {
		string filename;
		filename = GameObject.Find ("FileName").GetComponent<TMP_InputField> ().text;
		GameObject.Find ("FileName").SetActive (false);

		path = Application.dataPath + "/" + filename;
		reader = new StreamReader (path);

		s = reader.ReadLine ();	//get the player coordinate
		CutStringPlayer (s);	//set player
		s = reader.ReadLine ();	//get the goal left up place
		CutString (s);	//x→xNext y→yNext
		gLx = xNext;	//set goal
		gRy = yNext;
		s = reader.ReadLine ();	//get the goal right down place
		CutString (s);
		gRx = xNext;	//set goal
		gLy = yNext;
		greenArea.transform.SetPositionAndRotation (new Vector3 ((gLx + gRx) / 2, (gLy + gRy) / 2, 10f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)));	//set
		greenArea.transform.localScale = new Vector3 (gRx - gLx, gRy - gLy, 0.5f);	//set size

		s = reader.ReadLine ();	//first plot
		CutString(s);
		x = xNext;
		y = yNext;
		while (!reader.EndOfStream) {
			s = reader.ReadLine ();
			CutString (s);

			scale = Mathf.Sqrt ((Mathf.Pow ((xNext - x), 2) + Mathf.Pow ((yNext - y), 2)));	//get the length
			if (xNext >= x) {	//position = (x+xNext)/2,(y+yNext)/2,10     rotation = 0,0,sin-1((right y - left y)/length)
				Instantiate (wall, new Vector3 ((x + xNext) / 2, (y + yNext) / 2, 10), Quaternion.Euler (0f, 0f, Mathf.Asin ((yNext - y) / scale) * Mathf.Rad2Deg),wallParent);
			}
			else {
				Instantiate (wall, new Vector3 ((x + xNext) / 2, (y + yNext) / 2, 10), Quaternion.Euler (0f, 0f, Mathf.Asin ((y - yNext) / scale) * Mathf.Rad2Deg),wallParent);
			}
			wallParent.GetChild (wallParent.childCount - 1).localScale = new Vector3 (scale, 0.5f, 5f);
		
			x = xNext;
			y = yNext;
		}


		reader.Close ();

		thisTime = Time.time;
		path2 = Application.dataPath + "/result.txt";
		writer = new StreamWriter (path2);
	}
	
	// Update is called once per frame
	void Update () {
		if (SimpleControl.canControl || ComplexControl.canControl) {
			if (Time.time-thisTime >= 1f) {
				thisTime = Time.time;
				writer.WriteLine (player.transform.position.x.ToString () + " " + player.transform.position.y.ToString () + " " + forward.distance.ToString () + " " + right.distance.ToString () + " " + left.distance.ToString () + " " + com.theta.ToString ());
			}
		}
	}

	public static void FailedEnd(){
		writer = new StreamWriter (path2);
		writer.WriteLine ("Failed.");
		writer.Close ();
	}

	public static void EndProgram(){
		writer.Close ();
	}

	private void CutStringPlayer(string s){
		int start = 0;
		string temp="";
		for (int i = start; i < s.Length; i++) {	//get x
			if (s[i]==',') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		x = float.Parse (temp);
		for (int i = start; i < s.Length; i++) {	//get y
			if (s[i]==',') {
				temp = s.Substring (start, i - start);
				start = i + 1;
				break;
			}
		}
		y = float.Parse (temp);
		temp = s.Substring (start, s.Length - start);	//get rotate
		ro = float.Parse (temp);

		player.transform.SetPositionAndRotation(new Vector3(x,y,10f),Quaternion.Euler(new Vector3(0f,0f,ro-90f)));	//set
	}

	private void CutString(string s){	//cut x and y into xNext & yNext
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
