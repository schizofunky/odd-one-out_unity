using UnityEngine;
using System.Collections;

public class ImageHandler : MonoBehaviour {
	public bool isCorrect = false;
	public delegate void MouseHandler();
	public MouseHandler onCorrectClick;
	public MouseHandler onWrongClick;
	//public LevelController levelController;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() 
	{
		if(isCorrect)
		{
			onCorrectClick();
		}
		else{
			onWrongClick();
		}
	}
}
