using UnityEngine;
using System.Collections;

public class Item_guitarra : MonoBehaviour {
	private Animator animator;
	public bool Guitarra=false;
	//public AudioClip itemSoundClip;
	//public float itemSoundVolume= 1f;
	private int puntosGanados=15;
	// Use this for initialization
	public GameObject etiquetados;
	void Start () {
		animator = GetComponent<Animator>();
	}

	void FixedUpdate(){
		//animator.SetBool("ConGuitarra", Guitarra);
		//animator.SetBool("ConGuitarra", true);
		}
			// Update is called once per frame
	void Update () {
	}
	void Autodestruccion(){
		Destroy(gameObject);

		GameObject[] objects = GameObject.FindGameObjectsWithTag( "Guitarra" );
		foreach( GameObject go in objects )
		{
			Destroy( go );
		}
		Guitarra=false;
		animator.SetBool("ConGuitarra", Guitarra);
		//etiquetados = GameObject.FindWithTag("Guitarra");
//		GameObject[] GameObjects = (FindObjectsWithTag("Guitarra");
//	    for (int i = 0; i < GameObjects.Length; i++)
//		{
//			Destroy(GameObjects[i]);
//		}
	}




	//IEnumerator OnTriggerEnter2D(Collider2D collider){
 void OnTriggerEnter2D(Collider2D collider){	
	//Debug.Log("Tocado");
		if (collider.tag == "Player" ) {
			NotificationCenter.DefaultCenter ().PostNotification (this, "IncrementarPuntos", puntosGanados);
			NotificationCenter.DefaultCenter ().PostNotification (this, "PillaGuitarra", puntosGanados);

			//AudioSource.PlayClipAtPoint(itemSoundClip,  Camera.main.GetComponent<Transform>().position,itemSoundVolume);
			//GetComponent<AudioSource>().Play ();
			Guitarra=true;
			animator.SetBool("ConGuitarra", Guitarra);
			Invoke ("Autodestruccion", 4f);
			gameObject.collider2D.enabled=false;

		}
		//Destroy(gameObject);
	}
}