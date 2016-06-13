using UnityEngine;
using System.Collections;

public class ControladorPersonajeMariachi : MonoBehaviour {

	public float fuerzaSalto = 100f;
	private float FuerzaGolpe = 30f;
	private float tiempo_pulsado1;
	private float tiempo_pulsado2;
	private float tiempo_pulsado=0f;
	private float Maximo_tiempo=3f;

	public bool soltado=true;
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
	public bool chocando=false;
	public bool Ritmo=false;
	public bool guitarra=false;
	private bool destruido=false;
	private float Velocidad_ant;
	private bool rayos = false;
	private bool Tiene_rayos = true;
	private bool empezar=false;
	void Awake (){
		animator = GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start () {
		//Para ser notificado si nos damos un golpe con la plataforma
		//NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		NotificationCenter.DefaultCenter().AddObserver(this,"PuntuacionGuitarra");
		NotificationCenter.DefaultCenter().AddObserver(this,"JustinAparece");
		NotificationCenter.DefaultCenter().AddObserver(this,"Sinbalas");

	}


	
	void Sinbalas(Notification notificacion){
		Tiene_rayos = false;
//		Debug.Log("QUIIITAAAARayosssss");
		//Invoke ("Autodestruccion", 4f);
	}


	void PuntuacionGuitarra(Notification notificacion){
		Tiene_rayos = true;

		//Invoke ("Autodestruccion", 4f);
	}

	void JustinAparece(Notification notificacion){
		GetComponent<Rigidbody2D>().velocity=new Vector2(0, 0);
		corriendo=false;
		Ritmo=false;
		chocando = true;
	}


	void Tiempo_rayos(){
		rayos = false;
		//Invoke ("Autodestruccion", 4f);
	}


	//FUNCION QUE COMRPUEBA LAS VARIABLES Y ACTUALIZA CADA CIERTO TIEMPO
	void FixedUpdate(){//FixedUpdate should be used instead of Update when dealing with Rigidbody. 
		//For example when adding a force to a rigidbody, you have to apply 
		//the force every fixed frame inside FixedUpdate instead of every frame inside Update.
		animator.SetFloat ("Velx", GetComponent<Rigidbody2D>().velocity.x);
		enSuelo = Physics2D.OverlapCircle (ComprobadorSuelo.position, ComprobadorRadio, MascaraSuelo);
		animator.SetBool("isGrounded", enSuelo);
		animator.SetBool("rayos", rayos);
		animator.SetBool ("MariachiMode", guitarra);


//						////////////////////
//						if (Input.GetMouseButtonDown (0)) {
//						tiempo_pulsado += Time.deltaTime;
//						tiempo_pulsado = Mathf.Clamp (tiempo_pulsado, 0, Maximo_tiempo);
//				}
//			Debug.Log (tiempo_pulsado);
//		tiempo_pulsado = 0f;
		if(tiempo_pulsado>0)
//			Debug.Log (Tiene_rayos);

		if (tiempo_pulsado > 0.3 && Tiene_rayos) { 
				rayos = true;
				Invoke ("Tiempo_rayos", 1f);
				NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntosCalavera", (int) 2f);
			NotificationCenter.DefaultCenter ().PostNotification (this, "PuntuacionGuitarraDisparo");

			tiempo_pulsado=0f;					
		} else if (tiempo_pulsado > 0) {
		tiempo_pulsado=0f;
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

			tiempo_pulsado = 0f;

			}

								/// 
								//Invoke ("Tiempo_rayos", 2f);

						//}





				//Para atravesar plataformas al saltar
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
			GetComponent<Rigidbody2D>().velocity = new Vector2 (velocidad, GetComponent<Rigidbody2D>().velocity.y);

		} 
	
		
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


		////////////////////
		if (Input.GetMouseButtonDown (0) && soltado) {
			soltado=false;
						tiempo_pulsado1 = Time.time;
						//tiempo_pulsado = Mathf.Clamp (tiempo_pulsado, 0, Maximo_tiempo);
				}
		if (Input.GetMouseButtonUp (0)) {
			tiempo_pulsado2 = Time.time;
			soltado=true;
			//tiempo_pulsado = Mathf.Clamp (tiempo_pulsado, 0, Maximo_tiempo);
			tiempo_pulsado = tiempo_pulsado2 - tiempo_pulsado1;
		}

//		if (tiempo_pulsado2-tiempo_pulsado>0.1)
//			Debug.Log (tiempo_pulsado2-tiempo_pulsado);



		//PARA COMRPOBAR QUE SI CORRE
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > 0.01) {
						NotificationCenter.DefaultCenter ().PostNotification (this, "PersonajeCorre");
						Ritmo = true;
				} else {
		
						Ritmo = false;
						NotificationCenter.DefaultCenter ().PostNotification (this, "PersonajePara");
				}
		if (chocando) {
			
			chocando=false;
		}
		//ESTO DA PROBLEMas en el cambio de personajes. lo interpreta como un choque
		if (!chocando && !Ritmo && !enSuelo) {
				
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Velocidad_ant/2, 0);
			
			corriendo = false;
			Ritmo = false;
			chocando = true;
		} else {
			Velocidad_ant=GetComponent<Rigidbody2D>().velocity.x;
		}
		
		
//		if (Input.GetMouseButtonDown (0)) {
//			guitarra = false;
//			if(Ritmo){
//				//hacemos que salte si puede saltar
//				if (enSuelo || !dobleSalto){
//					if(!dobleSalto && !enSuelo){
//						dobleSalto=true;
//						//animator.SetBool("preSalto", false);
//					}
//					animator.SetBool("preSalto", false);
//					StartCoroutine(MyMethod());
//					
//					//GetComponent<Rigidbody2D>().velocity=new Vector2(rigidbody2D.velocity.x, fuerzaSalto);
//					///Vector2 al ser 2 dimensiones el vector de fuerza de Salto, 0 al no querer salto n x
//					//GetComponent<Rigidbody2D>().AddForce(new Vector2(0,fuerzaSalto));
//					//animator.SetBool("preSalto", true);
//					//El Truco esta en que aun no le ha dado tiempo a actualizas enSuelo cuando da la primera vez al boton
//					//No entra en este bucle despues de darle una vez al boton porque aun no ha actualizado "enSuelo"
//					
//				}
//				
//				if (Ritmo){
//					//NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeCorre");
//					//Debug.Log(GetComponent<Rigidbody2D>().velocity)
//				}
//				//CAMBIAR ESTO PORQUE NO SIGNIFICA QUE ESTE CORRIENDO
//				//HAY QUE COMPROBAR LA VELOCIDAD
//			}else{
//				corriendo=true;
//				
//				
//				
//			}
//			
//			
//		}
		
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
