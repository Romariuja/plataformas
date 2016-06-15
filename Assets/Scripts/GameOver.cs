using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public AudioClip GameOverClip;

	public GameObject CamaraGameOver;
    public GameObject CamaraGameWinner;
    // Use this for initialization
    void Start () {
	NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
        NotificationCenter.DefaultCenter().AddObserver(this, "JustinMuere");
    }
	void PersonajeMuere(Notification notificacion){
		GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().clip = GameOverClip;
		GetComponent<AudioSource> ().loop = false;
		GetComponent<AudioSource> ().volume = 0.05f;
		GetComponent<AudioSource>().Play ();
		CamaraGameOver.SetActive (true);

	}


    void JustinMuere(Notification notificacion)
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = GameOverClip;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().volume = 0.05f;
        GetComponent<AudioSource>().Play();
        CamaraGameWinner.SetActive(true);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
