using UnityEngine;
using System.Collections;

public class HeartAnimController : MonoBehaviour {
	public bool lost = false;
	private float alpha = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(lost && alpha > 0){
			alpha -=0.1f;
			Color newColor = new Color(1,1,1,alpha);
			transform.renderer.material.color = newColor;
		}
	
	}

	public void loseLife(){
		lost = true;
	}
}
