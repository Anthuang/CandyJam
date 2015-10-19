using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {
	
	public int speed = 15;

	// Use this for initialization
	void Start () {
//		Redraw ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag != "Fireball" && col.gameObject.tag != "Player1") {
			Destroy (this.gameObject);
		}
	}
}
