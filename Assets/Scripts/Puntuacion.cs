using UnityEngine;
using System.Collections;

public class Puntuacion : MonoBehaviour {
	public bool guitarra=false;
	public int puntuacion = 0;
	public int puntuacionCalavera = 0;
	public TextMesh marcador;
	public TextMesh marcadorCalavera;
	public AudioClip GuitarraClip;
	public AudioClip Justin;
	private float espera;
	// Use this for initialization

	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"IncrementarPuntos");
		NotificationCenter.DefaultCenter().AddObserver(this,"IncrementarPuntosCalavera");
		NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
		NotificationCenter.DefaultCenter ().AddObserver (this, "JustinAparece");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PuntuacionGuitarraDisparo");
		ActualizarMarcador ();
		ActualizarMarcadorCalavera();
	}
	void PersonajeMuere(Notification notificacion){
		if (puntuacion > EstadoJuego.estadoJuego.puntuacionMaxima) {
			Debug.Log ("Puntuacion maxima superada!!! Maxima: " +EstadoJuego.estadoJuego.puntuacionMaxima+" Actual"+ puntuacion);
			EstadoJuego.estadoJuego.puntuacionMaxima = puntuacion;
			EstadoJuego.estadoJuego.Guardar ();

		}else{
			Debug.Log ("Puntuacion maxima NOOOOOO superada!! Maxima" +EstadoJuego.estadoJuego.puntuacionMaxima+" Actual"+ puntuacion);
		}
	
	}
	void IncrementarPuntos(Notification notificacion){
		int puntosAIncrementar = (int)notificacion.data;
		puntuacion += puntosAIncrementar;
		//Debug.Log ("Incrementado "+puntosAIncrementar+" puntos. Total ganados: " + puntuacion);
		ActualizarMarcador ();
	}

	void IncrementarPuntosCalavera(Notification notificacion){
		int puntosAIncrementar =  (int) (notificacion.data);
		if (puntosAIncrementar>1){
			puntuacionCalavera -= 1;
		}
		else{
			puntuacionCalavera += puntosAIncrementar;}
		if (puntuacionCalavera <= 0) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "Sinbalas");
			puntuacionCalavera=0;
		} else {
			NotificationCenter.DefaultCenter ().PostNotification (this, "PuntuacionGuitarra");
		}
		ActualizarMarcadorCalavera ();
		ActualizarMarcadorCalavera ();
		//if ((puntuacionCalavera > 0) && guitarra) {
			//Debug.Log("DaleRayosssss");
			//NotificationCenter.DefaultCenter().PostNotification(this, "PuntuacionGuitarra");
			//puntuacionCalavera=puntuacionCalavera-1;
		//} 
	}


//	void PinchaPachanga(AudioClip Pachanga){
//		GetComponent<AudioSource>().Stop ();
//		GetComponent<AudioSource>().clip = Pachanga;
//		GetComponent<AudioSource> ().loop = false;
//		GetComponent<AudioSource>().Play ();
//	}

	IEnumerator MyMethod(AudioClip Pachanga, float espera) {
		//Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(espera);
		if (Pachanga == Justin) {
//			GetComponent<AudioSource> ().Stop ();
//			audio.PlayOneShot(Pachanga, 1F);
				} else {
						GetComponent<AudioSource> ().Stop ();
						GetComponent<AudioSource> ().clip = Pachanga;
						GetComponent<AudioSource> ().loop = true;
						GetComponent<AudioSource> ().Play ();

				}
		//Debug.Log("After Waiting 2 Seconds");
	}
	//PARA LLAMAR AL CODIGO ANTERIO DE PAUSA ES NECESARIO ESTA ORDEN
	//

	void PillaGuitarra(Notification notificacion){
//		GetComponent<AudioSource>().Stop ();
//		GetComponent<AudioSource>().clip = GuitarraClip;
//		GetComponent<AudioSource> ().loop = false;
//		GetComponent<AudioSource>().Play ();
		//Invoke ("PinchaPachanga(GuitarClip)", 4f);
		StartCoroutine(MyMethod(GuitarraClip,3));
		guitarra = true;

	}

	void JustinAparece(Notification notificacion){
		StartCoroutine(MyMethod(Justin,0));
		
	}
	void ActualizarMarcador(){
		marcador.text = puntuacion.ToString ();
	}
	void ActualizarMarcadorCalavera(){
		marcadorCalavera.text = puntuacionCalavera.ToString ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
