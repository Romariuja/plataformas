using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {
	private GameObject Nuevo;
	public GameObject[] obj;
	public float tiempoMin=2f;
	public float tiempoMax=3f;
	public bool fin=false;
	public bool para=false;
	public float primeraX=-100f;
	public float ultimaX=-100f;
	public float desplazamientoX=40f;
	public int XMinima = 0;
	private float umbral=100;
	private bool JustinondaJaus= false;
	private GameObject[] Todos_Justin;
	// Use this for initialization. SOLO UNA VEZ
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"PersonajeCorre");
		NotificationCenter.DefaultCenter().AddObserver(this,"PersonajePara");
		//Cuando se active la notificacion llama a PersonajeCorre
		NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
	}


	void PersonajeMuere(){
		fin=true;
	}
	void PersonajePara(){
		para=true;
	}

	void PersonajeCorre(Notification notificacion){
		Generar ();
		para = false;
	}

	// Update is called once per frame
	void Update () {
				ultimaX = transform.position.x;
		//Debug.Log (Time.frameCount%10);
		}
	void Generar(){
				umbral = primeraX + desplazamientoX * Time.time / 10;

		if (umbral > 500) {
			umbral=500;		
		}
		if (!fin && !para && (ultimaX>umbral)) {
			if (transform.position.x>XMinima && !JustinondaJaus){
			Nuevo= (GameObject) Instantiate (obj [Random.Range (0, obj.Length)], transform.position, Quaternion.identity);
				//Debug.Log (obj);
                //Aparece Justin
				if (Nuevo.tag == "Justin"){
					JustinondaJaus= true;
					Todos_Justin = GameObject.FindGameObjectsWithTag("Justin");
					//Destroy(personaje);
					foreach (GameObject personaje in Todos_Justin) {
						Destroy(personaje);
					}
					NotificationCenter.DefaultCenter ().PostNotification (this, "JustinAparece");

				}	
			}
			else
				Instantiate (obj [Random.Range (0, obj.Length-1)], transform.position, Quaternion.identity);
		//	Debug.Log("Se ha creado a mierder");
			Invoke ("Generar", Random.Range (tiempoMin, tiempoMax));
			primeraX=ultimaX;
		}

	}
}
