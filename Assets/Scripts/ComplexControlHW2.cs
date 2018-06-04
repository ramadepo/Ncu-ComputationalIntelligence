using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexControlHW2 : MonoBehaviour {

	public static bool canControl;

	public Transform player;
	public LineSensor forward;
	public LineSensor left;
	public LineSensor right;
	public PSOManager psoManager;

	public float theta;
	private float thistime;
	private float tempx;
	private float tempy;
	private float tempro;

	// Use this for initialization
	void Start () {
		canControl = false;
		Time.timeScale = 10f;
		thistime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (canControl && (Time.time-thistime >= 1f)) {
			thistime = Time.time;
			theta = psoManager.ReturnTheta (forward.distance, left.distance, right.distance);
			//theta = 4;
			Debug.Log (theta);
			//x(t+1) = x(t) + cos[Φ(t)+θ(t)] + sin[θ(t)]sin[Φ(t)]
			tempx = player.position.x + (Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)));
			//y(t+1) = y(t) + sin[Φ(t)+θ(t)] - sin[θ(t)]cos[Φ(t)]
			tempy = player.position.y + (Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)));
			//Φ(t+1) = Φ(t) - arcsin[ 2*sin[θ(t)]/b ]
			tempro = player.rotation.eulerAngles.z + 90f - ((Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg);
			//set the car position x,y & rotation Φ
			player.position = new Vector3 (tempx, tempy, 10f);
			player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
		}
	}
}
