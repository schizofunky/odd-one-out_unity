using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int maximumLives = 5;
	public GameObject imageHolder;

	private int lives;
	private int currentRobot;
	private int imagesCreated;
	private bool gameOver;
	private float timeLimit;
	private GameObject[] imagesForLevel;

	// Use this for initialization
	void Start () {
		lives = maximumLives;
		gameOver = false;
		createLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver){
			Application.LoadLevel("GameOver");
		}
		else{
			timeLimit -= 0.2f;

			if(timeLimit <= 0){
				reduceLives();
				destroyLevel();
				createLevel();
			}
		}
	}

	void createLevel(){
		currentRobot = 4;
		timeLimit = 100;
		int imagesToCreate = 3;
		imagesForLevel = new GameObject[imagesToCreate];
		imagesCreated = 0;
		Vector2[] levelCoordinates = {new Vector2(-3,1),new Vector2(0,1),new Vector2(3,1)};
		int badIndex = (int) Mathf.Round(Random.Range(0,levelCoordinates.Length-1));
		for (int imageCounter = 0; imageCounter < levelCoordinates.Length; imageCounter++){
			if(imageCounter == badIndex){
				createImageObject(levelCoordinates[imageCounter],"BadEasy");
			}
			else{
				createImageObject(levelCoordinates[imageCounter],"Good");	
			}
		}
	}

	void destroyLevel(){
		//need to destroy all of the robots we have created
		for(int i = 0; i < imagesCreated; i++)
		{
			Destroy(imagesForLevel[i]);
			imagesForLevel[i] = null;	
		}
		imagesForLevel = null;
	}


	void createImageObject(Vector2 coordinates, string type){
		GameObject go = Instantiate(imageHolder) as GameObject;
		go.transform.position = new Vector3(coordinates.x, coordinates.y, 0);
		go.renderer.material.mainTexture = Resources.Load("Robot"+currentRobot+type) as Texture;
		if(type != "Good"){
			go.GetComponent<ImageHandler>().isCorrect = true;
		}
		ImageHandler.MouseHandler correctHandler = onCorrectClick;
		ImageHandler.MouseHandler wrongHandler = onWrongClick;
		go.GetComponent<ImageHandler>().onCorrectClick = correctHandler;
		go.GetComponent<ImageHandler>().onWrongClick = wrongHandler;
		imagesForLevel[imagesCreated] = go;
		imagesCreated++;
	}

	public void onCorrectClick(){
		updateScore();
		destroyLevel();
		createLevel();	
	}

	void updateScore(){

	}

	public void onWrongClick(){
		reduceLives();	
		destroyLevel();
		createLevel();	
	}

	void reduceLives(){
		lives--;
		if(lives <= 0){
			gameOver = true;
		}
	}
}
