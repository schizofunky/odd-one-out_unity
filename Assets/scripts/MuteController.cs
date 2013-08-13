using UnityEngine;
using System.Collections;

public class MuteController : MonoBehaviour {

	public static bool muted = false;
	public Texture mutedTexture;
	public Texture unmutedTexture;
	// Use this for initialization
	void Start () {
		updateMuteSettings();	
	}

	void OnMouseDown(){
		muted = !muted;
		updateMuteSettings();
	}

	private void updateMuteSettings(){
		if(muted){
			GetComponent<MeshRenderer>().materials[0].mainTexture = mutedTexture;
			AudioListener.volume = 0;
		}
		else{
			GetComponent<MeshRenderer>().materials[0].mainTexture = unmutedTexture;
			AudioListener.volume = 1;	
		}	
	}
}
