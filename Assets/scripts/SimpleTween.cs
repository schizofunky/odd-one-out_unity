using UnityEngine;
using System.Collections;

public class SimpleTween : MonoBehaviour {

	public int iterations = 0;
	public Vector3 speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(iterations > 0)
		{
			iterations--;
			gameObject.transform.Translate(speed.x,speed.y,speed.z);
		}	
	}
}
