using UnityEngine;
using System.Collections;


public class Destructor : MonoBehaviour {
	public GameObject[] personajes;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeMuere");
            //BUSAR TAMBIEN EL MARiachi	
            //GameObject personaje = GameObject.Find ("Personaje");
            personajes = GameObject.FindGameObjectsWithTag("Player");
            //Debug.Log (personajes);
            //Destroy(personaje);
            foreach (GameObject personaje in personajes)
            {
                personaje.SetActive(false);
            }
            //Debug.Log ("me he cargado a alguien");
            //Si lo destruimos la camara no tiene a quien seguir y chifla,
            //Por eso mejor se desactiva del juego.
            //FIN DEL JUEGO.MARCADORES ETC
            //				} else if (other.tag == "Justin") {
            //						GameObject personaje = GameObject.Find ("JustinBerenjena");
            //		
            //						Destroy(personaje);
            //						//personaje.SetActive (false);
        }
        else if (other.tag == "Justin") { 
        personajes = GameObject.FindGameObjectsWithTag("Justin");
        foreach (GameObject personaje in personajes)
        {
            personaje.SetActive(false);
        }
            personajes = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject personaje in personajes)
            {
                personaje.SetActive(false);
            }

            NotificationCenter.DefaultCenter().PostNotification(this, "JustinMuere");


        }
        else
            Destroy(other.gameObject);
	}
}

