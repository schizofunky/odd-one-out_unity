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

	public void onCorrectClick(){
		updateScore();
		destroyLevel();
		createLevel();	
	}

	public void onWrongClick(){
		reduceLives();	
		destroyLevel();
		createLevel();	
	}

	private void createLevel(){
		currentRobot = getRobotType();
		timeLimit = 100;
		imagesCreated = 0;
		Vector2[] levelCoordinates = getLevelCoordinates();
		imagesForLevel = new GameObject[levelCoordinates.Length];
		int badIndex = GetRandomIndex(levelCoordinates.Length);
		for (int imageCounter = 0; imageCounter < levelCoordinates.Length; imageCounter++){
			if(imageCounter == badIndex){
				createImageObject(levelCoordinates[imageCounter],"Bad"+getImageDifficulty());
			}
			else{
				createImageObject(levelCoordinates[imageCounter],"Good");	
			}
		}
	}

	private string getImageDifficulty(){
		string[] difficulties = {"Easy","Medium","Hard"};
		return difficulties[GetRandomIndex(difficulties.Length)];
	}

	private int getRobotType(){
		return 1+GetRandomIndex(10);
	}

	private Vector2[] getLevelCoordinates(){
		Vector2[] threeImages = {new Vector2(-3,1),new Vector2(0,1),new Vector2(3,1)};
		Vector2[] fourImages = {new Vector2(-4,3),new Vector2(-4,-1),new Vector2(4,3),new Vector2(4,-1)};
		Vector2[][] levelLists = {threeImages,fourImages};
		return levelLists[GetRandomIndex(levelLists.Length)];
	}

	private void destroyLevel(){
		//need to destroy all of the robots we have created
		for(int i = 0; i < imagesCreated; i++)
		{
			Destroy(imagesForLevel[i]);
			imagesForLevel[i] = null;	
		}
		imagesForLevel = null;
	}


	private void createImageObject(Vector2 coordinates, string type){
		GameObject go = Instantiate(imageHolder) as GameObject;
		go.transform.position = new Vector3(coordinates.x, coordinates.y, 0);
		print("Creating:"+currentRobot+type);
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

	private void updateScore(){

	}

	private void reduceLives(){
		lives--;
		if(lives <= 0){
			gameOver = true;
		}
	}

	private int GetRandomIndex(int length){
		return (int) Mathf.Round(Random.value * (length-1));
	}
}
