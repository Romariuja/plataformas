using UnityEngine;
using System.Collections;

public class Item_calavera : MonoBehaviour {

	private int puntosGanados=1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
//	void OnTriggerEnter2D(Collider2D collider){
//		Debug.Log("Tocado");
//		if (collider.tag == "Player") {
//			NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntosCalavera", puntosGanados);
//		}
//		Destroy(gameObject);
//	}
//	
//	}

	 void OnTriggerEnter2D(Collider2D collider){	
	//Debug.Log("Tocado");
		if (collider.tag == "Player" ) {
					NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntosCalavera", puntosGanados);

			//AudioSource.PlayClipAtPoint(itemSoundClip,  Camera.main.GetComponent<Transform>().position,itemSoundVolume);
			//GetComponent<AudioSource>().Play ();


		}
		Destroy(gameObject);
	}
}
