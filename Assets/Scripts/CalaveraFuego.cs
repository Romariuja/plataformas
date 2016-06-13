using UnityEngine;
using System.Collections;

public class CalaveraFuego : MonoBehaviour {
	private GameObject Calavera;
	//public Transform Calavera2;
	public GameObject Calavera2;
	private Animator animator;
	private bool Fuego = false;
	private float FuerzaCalavera=100;
	private Vector3 posicion;
	//private bool follow= true;
	//Use this for initialization

	void awake (){
		animator = GetComponent<Animator>();
	}

	void Start () {

		NotificationCenter.DefaultCenter().AddObserver(this,"PuntuacionGuitarraDisparo");
	//	Calavera2.GetComponent<SpriteRenderer> ().enabled = false;
	}
	

	void PuntuacionGuitarraDisparo(Notification notificacion){
	
		gameObject.SetActive (true);
		//Debug.Log ("Suelta Lastre!");
		Fuego = true;

	//	transform.parent = null;
	//	transform.DetachChildren();
		//Invoke ("Autodestruccion", 4f);
	}


	void Dispara(){
		
	//transform.parent = null;
	//	rigidbody2D.velocity = new Vector2 (FuerzaCalavera, 0);
		//Calavera2 = (Transform) Instantiate (Calavera2, posicion,  Quaternion.Euler(0, 0, 0));
		//Calavera2.rigidbody2D.velocity=new Vector2 (FuerzaCalavera, 0);
		//gameObject.collider.active = false
		//Calavera.GetComponents<BoxCollider>() = false;
		//Calavera2.rigidbody2D.isKinematic = false;
		//Calavera2.transform.parent = null;

		Calavera = (GameObject) Instantiate (Calavera2, posicion,  Quaternion.Euler(0, 0, 0));
		Calavera.GetComponent<Collider2D>().enabled = false;
		//Calavera.transform.parent = null;

		Calavera.GetComponent<Rigidbody2D>().isKinematic = false;
		Calavera.GetComponent<Rigidbody2D> ().velocity=new Vector2 (FuerzaCalavera/2, FuerzaCalavera/8);
		//Invoke("ConCollider",0.1f);
		Invoke ("ConCollider",0.1f);
	}
	void ConCollider(){
		Calavera.GetComponent<Collider2D>().enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
		posicion = transform.position;

		if (Fuego) {		
			Invoke("Dispara",0.2f);
			Fuego=false;
		}

}

	void FixedUpdate(){
//		animator.SetBool ("Fuego", Fuego);	
		}



void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "Suelo") {
		Destroy(gameObject);
	}
}
}