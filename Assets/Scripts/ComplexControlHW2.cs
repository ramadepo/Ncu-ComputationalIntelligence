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
	}

	// Update is called once per frame
	void Update () {
		if (canControl) {
			theta = psoManager.ReturnTheta (forward.distance, left.distance, right.distance);
			//x(t+1) = x(t) + cos[Φ(t)+θ(t)] + sin[θ(t)]sin[Φ(t)]
			tempx = player.position.x + (Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)))*Time.deltaTime;
			//y(t+1) = y(t) + sin[Φ(t)+θ(t)] - sin[θ(t)]cos[Φ(t)]
			tempy = player.position.y + (Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)))*Time.deltaTime;
			//Φ(t+1) = Φ(t) - arcsin[ 2*sin[θ(t)]/b ]
			tempro = player.rotation.eulerAngles.z + 90f - ((Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg)*Time.deltaTime;
			//set the car position x,y & rotation Φ
			player.position = new Vector3 (tempx, tempy, 10f);
			player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
		}
	}

	private float SSGG(float x, float x0,float roo){	//Gauss function for min(Left,Right)
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (roo, 2))));
		return result;
	}
}
