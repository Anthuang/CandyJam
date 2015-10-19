using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

	public Transform movingPlatform;
	public Transform startPosition;
	public Transform endPosition;
	public Vector3 newPosition;
	public string currentState;
	public float smooth;
	public float resetTime;

	// Use this for initialization
	void Start () {
		ChangeTarget ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movingPlatform.position = Vector3.Lerp (movingPlatform.position, newPosition, smooth * Time.deltaTime);
	}

	void ChangeTarget() {
		if (currentState == "Moving To Start") {
			currentState = "Moving To End";
			newPosition = endPosition.position;
		} else if (currentState == "Moving To End") {
			currentState = "Moving To Start";
			newPosition = startPosition.position;
		} else if (currentState == "") {
			currentState = "Moving To End";
			newPosition = endPosition.position;
		}
		Invoke ("ChangeTarget", resetTime);
	}
}
