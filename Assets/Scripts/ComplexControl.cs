using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexControl : MonoBehaviour {

	public static bool canControl;

	public Transform player;
	public LineSensor forward;
	public LineSensor left;
	public LineSensor right;

	public float forwardSmallValue = 1f;
	public float forwardMediumValue = 0.5f;
	public float forwardLargeValue = 0f;
	public float forwardSmall = 9f;
	public float forwardMedium = 13f;
	public float forwardLarge = 17f;
	public float RLSmallValue = 0.3f;
	public float RLMediumValue = 0.5f;
	public float RLLargeValue = 1f;
	public float RLSmall = 1f;
	public float RLMedium = 3f;
	public float RLLarge = 5f;

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
			if (canControl) {
				if (Mathf.Abs(right.distance-left.distance) <= 0.01) {	//go straight
					theta = 0f;
				}
				else if (right.distance > left.distance) {	//turn right
					theta = GGGG();
					Debug.Log (theta);
				}
				else if (left.distance > right.distance) {	//turn left
					theta = (-1f * GGGG());
					Debug.Log (theta);
				}
					//x(t+1) = x(t) + cos[Φ(t)+θ(t)] + sin[θ(t)]sin[Φ(t)]
				tempx = player.position.x + (Mathf.Cos ((player.rotation.eulerAngles.z + 90f - theta)*Mathf.Deg2Rad) + (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Sin ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)))*Time.deltaTime;
					//y(t+1) = y(t) + sin[Φ(t)+θ(t)] - sin[θ(t)]cos[Φ(t)]
				tempy = player.position.y + (Mathf.Sin ((player.rotation.eulerAngles.z + 90f + theta)*Mathf.Deg2Rad) - (Mathf.Sin (theta*Mathf.Deg2Rad) * Mathf.Cos ((player.rotation.eulerAngles.z + 90f)*Mathf.Deg2Rad)))*Time.deltaTime;
					//Φ(t+1) = Φ(t) - arcsin[ 2*sin[θ(t)]/b ]
				tempro = player.rotation.eulerAngles.z + 90f - ((Mathf.Asin ((Mathf.Sin (theta * Mathf.Deg2Rad) * 2f) / 6f))*Mathf.Rad2Deg)*Time.deltaTime;
					player.position = new Vector3 (tempx, tempy, 10f);
					player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
			}
	}

	private float GGGG(){
		float afa;
		float beta;
		if (forward.distance >= forwardLarge) {
			afa = forwardLargeValue;
		}
		else if (forward.distance <= forwardSmall) {
			afa = forwardSmallValue;
		} else {
			afa = ((forwardSmallValue * GGSS (forward.distance, forwardSmall)) + (forwardMediumValue * GGSS (forward.distance, forwardMedium)) + (forwardLargeValue * GGSS (forward.distance, forwardLarge))) / (GGSS (forward.distance, forwardSmall) + GGSS (forward.distance, forwardMedium) + GGSS (forward.distance, forwardLarge));
		}
		float minus;
		minus = Mathf.Min (right.distance, left.distance);
		if (minus <= RLSmall) {
			beta = RLSmallValue;
		}
		else if (minus >= RLLarge) {
			beta = RLLargeValue;
		} else {
			beta = ((RLSmallValue * SSGG (minus, RLSmall)) + (RLMediumValue * SSGG (minus, RLMedium)) + (RLLargeValue * SSGG (minus, RLLarge))) / (SSGG (minus, RLSmall) + SSGG (minus, RLMedium) + SSGG (minus, RLLarge));
		}



		return (40f * afa * beta);
	}

	private float SSGG(float x, float x0){
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (1f, 2))));
		return result;
	}

	private float GGSS(float x, float x0){
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (3f, 2))));
		return result;
	}
}
