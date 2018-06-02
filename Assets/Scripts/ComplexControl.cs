using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexControl : MonoBehaviour {

	public static bool canControl;

	public Transform player;
	public LineSensor forward;
	public LineSensor left;
	public LineSensor right;

	public float forwardSmallValue = 1f;	//forward weight(afa) of Small Class for theta
	public float forwardMediumValue = 0.5f;	//forward weight(afa) of Midium Class for theta
	public float forwardLargeValue = 0f;	//forward weight(afa) of Large Class for theta
	public float forwardSmall = 9f;		//center of forward of Small Class in Gauss function
	public float forwardMedium = 13f;	//center of forward of Midium Class in Gauss function
	public float forwardLarge = 17f;	//center of forward of Large Class in Gauss function
	public float RLSmallValue = 0.3f;	//min(Left,Right) weight(beta) of Small Class for theta
	public float RLMediumValue = 0.5f;	//min(Left,Right) weight(beta) of Medium Class for theta
	public float RLLargeValue = 1f;		//min(Left,Right) weight(beta) of Large Class for theta
	public float RLSmall = 1f;	//center of min(Left,Right) of Small Class in Gauss function
	public float RLMedium = 3f;	//center of min(Left,Right) of Gauss Medium in Class function
	public float RLLarge = 5f;	//center of min(Left,Right) of Large Class in Gauss function

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
				if (right.distance > left.distance) {	//turn right
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
				//set the car position x,y & rotation Φ
				player.position = new Vector3 (tempx, tempy, 10f);
				player.rotation = Quaternion.Euler (player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, tempro - 90f);
			}
	}

	private float GGGG(){
		float afa;	//forward weight
		float beta;	//min(Left,Right) weight
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

	private float SSGG(float x, float x0){	//Gauss function for min(Left,Right)
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (1f, 2))));
		return result;
	}

	private float GGSS(float x, float x0){	//Gauss function for forward
		float result;
		result = Mathf.Exp (((-1f) * (Mathf.Pow (x - x0, 2))) / (2f * (Mathf.Pow (3f, 2))));
		return result;
	}
}
