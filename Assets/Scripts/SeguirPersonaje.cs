using UnityEngine;
using System.Collections;

public class SeguirPersonaje : MonoBehaviour {
	public Transform personaje;
	public Transform personaje2;
	public Transform personaje3;
	private float Margen_Justin=10f;
	public float separacion=1000f;
	public Vector3 posicion;
	public Vector3 posicionj;
	private bool guitarra=false;
	private bool generado=false;
	public bool fin=false;
	private bool Justin=false;
	float tiltAngle = 270;
	private ControladorPersonajeMariachi ControladorMariachi;
	//float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
	//float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;


	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		NotificationCenter.DefaultCenter().AddObserver(this,"PersonajeMuere");
		NotificationCenter.DefaultCenter().AddObserver(this,"PersonajeCambio");
		NotificationCenter.DefaultCenter().AddObserver(this,"JustinAparece");
	}
	void PillaGuitarra(){
		Invoke ("GeneraMariachi", 4f);
		}


	void PersonajeMuere(){
		fin=true;
	}
	void JustinAparece(Notification notificacion){
		if (!Justin) {
						 
						Camera camera = GetComponent<Camera> ();
						Vector3 p = camera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height / 2, camera.nearClipPlane + Margen_Justin));
			posicionj=p + new Vector3 (-Margen_Justin,0,0);			
			//personaje3 = (Transform) Instantiate (personaje3, posicionj,  Quaternion.Euler(0, 0, 0));
						personaje3 = (Transform)Instantiate (personaje3, p, Quaternion.Euler (0, 0, 0));
						Justin = true;
				}
//		personaje3 = (Transform) GameObject.FindGameObjectsWithTag("Justin");
		//personaje3 = (Transform) personaje3.transform.Find ("Justin");
	}

	void GeneraMariachi(){
		if (!generado) {
			// accedemos al script con los valores iniciales
//			balaControl.Speed = 100;
//			balaControl.Damage = 30;
				//	Instantiate (personaje2, posicion, Quaternion.identity);
			posicion = new Vector3 (personaje.position.x, personaje.position.y, personaje.position.z);
			personaje2 = (Transform) Instantiate (personaje2, posicion,  Quaternion.Euler(0, 0, tiltAngle));

			//GameObject personaje2 = (GameObject) Instantiate (personaje2, posicion,  Quaternion.Euler(0, 0, tiltAngle));
			//ControladorPersonajeMariachi ControladorMariachi = personaje2.GetComponent<ControladorPersonajeMariachi>; 
			//ControladorMariachi.Ritmo=true;
			//ControladorMariachi.corriendo=true;
			//personaje2.rigidbody2D.velocity=new Vector2(30,30);


			//Debug.Log "Mariachi creado en" + posicion);
			guitarra = true;
			generado=true;
			}

	}

	void PersonajeCambio(Notification guay)
	{//Para que el Mariachi mantenga la velocidad del personaje anterior
		//Vector3 someVectorThree = note.data;
         personaje2.GetComponent<Rigidbody2D>().velocity=(Vector2) guay.data;
		//GetComponent<Rigidbody2D> ().velocity=
		//GetComponent<Rigidbody2D> ().velocity=new Vector2(30f,30f);
		//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
	}

	// Update is called once per frame
	void Update () {
		if (!fin){
			//Camera camera = GetComponent<Camera>();
			Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width-Margen_Justin,Screen.height/2, GetComponent<Camera>().nearClipPlane));
			//Debug.Log(p);//Va diciendo al coordenada que corresponde al final de la pantalla (punto medio de altura)
			//Debug.Log(posicion);
			if(Justin){//No actualiza bien la posicion de personaje3
				//Debug.Log (personaje3.position.x);
				posicionj = new Vector3 (personaje3.position.x, personaje3.position.y, personaje3.position.z);
				transform.position = Vector3.Lerp(this.transform.position, new Vector3(personaje3.position.x + separacion, transform.position.y, transform.position.z),0.05f);
			//	transform.position = new Vector3 (personaje3.position.x + separacion, transform.position.y, transform.position.z);
			}
				else if (!guitarra) {
						posicion = new Vector3 (personaje.position.x, personaje.position.y, personaje.position.z);
				//Debug.Log (posicion);	
				posicion=new Vector3 (personaje.position.x + separacion, transform.position.y, transform.position.z);
			//	Debug.Log (posicion);
				transform.position = new Vector3 (personaje.position.x + separacion, transform.position.y, transform.position.z);
				//transform.position = new Vector3 (personaje.position.x + separacion, personaje.position.y-separacion, transform.position.z);
				} else {//Estado MAriachi
				posicion = new Vector3 (personaje2.position.x, personaje2.position.y, personaje2.position.z);
						transform.position = new Vector3 (personaje2.position.x + separacion, transform.position.y, transform.position.z);
				//transform.position = new Vector3 (personaje2.position.x + separacion, personaje2.position.y, transform.position.z);

			}
		//	transform.position = posicion;

		}
}
}