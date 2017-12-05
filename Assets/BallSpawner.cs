using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

	public GameObject ballPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject newBall = Instantiate (ballPrefab);
			newBall.transform.localPosition = transform.localPosition;
			newBall.transform.localScale = new Vector3 (2, 2, 2);
		}
	}
}
