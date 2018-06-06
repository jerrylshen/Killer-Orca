using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using LeapInternal;
using UnityEngine.UI;


public class Player : MonoBehaviour {

	private Rigidbody pr;
	public float movemin;
	public float moveforce;
	public float turnmin;
	public float turnforce;
	public float upforce;
	public float rotateforce;
	public float speed;
	public float rotateLimit;
	public float maxSpeed;

	public float hp;
	public float losehp;
	public GameObject hpSlider;

	public GameObject text;

	public int score;
	Controller con;
	// Use this for initialization
	void Start () {
		pr = GetComponent<Rigidbody> ();

		turnmin = 50;
		turnforce = 1.5f;
		upforce = 0.2f;
		rotateforce = 0.7f;
		maxSpeed = 50;
		rotateLimit = 0.7f;
		con = new Controller ();
		hp = 100;
		hpSlider = GameObject.Find("Slider");
		losehp = 2f;
		text = GameObject.Find("Text1");
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {

		Frame frame = con.Frame ();
		if (frame.Hands.Count > 0) {
			List<Hand> hands = frame.Hands;
			Hand firstHand = hands [0];
			//Debug.Log (firstHand.Rotation);

			speed = -(firstHand.PalmPosition.z - 50);
			if (speed < 0) {
				speed = 0;
			}

			speed = speed / 300 * maxSpeed;
			//forward
			//left
			if (firstHand.PalmPosition.x < -turnmin) {
				transform.Rotate (0, Time.deltaTime * (-firstHand.PalmPosition.x - turnmin) * -turnforce, 0);
			}
			//right
			if (firstHand.PalmPosition.x > turnmin) {
				transform.Rotate (0, Time.deltaTime * (firstHand.PalmPosition.x - turnmin) * turnforce, 0);
			}

			if (firstHand.PalmPosition.y < 200 || firstHand.PalmPosition.y > 300) {
				transform.Rotate (Time.deltaTime * (250 - firstHand.PalmPosition.y) * upforce, 0, 0);
			}

			//transform.rotation=new Vector 3(transform.rotation.x, transform.transform.rotation.y, 0) ;
			//Debug.Log (firstHand.Rotation.Normalized);

		} else {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

		}
		pr.velocity = transform.forward * speed;

		hp -= losehp * Time.deltaTime;
		if (hp < 0) {
			hp = 0;
		}
		hpSlider.GetComponent<Slider>().value = hp;
		if (hpSlider.GetComponent<Slider>().value < 30) {
			hpSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<UnityEngine.UI.Image>().color = new Color32 (255, 0, 0, 200);
		} else if(hpSlider.GetComponent<Slider>().value<60){
			hpSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<UnityEngine.UI.Image>().color = new Color32 (255, 255, 0, 200);
		} else {
			hpSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<UnityEngine.UI.Image>().color = new Color32 (0, 255, 0, 200);
		}

		if (transform.rotation.y > 60) {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, 60, 0);
		} else if (transform.rotation.x < -60) {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, -60, 0);
		} else {
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
		}

		//Debug.Log (hp);

		//transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
		//pr.velocity = new Vector3 ();
		//pr.velocity = new Vector3 (Mathf.Min (pr.velocity.x, maxSpeed), Mathf.Min (pr.velocity.y, maxSpeed), Mathf.Min (pr.velocity.z, maxSpeed));
		//pr.velocity = new Vector3 (Mathf.Max (pr.velocity.x, -maxSpeed), Mathf.Max (pr.velocity.y, -maxSpeed), Mathf.Max (pr.velocity.z, -maxSpeed));
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.parent != null) {
			if (col.gameObject.transform.parent.transform.parent.tag == "fish") {
				Destroy (col.gameObject);
				score += 1;
				hp += 50;
				if (hp > 100) {
					hp = 100;
				}
				//hpSlider.GetComponent<Slider>().value = hp;
				transform.localScale += new Vector3 (0.02f, 0.02f, 0.02f);
				text.GetComponent<Text> ().text = "Score: " + score;
			}
		}
	} 


}
