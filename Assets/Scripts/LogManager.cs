using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {

	public GameObject player;

	private Text log;
	private int count = 0;
	private List<string> logContents = new List<string> ();
	private float lastTime;
	string coordinate;

	// Use this for initialization
	void Start () {
		log = GetComponent<Text> ();
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time-lastTime>=1f) {
			lastTime = Time.time;
			coordinate = "(" + player.transform.position.x.ToString ("##.##") + "," + player.transform.position.y.ToString ("##.##") + ")";
			logContents.Add (coordinate);
			count++;
			if (logContents.Count>10) {
				logContents.RemoveAt (0);
				count--;
			}
			log.text = "";
			for (int i = 0; i < count; i++) {
				log.text += logContents [i];
				if (i != count-1) {
					log.text += "\n";
				}
			}
		}
	}
}
