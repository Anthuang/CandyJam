using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour {
	
	public float maxSpeed = 10f;
	bool facingRight = false;
	float move;
	
	Rigidbody2D rigid;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	float jumpForce = 800f;

	public GameObject Blueball;
	public Transform shootPoint;
	float timeStamp;
	float timeStamp_s;
	float cooldown = 0.3f;
	float cooldown_s = 1.0f;
	int numBalls = 0;
	int health = 3;

	GameObject[] hearts;
	GameObject[] slots;

	public bool powerUpBall;
	float timeStamp_p;
	float duration = 3.0f;

	Animator anim;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.UpArrow)) {
			rigid.AddForce (new Vector2(0, jumpForce));
		}
		if (Input.GetKeyDown (KeyCode.M) && powerUpBall && timeStamp_p > Time.time) {
			if (facingRight) {
				Instantiate (Blueball, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			} else {
				Instantiate (Blueball, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}
		}
		else if (Input.GetKeyDown (KeyCode.M) && timeStamp <= Time.time && numBalls < 3) {
			anim.SetBool ("cast", true);
			timeStamp = Time.time + cooldown;
			if (facingRight) {
				Instantiate(Blueball, shootPoint.position, Quaternion.Euler(new Vector3(0,0,0)));
			} else {
				Instantiate(Blueball, shootPoint.position, Quaternion.Euler(new Vector3(0,0,180f)));
			}
			numBalls += 1;
			if (timeStamp_s == 0)
				timeStamp_s = Time.time + cooldown_s;
		} else {
			anim.SetBool ("cast", false);
		}
		if (timeStamp_s <= Time.time && numBalls > 0) {
			numBalls -= 1;
			timeStamp_s = Time.time + cooldown_s;
		} else if (numBalls == 0) {
			timeStamp_s = 0;
		}
		Redraw ();
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (grounded) {
			anim.SetBool ("jumped", false);
		} else {
			anim.SetBool ("jumped", true);
		}

		if (Input.GetKey (KeyCode.RightArrow)) move = 1;
		else if (Input.GetKey (KeyCode.LeftArrow))  move = -1;
		else move = 0;
		rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

		anim.SetFloat ("speed", Mathf.Abs (move));

		if (move > 0 && !facingRight)
			flip ();
		else if (move < 0 && facingRight)
			flip ();

		if (health <= 0)
			GameOver (2);
	}
	
	void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Fireball") {
			health -= 1;
			UpdateHealth ();
		} else if (col.gameObject.tag == "PowerUp") {
			powerUpBall = true;
			timeStamp_p = Time.time + duration;
			Destroy (col.gameObject);
		}
	}

	void UpdateHealth () {
		hearts = GameObject.FindGameObjectsWithTag("BlueHeart");
		for (int j = 0; j < health; j++) {
			hearts[j].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1.0f);
		}
		for (int k = health; k < 3; k++) {
			hearts[k].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0.5f);
		}
	}

	void Redraw () {
		slots = GameObject.FindGameObjectsWithTag("BlueballSlot");
		for (int j = 0; j < numBalls; j++) {
			slots[j].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0.5f);
		}
		for (int k = numBalls; k < 3; k++) {
			slots[k].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1.0f);
		}
	}

	void GameOver(int level) {
		Application.LoadLevel (level);
	}
}
