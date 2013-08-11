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
		createImageObject(-3,1,"Good");
		createImageObject(0,1,"Good");
		createImageObject(3,1,"BadEasy");
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


	void createImageObject(int x, int y, string type){
		GameObject go = Instantiate(imageHolder) as GameObject;
		go.transform.position = new Vector3(x, y, 0);
		go.renderer.material.mainTexture = Resources.Load("Robot"+currentRobot+type) as Texture;
		imagesForLevel[imagesCreated] = go;
		imagesCreated++;
	}

	void onCorrectImageChosen(){
		updateScore();
		destroyLevel();
		createLevel();	
	}

	void updateScore(){

	}

	void onWrongImageChosen(){
		reduceLives();	
		destroyLevel();
		createLevel();	
	}

	void reduceLives(){
		print("you just lost a life "+lives);
		lives--;
		if(lives <= 0){
			print("Game Over");
			gameOver = true;
		}
	}
}
