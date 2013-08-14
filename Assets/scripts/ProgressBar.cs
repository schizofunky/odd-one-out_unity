using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public float position = 100;
    public Vector2 pos = new Vector2(316.5f,152.5f);
    private Vector2 size  = new Vector2(2.83f,14.5f);
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
