using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
	public bool IniciarENMovimiento=false;
	public float velocidad=0f;
	private bool enMovimiento=false;
	private bool para=false;
	public float Tiempo_Ant=0;

	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeCorre");
		NotificationCenter.DefaultCenter ().AddObserver (this, "PersonajeMuere");
		NotificationCenter.DefaultCenter().AddObserver(this,"PersonajePara");
		if (IniciarENMovimiento) {
			enMovimiento=true;
		}
	}
	void PersonajeMuere(){
		enMovimiento = false;
		//Tiempo_Ant ++;
		para = true;
	}
	void PersonajeCorre(){
		enMovimiento = true;
		para = false;
		Tiempo_Ant ++;
	}

	void PersonajePara(){
	para = true;
		//Tiempo_Ant = Time.time;
}

	// Update is called once per frame
	void Update () {
		if (enMovimiento && !para) {
			renderer.material.mainTextureOffset = new Vector2 (velocidad*transform.position.x/100, 0);
		}
	}
}
