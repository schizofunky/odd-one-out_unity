using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    private float position = 100;
    private Vector2 pos = new Vector2(604,152.5f);
    private Vector2 size  = new Vector2(2.83f,14.5f);
    private DifficultyController difficultyController;
    public GUISkin loadingSkin;

    void Start(){
        difficultyController = gameObject.GetComponent<DifficultyController>();
    }
     
    void OnGUI(){
	    GUI.skin = loadingSkin;
	    GUI.Box(new Rect(pos.x,pos.y, size.x*position, size.y),"");     
    }

    public void reset(){
    	position = 100;
    }
     
    void Update()
    {
    	position -= difficultyController.getTimeDecaySpeed();
    }
}
