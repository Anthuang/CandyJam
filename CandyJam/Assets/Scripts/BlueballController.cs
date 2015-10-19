using UnityEngine;
using System.Collections;

public class BlueballController : MonoBehaviour {
	
	public int speed = 15;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag != "Blueball" && col.gameObject.tag != "Player2") {
			Destroy (this.gameObject);
		}
	}
}
