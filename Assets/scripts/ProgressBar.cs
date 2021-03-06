using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public float position = 100;
    private Vector2 pos = new Vector2(360.32f,32f);
    private Vector2 size  = new Vector2(6.1f,25.7f);
    private DifficultyController difficultyController;
    public GUISkin loadingSkin;

    void Start(){
        difficultyController = gameObject.GetComponent<DifficultyController>();
    }
     
    void OnGUI(){
	    GUI.skin = loadingSkin;
        print(size.x*position);
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
