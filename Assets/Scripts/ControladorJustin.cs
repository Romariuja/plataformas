using UnityEngine;
using System.Collections;

public class ControladorJustin : MonoBehaviour {

    Vector3 p;
    Vector3 q;
    private float anchura_pantalla;
    private float distancia;
    private int vidas = 3;
	private float fuerzaSalto0 = 100f;
	private float FuerzaGolpe = 100f;
	private float fuerzaSalto ;
	public bool enSuelo=true;
	public bool Onfire=false;
	public Transform ComprobadorSuelo;
	public Transform Pie_izq;
	public Transform Pie_der;
	float ComprobadorRadio=0.1f;
	public LayerMask MascaraSuelo;
	//En falso no se ha utilizado el doble salto y no se esta en el suelo

	//Para asociar variables a eventos del animador de UNITY
//	private bool dobleSalto=true;
	public AudioClip Justin_band;
	public AudioClip Justin_dead;
	private Animator animator;
	private bool corriendo=false;
	private float velocidad=3f;
	private float velocidadAcumulada=3f;
	public bool chocando=false;
	private bool Ritmo=false;
	private bool guitarra=false;
	private bool cantar=false;
	private bool destruido=false;
	private bool preSalto=false;
	public float DecisionSentido;
	private float DecisionSalto;
	private float DecisionCantar;
	private float TiempoDecision = 2;
	public float VelocidadReal=0;
	public int n;
	void Awake (){
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		//Para ser notificado si nos damos un golpe con la plataforma
		//NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		StartCoroutine(MyMethod(Justin_band,0));
	}

	void Autodestruccion(){
				if (!destruido) {

            //Destroy (gameObject);
            gameObject.active = false;
            NotificationCenter.DefaultCenter().PostNotification(this, "JustinMuere");
            //				Debug.Log ("Destruir PErsonaje");
            destruido =true;
				}
		}
	IEnumerator MyMethod(AudioClip Pachanga, float espera) {
		//Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(espera);
		//if (Pachanga == Justin_band) {
		GetComponent<AudioSource> ().Stop ();
			GetComponent<AudioSource> ().clip = Pachanga;
			GetComponent<AudioSource> ().loop = true;
			GetComponent<AudioSource> ().Play ();		
		//}
		//Debug.Log("After Waiting 2 Seconds");
	}

	//void PillaGuitarra(Notification notificacion){
		//GetComponent<Rigidbody2D>().velocity=new Vector2(0, 0);
		//guitarra = true;
	//	corriendo=false;
	//	Ritmo=false;
	//	chocando = true;
		//Invoke ("Autodestruccion", 4f);
	//}
	//FUNCION QUE COMRPUEBA LAS VARIABLES Y ACTUALIZA CADA CIERTO TIEMPO
	void FixedUpdate(){
        distancia = Camera.main.gameObject.transform.position.x - gameObject.transform.position.x;
        p = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        q=  Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        anchura_pantalla = p.x - q.x;

        // Debug.Log(gameObject.transform.position.x);
        //Debug.Log(Camera.main.gameObject.transform.position.x);
      //  Debug.Log(distancia);
            //Debug.Log(anchura_pantalla/2);
        //}
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


        if (1.5*distancia > anchura_pantalla / 2)
        {
            Debug.Log("CENTRA LA MOVIDA JUSTIN");
            DecisionCantar = 0;
            DecisionSalto = 0;
            //            DecisionSentido = Mathf.Sign(-distancia);
            //          VelocidadReal = DecisionSentido * 1.5f * velocidad;

            //GetComponent<Rigidbody2D>().velocity = new Vector2(VelocidadReal, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(35f, GetComponent<Rigidbody2D>().velocity.y);
        }

        else if ((Time.time > n * TiempoDecision && !Onfire))
        {
            Debug.Log("NO CENTRES LA MOVIDA JUSTIN");
            DecisionSentido = Random.Range(-1, 3);
            DecisionSalto = Random.Range(0, 5);
            DecisionCantar = Random.Range(0, 5);
            n++;

            if (DecisionCantar > 3 && !Onfire)
            {
                cantar = true;
            }
            else if (Onfire)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            }
            else
            {
                cantar = false;
                VelocidadReal = DecisionSentido * velocidad;
                GetComponent<Rigidbody2D>().velocity = new Vector2(VelocidadReal, GetComponent<Rigidbody2D>().velocity.y);
                //velocidadAcumulada = velocidadAcumulada + 0.05f;
                if (DecisionSalto > 2)
                {
                    preSalto = true;
                    fuerzaSalto = fuerzaSalto0 * +0.1f * DecisionSalto;
                    StartCoroutine(MyMethod());
                    DecisionSalto = 0;
                }
            }
        }
			
	
		animator.SetFloat ("VelxJ", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		animator.SetBool("preSaltoJ", preSalto);
		enSuelo = Physics2D.OverlapCircle (ComprobadorSuelo.position, ComprobadorRadio, MascaraSuelo);
		animator.SetBool("isGroundedJ", enSuelo);
		animator.SetBool("Cantar", cantar);
		animator.SetBool("Onfire", Onfire);
		//animator.SetBool ("MariachiMode", guitarra);
	//Cada vez que se comprueba si esta tocando suelo tambien se entera el animador (en Unity)
		//animator.SetBool("isGrounded", enSuelo);
	
	}
	//PARA HACER PAUSAS EN EL CODIGO.
	IEnumerator MyMethod() {
		if (preSalto) {
						//Debug.Log ("Entra aqui");
						yield return new WaitForSeconds (0.3f);
						//GetComponent<AudioSource> ().Play ();
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, fuerzaSalto);
			preSalto=false;		
		}

		//Debug.Log("After Waiting 2 Seconds");
	}
	//PARA LLAMAR AL CODIGO ANTERIO DE PAUSA ES NECESARIO ESTA ORDEN
	//StartCoroutine(MyMethod());
	
	// Update is called once per frame
	void Update () {
}



	void OnTriggerEnter2D(Collider2D other) {
		//Destroy(other.gameObject);
		if (other.tag == "Proyectil") {
			//Debug.Log("Justin tocado");
			StartCoroutine(MyMethod(Justin_dead,0));
			Onfire=true;
			//FALTA PROGRAMAR COMO REBOTA CON ESTO
			//rigidbody2D.velocity=new Vector2(-velocidad,-rigidbody2D.velocity.y);
			//GetComponent<Rigidbody2D> ().velocity = new Vector2 (FuerzaGolpe, rigidbody2D.velocity.y);
			corriendo=false;
			Ritmo=false;
			chocando = true;
			Invoke ("Autodestruccion", 4f);
		}
	}

		//	NotificationCenter.DefaultCenter ().PostNotification (this, "GolpeContraPlataforma");
		//	}
	}
		
		//	NotificationCenter.DefaultCenter ().PostNotification (this, "GolpeContraPlataforma");
		//	}
