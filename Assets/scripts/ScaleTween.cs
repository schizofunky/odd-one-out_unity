using UnityEngine;
using System.Collections;

public class ScaleTween : MonoBehaviour {

	public int interations;
	public Vector3 scaleSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(interations > 0){
			interations--;
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x += scaleSpeed.x;
			newScale.y += scaleSpeed.y;
			newScale.z += scaleSpeed.z;
			gameObject.transform.localScale = newScale; 
		}
	}
}
