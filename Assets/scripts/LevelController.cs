using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {


	public static int currentScore;//static so it can be accessed via the Game Over scene

	public int maximumLives = 5;
	public int gameOverDelay = 20;
	public GameObject imageHolder;
	public GameObject life;
	public GUIText scoreText;
	public AudioClip correctSfx;
	public AudioClip wrongSfx;

	private int lives;
	private int currentRobot;
	private int imagesCreated;
	private bool gameOver;
	private float timeLimit;
	private GameObject[] imagesForLevel;
	private GameObject[] lifeObjects;
	private AudioSource fx;
	private DifficultyController difficultyController;

	// Use this for initialization
	void Start () {
		fx = gameObject.AddComponent<AudioSource>();
		difficultyController = gameObject.GetComponent<DifficultyController>();
		lives = maximumLives;
		gameOver = false;
		currentScore = 0;		
		createLives();
		createLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver){
			if(gameOverDelay-- <= 0){
				Application.LoadLevel("GameOver");
			}
		}
		else{
			timeLimit -= difficultyController.getTimeDecaySpeed();

			if(timeLimit <= 0){
				onWrongClick();
			}
		}
	}

	public void onCorrectClick(){
		if(!gameOver){
			fx.clip = correctSfx;
			fx.Play();
			updateScore();
			createLevel();	
		}
	}

	public void onWrongClick(){
		if(!gameOver){
			fx.clip = wrongSfx;
			fx.Play();
			reduceLives();	
			if(!gameOver){
				createLevel();	
			}
		}	
	}

	private void createLives(){
		lifeObjects = new GameObject[maximumLives];
		for(int lifeCounter = 0; lifeCounter < maximumLives; lifeCounter++){
			GameObject go = Instantiate(life) as GameObject;
			go.transform.position = new Vector3(14.89f, -5.4f+(lifeCounter*2.0f), 0);
			lifeObjects[lifeCounter] = go;
		}
	}

	private void createLevel(){
		destroyLevel();
		currentRobot = getRobotType();
		timeLimit = 100;
		imagesCreated = 0;
		Vector2[] levelCoordinates = difficultyController.getLevelCoordinates();
		imagesForLevel = new GameObject[levelCoordinates.Length];
		int badIndex = GetRandomIndex(levelCoordinates.Length);
		for (int imageCounter = 0; imageCounter < levelCoordinates.Length; imageCounter++){
			if(imageCounter == badIndex){
				createImageObject(levelCoordinates[imageCounter],"Bad"+difficultyController.getOddAsset());
			}
			else{
				createImageObject(levelCoordinates[imageCounter],"Good");	
			}
		}
		gameObject.GetComponent<ProgressBar>().reset();
	}

	/*private string getImageDifficulty(){
		string[] difficulties = {"Easy","Medium","Hard"};
		return difficulties[GetRandomIndex(difficulties.Length)];
	}*/

	private int getRobotType(){
		return 1+GetRandomIndex(10);
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
		if(difficultyController.canImagesRotate()){
			go.transform.RotateAround(go.transform.position, go.transform.forward, Random.value * 360f);
		}
		ImageHandler.MouseHandler correctHandler = onCorrectClick;
		ImageHandler.MouseHandler wrongHandler = onWrongClick;
		go.GetComponent<ImageHandler>().onCorrectClick = correctHandler;
		go.GetComponent<ImageHandler>().onWrongClick = wrongHandler;
		imagesForLevel[imagesCreated] = go;
		imagesCreated++;
	}

	private void updateScore(){
		currentScore += (int) Mathf.Round(1*timeLimit);
		gameObject.GetComponent<DifficultyController>().updateDifficulty(currentScore);
		scoreText.text = currentScore.ToString();
	}

	private void reduceLives(){
		lives--;
		lifeObjects[lives].GetComponent<HeartAnimController>().loseLife();
		if(lives <= 0){
			gameOver = true;
		}
	}

	private int GetRandomIndex(int length){
		return (int) Mathf.Round(Random.value * (length-1));
	}
}
