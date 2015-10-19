using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform player1, player2;
	public float minSizeY = 5f;
	public float z = -10f;

	private Vector3 offset;

	// Use this for initialization
	void Start () {

	}

	void Update() {
		SetCameraPos ();
		SetCameraSize ();
	}

	void SetCameraPos() {
		Vector3 middle = (player1.position + player2.position) * 0.5f;

		transform.position = new Vector3 (middle.x, middle.y, z);
	}

	void SetCameraSize() {
		float minSizeX = minSizeY * Screen.width / Screen.height;

		float width = (Mathf.Abs (player1.position.x - player2.position.x) * 0.5f)+minSizeX;
		float height = (Mathf.Abs(player1.position.y - player2.position.y) * 0.5f+minSizeY);

		//computing the size
		float camSizeX = Mathf.Max(width, minSizeX);
		GetComponent<Camera>().orthographicSize = Mathf.Max(height,
		camSizeX * Screen.height / Screen.width, minSizeY) - 2;
	}
}
