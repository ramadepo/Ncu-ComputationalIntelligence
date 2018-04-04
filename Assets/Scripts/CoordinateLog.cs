using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoordinateLog : MonoBehaviour {

	public GameObject player;

	private TextMeshProUGUI log;
	private int count = 0;
	private List<string> logContents = new List<string> ();
	private float lastTime;
	string coordinate;

	// Use this for initialization
	void Start () {
		log = GetComponent<TextMeshProUGUI> ();
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time-lastTime>=1f) {	//show coordinate every 1 second
			lastTime = Time.time;
			coordinate = "( " + player.transform.position.x.ToString ("##.##") + " , " + player.transform.position.y.ToString ("##.##") + " )";
			logContents.Add (coordinate);
			count++;
			if (logContents.Count>10) {	//10 logs max
				logContents.RemoveAt (0);
				count--;
			}
			log.text = "";
			for (int i = 0; i < count; i++) {	//printf
				log.text += logContents [i];
				if (i != count-1) {
					log.text += "\n";
				}
			}
		}
	}
}
