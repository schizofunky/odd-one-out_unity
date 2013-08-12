using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int maximumLives = 5;
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
	private float currentScore;
	private GameObject[] imagesForLevel;
	private GameObject[] lifeObjects;
	private AudioSource fx;

	// Use this for initialization
	void Start () {
		fx = gameObject.AddComponent<AudioSource>();
		lives = maximumLives;
		gameOver = false;
		currentScore = 0;		
		createLives();
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
		fx.clip = correctSfx;
		fx.Play();
		updateScore();
		destroyLevel();
		createLevel();	
	}

	public void onWrongClick(){
		fx.clip = wrongSfx;
		fx.Play();
		reduceLives();	
		destroyLevel();
		createLevel();	
	}

	private void createLives(){
		lifeObjects = new GameObject[maximumLives];
		for(int lifeCounter = 0; lifeCounter < maximumLives; lifeCounter++){
			GameObject go = Instantiate(life) as GameObject;
			go.transform.position = new Vector3(7.12f, -3.4f+(lifeCounter*1.0f), 0);
			lifeObjects[lifeCounter] = go;
		}
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
		gameObject.GetComponent<ProgressBar>().reset();
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
		Vector2[] nineImages = {new Vector2(-3,1),new Vector2(0,1),new Vector2(3,1),new Vector2(-3,-1.3f),new Vector2(0,-1.3f),new Vector2(3,-1.3f),new Vector2(-3,3.3f),new Vector2(0,3.3f),new Vector2(3,3.3f)};
		Vector2[] sixteenImages = {new Vector2(-3.8f,2.1f),new Vector2(-1.3f,2.1f),new Vector2(1.2f,2.1f),new Vector2(3.7f,2.1f),new Vector2(-3.8f,0),new Vector2(-1.3f,0),new Vector2(1.2f,0),new Vector2(3.7f,0),new Vector2(-3.8f,-2.2f),new Vector2(-1.3f,-2.2f),new Vector2(1.2f,-2.2f),new Vector2(3.7f,-2.2f),new Vector2(-3.8f,4.2f),new Vector2(-1.3f,4.2f),new Vector2(1.2f,4.2f),new Vector2(3.7f,4.2f)};
		Vector2[][] levelLists = {threeImages,fourImages,nineImages,sixteenImages};
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
		currentScore += Mathf.Round(10*timeLimit);
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
