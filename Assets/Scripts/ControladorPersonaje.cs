using UnityEngine;
using System.Collections;

public class ControladorPersonaje : MonoBehaviour {

	public float fuerzaSalto = 100f;
	private float FuerzaGolpe = 50f;

	public bool enSuelo=true;
	public Transform ComprobadorSuelo;
	public Transform Pie_izq;
	public Transform Pie_der;
	float ComprobadorRadio=0.8f;
	public LayerMask MascaraSuelo;
	//En falso no se ha utilizado el doble salto y no se esta en el suelo

	//Para asociar variables a eventos del animador de UNITY
	public bool dobleSalto=true;
	private Animator animator;
	private bool corriendo=false;
	public float velocidad=3f;
	public float velocidadAcumulada=3f;
	public bool chocando=false;
	public bool Ritmo=false;
	public bool guitarra=false;
	private bool destruido=false;
	private float Velocidad_ant;
	private Vector2 VelocidadCambio;
	void Awake (){
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		//Para ser notificado si nos damos un golpe con la plataforma
		NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		NotificationCenter.DefaultCenter().AddObserver(this,"JustinAparece");
		//Pie_der = Pie_der.gameObject;
		//Pie_izq = Pie_izq.gameObject;


		//NotificationCenter.DefaultCenter().AddObserver(this,"Posicion camara");
	}

	void Autodestruccion(){
				if (!destruido) {
			VelocidadCambio=new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
			NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeCambio", VelocidadCambio);
            //	Destroy (gameObject);
            gameObject.active = false;			
            //Debug.Log ("Destruir PErsonaje");
			destruido=true;
				}
		}

	void PillaGuitarra(Notification notificacion){
		GetComponent<Rigidbody2D>().velocity=new Vector2(0, 0);
		guitarra = true;
		corriendo=false;
		Ritmo=false;
		chocando = true;

		Invoke ("Autodestruccion", 4f);
	}

	void JustinAparece(Notification notificacion){
		GetComponent<Rigidbody2D>().velocity=new Vector2(0, 0);
		corriendo=false;
		Ritmo=false;
		chocando = true;
	}

	//FUNCION QUE COMRPUEBA LAS VARIABLES Y ACTUALIZA CADA CIERTO TIEMPO
	void FixedUpdate(){
		if (GetComponent<Rigidbody2D> ().velocity.y > 0.1) {
			//Debug.Log(GetComponent<Rigidbody2D> ().velocity.y);
			Pie_izq.gameObject.GetComponent<Collider2D>().enabled=false;
			Pie_der.gameObject.GetComponent<Collider2D>().enabled=false;

			//(GameObject) Pie_der.collider2D.enabled=false;
				} else {
			Pie_izq.gameObject.GetComponent<Collider2D>().enabled=true;
			Pie_der.gameObject.GetComponent<Collider2D>().enabled=true;
			//(GameObject) Pie_izq.collider2D.enabled=false;
			//(GameObject) Pie_der.collider2D.enabled=false;
				}

		if (corriendo) {
						GetComponent<Rigidbody2D>().velocity = new Vector2 (velocidadAcumulada, GetComponent<Rigidbody2D>().velocity.y);
						velocidadAcumulada= velocidadAcumulada + 0.05f;
				} else
			NotificationCenter.DefaultCenter().PostNotification(this, "PersonajePara");
						velocidadAcumulada = velocidad;
		animator.SetFloat ("Velx", GetComponent<Rigidbody2D>().velocity.x);
		enSuelo = Physics2D.OverlapCircle (ComprobadorSuelo.position, ComprobadorRadio, MascaraSuelo);
		animator.SetBool("isGrounded", enSuelo);
		animator.SetBool ("MariachiMode", guitarra);
	//Cada vez que se comprueba si esta tocando suelo tambien se entera el animador (en Unity)
		//animator.SetBool("isGrounded", enSuelo);
		if (enSuelo && (Input.GetMouseButtonDown (0))) {
			animator.SetBool("preSalto", true);
				}
		if (enSuelo) {
			dobleSalto=false;
				}
	}
	//PARA HACER PAUSAS EN EL CODIGO.
	IEnumerator MyMethod() {
		//Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(0.1f);
		GetComponent<AudioSource>().Play ();
		GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x, fuerzaSalto);
		//Debug.Log("After Waiting 2 Seconds");
	}
	//PARA LLAMAR AL CODIGO ANTERIO DE PAUSA ES NECESARIO ESTA ORDEN
	//StartCoroutine(MyMethod());
	// Update is called once per frame
	void Update () {
		//PARA COMRPOBAR QUE SI CORRE
		if (Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.x) > 0.001) {
			NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeCorre");
			Ritmo = true;
			chocando=false;
		} else
			Ritmo = false;

//		if (chocando) {
//
//			chocando=false;
//		}

		if (!chocando && !Ritmo && !enSuelo) {
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Velocidad_ant/2, 0);
						corriendo = false;
						Ritmo = false;
						chocando = true;
				} else {
			Velocidad_ant=GetComponent<Rigidbody2D>().velocity.x;
				}
		//	NotificationCenter.DefaultCenter ().PostNotification (this, "GolpeContraPlataforma");
		//	}


		if (Input.GetMouseButtonDown (0)) {
			guitarra = false;
			if(Ritmo){
				//hacemos que salte si puede saltar
				if (enSuelo || !dobleSalto){
					if(!dobleSalto && !enSuelo){
						dobleSalto=true;
						//animator.SetBool("preSalto", false);
					}
					animator.SetBool("preSalto", false);
					StartCoroutine(MyMethod());
					
					//GetComponent<Rigidbody2D>().velocity=new Vector2(rigidbody2D.velocity.x, fuerzaSalto);
					///Vector2 al ser 2 dimensiones el vector de fuerza de Salto, 0 al no querer salto n x
					//GetComponent<Rigidbody2D>().AddForce(new Vector2(0,fuerzaSalto));
					//animator.SetBool("preSalto", true);
					//El Truco esta en que aun no le ha dado tiempo a actualizas enSuelo cuando da la primera vez al boton
					//No entra en este bucle despues de darle una vez al boton porque aun no ha actualizado "enSuelo"
					
				}

				if (Ritmo){
					//NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeCorre");
					//Debug.Log(GetComponent<Rigidbody2D>().velocity)
				}
				//CAMBIAR ESTO PORQUE NO SIGNIFICA QUE ESTE CORRIENDO
				//HAY QUE COMPROBAR LA VELOCIDAD
		}else{
				corriendo=true;



			}


		}
	
}






	void OnCollisionEnter2D(Collision2D collision){
		//SOLO para comrprobar con que tipo de objeto colisiona con las plataformas
		//Debug.Log (collision.gameObject.name);


			//El [0] es para seleccionar solo el primer collider con el que choca la plataforma
			GameObject obj = collision.contacts [0].collider.gameObject;
			
			//SI HA COLISIONADO CON LOS PIES LA PLATAFORMA SON PUNTIS
		if ((obj.name == "Item_Botas") || (obj.tag == "Justin") ) {
				
				
				//FALTA PROGRAMAR COMO REBOTA CON ESTO
				//rigidbody2D.velocity=new Vector2(-velocidad,-rigidbody2D.velocity.y);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-FuerzaGolpe, GetComponent<Rigidbody2D>().velocity.y);
				
				corriendo=false;
				Ritmo=false;
				chocando = true;

				//Tercer parametro son los puntos que ganamos
			//	NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntos", puntosGanados);
			}
				
			//	NotificationCenter.DefaultCenter ().PostNotification (this, "GolpeContraPlataforma");
	//	}
	}
}
