using UnityEngine;
using System.Collections;

public class RotateFruit : MonoBehaviour {

	public GameObject tomato;
	public float Speed;
	public float ForceRotate;

	// Use this for initialization
	void Start () {
		//Speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,Time.deltaTime * Speed);
	}

	void RotateZ () {
		transform.Rotate = Quaternion.Euler (0, 0, ForceRotate);
	}
}
 