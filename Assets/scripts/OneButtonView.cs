using UnityEngine;
using System.Collections;

public class OneButtonView : MonoBehaviour {

	public string levelToLoad;
	public Texture rolloverBackground;
	public Texture rolloverText;
	public GameObject buttonTextObject;
	public GameObject buttonBGObject;
	private Texture rolloutBackground;
	private Texture rolloutText;

	// Use this for initialization
	void Start () {
		rolloutBackground = buttonBGObject.GetComponent<MeshRenderer>().materials[0].mainTexture;
		rolloutText = buttonTextObject.GetComponent<MeshRenderer>().materials[0].mainTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		GetComponent<AudioSource>().Play();
		Application.LoadLevel(levelToLoad);
	}
	
	void OnMouseOver() 
	{
		buttonTextObject.GetComponent<MeshRenderer>().materials[0].mainTexture = rolloverText;	
		buttonBGObject.GetComponent<MeshRenderer>().materials[0].mainTexture = rolloverBackground;
	}
	
	void OnMouseExit() 
	{
		buttonTextObject.GetComponent<MeshRenderer>().materials[0].mainTexture = rolloutText;	
		buttonBGObject.GetComponent<MeshRenderer>().materials[0].mainTexture = rolloutBackground;	
	}
}
