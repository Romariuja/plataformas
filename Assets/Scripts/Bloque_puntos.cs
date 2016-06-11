using UnityEngine;
using System.Collections;

public class Bloque_puntos : MonoBehaviour {
	private bool haColisionadoConEljugador=false;
	public int puntosGanados = 0;
	//public Collider [] Sup;  
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collision){
		//SOLO para comrprobar con que tipo de objeto colisiona con las plataformas
		//Debug.Log (collision.gameObject.name);
		if (!haColisionadoConEljugador && collision.gameObject.tag == "Player") {
		//Collider Sup=GetComponent<Collider> ();
			//Debug.Log(collision.gameObject.tag);
						//El [0] es para seleccionar solo el primer collider con el que choca la plataforma
						GameObject obj = collision.contacts [0].collider.gameObject;
			//Debug.Log(Sup);
			//SI HA COLISIONADO CON LOS PIES LA PLATAFORMA SON PUNTIS
						if (obj.name == "pie_izq" || obj.name == "pie_der") {
								haColisionadoConEljugador = true;

								//Tercer parametro son los puntos que ganamos
								NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntos", puntosGanados);
						}//else

				//NotificationCenter.DefaultCenter ().PostNotification (this, "GolpeContraPlataforma");
				}
}
}