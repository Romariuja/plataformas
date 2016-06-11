using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public AudioClip GameOverClip;

	public GameObject CamaraGameOver;
	// Use this for initialization
	void Start () {
	NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
	}
	void PersonajeMuere(Notification notificacion){
		GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().clip = GameOverClip;
		GetComponent<AudioSource> ().loop = false;
		GetComponent<AudioSource> ().volume = 0.05f;
		GetComponent<AudioSource>().Play ();
		CamaraGameOver.SetActive (true);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
