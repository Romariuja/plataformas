using UnityEngine;
using System.Collections;

public class Puntuacion2 : MonoBehaviour {

	public int puntuacion = 0;
	public int puntuacionCalavera = 0;
	public TextMesh marcador;
	public TextMesh marcadorCalavera;
	public AudioClip GuitarraClip;
	private bool  guitarra=false;
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter().AddObserver(this,"IncrementarPuntos");
		NotificationCenter.DefaultCenter().AddObserver(this,"IncrementarPuntosCalavera");
		NotificationCenter.DefaultCenter().AddObserver(this,"PillaGuitarra");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
		ActualizarMarcador ();
		ActualizarMarcadorCalavera();
	}

	void PinchaPachanga(AudioClip Pachanga){
		GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().clip = Pachanga;
		GetComponent<AudioSource> ().loop = false;
		GetComponent<AudioSource>().Play ();
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
		int puntosAIncrementar = (int)notificacion.data;
		puntuacionCalavera += puntosAIncrementar;
		//Debug.Log ("Incrementado "+puntosAIncrementar+" puntos. Total ganados: " + puntuacion);
		ActualizarMarcadorCalavera ();
		if ((puntuacionCalavera > 0) && guitarra) {
			NotificationCenter.DefaultCenter().PostNotification(this, "PuntuacionGuitarra");
			puntuacionCalavera=puntuacionCalavera-1;
		}
		ActualizarMarcadorCalavera ();
	}

	void PillaGuitarra(Notification notificacion){
		//GetComponent<AudioSource>().Stop ();
		//GetComponent<AudioSource>().clip = GuitarraClip;
		//GetComponent<AudioSource> ().loop = false;
		//GetComponent<AudioSource>().Play ();

		PinchaPachanga (GuitarraClip);

		guitarra = true;
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
