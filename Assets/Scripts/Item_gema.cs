using UnityEngine;
using System.Collections;

public class Item_gema : MonoBehaviour {
	private int puntosGanados=5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D collider){
				//Debug.Log("Tocado");
		if (collider.tag == "Player") {
						NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntos", puntosGanados);
				}
			Destroy(gameObject);
	}
}
