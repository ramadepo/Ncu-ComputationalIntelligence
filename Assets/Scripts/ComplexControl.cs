using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexControl : MonoBehaviour {

	public static bool canControl;

	public Transform player;
	public LineSensor forward;
	public LineSensor left;
	public LineSensor right;

	public float forwardSmall;
	public float forwardMedium;
	public float forwradLarge;
	public float RLSmall;
	public float RLMedium;
	public float RLLarge;

	private float theta;
	private float thistime;
	private float tempx;
	private float tempy;
	private float tempro;
	// Use this for initialization
	void Start () {
		thistime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time-thistime >=1) {
			thistime = Time.time;
			if (canControl) {
				if (Mathf.Abs(right.distance-left.distance) <= 0.01) {	//go straight
					theta = 0f;
					tempx = player.position.x + Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempy = player.position.y + Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempro = player.rotation.eulerAngles.z + 90f - (Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg;
					player.position = new Vector3 (tempx, tempy, 10f);
					player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
				}
				else if (right.distance > left.distance) {	//turn right



					/*
					tempx = player.position.x + Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempy = player.position.y + Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempro = player.rotation.eulerAngles.z + 90f - (Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg;
					player.position = new Vector3 (tempx, tempy, 10f);
					player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
					*/
				}
				else if (left.distance > right.distance) {	//turn left



					/*
					tempx = player.position.x + Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempy = player.position.y + Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					tempro = player.rotation.eulerAngles.z + 90f - (Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg;
					player.position = new Vector3 (tempx, tempy, 10f);
					player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
					*/
				}
				/*
					//x(t+1) = x(t) + cos[Φ(t)+θ(t)] + sin[θ(t)]sin[Φ(t)]
					tempx = player.position.x + Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					//y(t+1) = y(t) + sin[Φ(t)+θ(t)] - sin[θ(t)]cos[Φ(t)]
					tempy = player.position.y + Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad));
					//Φ(t+1) = Φ(t) - arcsin[ 2*sin[θ(t)]/b ]
					tempro = player.rotation.eulerAngles.z + 90f - (Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg;
					player.position = new Vector3 (tempx, tempy, 10f);
					player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
				*/
			}
		}
	}

	private float GGSS(float x, float x0, float deauta){
		float result;
		result = Mathf.Exp (((-1) * (Mathf.Pow (x - x0, 2))) / (2 * (Mathf.Pow (deauta, 2))));
		return result;
	}
}
