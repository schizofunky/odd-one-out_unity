using UnityEngine;
using System.Collections;

public class DifficultyController : MonoBehaviour {
	private uint levelId = 0;
	private int imageDifficulty = 0;
	private float timeDecaySpeed = 0.2f;
	private bool imagesCanRotate = false;
	private string[] assetDifficulties = {"Easy","Medium","Hard"};
	private Vector2[] threeImages = {new Vector2(-5,1),new Vector2(0,1),new Vector2(5,1)};
	private	Vector2[] fourImages = {new Vector2(-4,3),new Vector2(-4,-1),new Vector2(4,3),new Vector2(4,-1)};
	private	Vector2[] nineImages = {new Vector2(-5,1),new Vector2(0,1),new Vector2(5,1),new Vector2(-5,-3.3f),new Vector2(0,-3.3f),new Vector2(5,-3.3f),new Vector2(-5,5.3f),new Vector2(0,5.3f),new Vector2(5,5.3f)};
	private	Vector2[] sixteenImages = {new Vector2(-5.8f,2.7f),new Vector2(-2f,2.7f),new Vector2(2f,2.7f),new Vector2(5.7f,2.7f),new Vector2(-5.8f,-0.8f),new Vector2(-2f,-0.8f),new Vector2(2f,-0.8f),new Vector2(5.7f,-0.8f),new Vector2(-5.8f,-4.2f),new Vector2(-2f,-4.2f),new Vector2(2f,-4.2f),new Vector2(5.7f,-4.2f),new Vector2(-5.8f,6.2f),new Vector2(-2f,6.2f),new Vector2(2f,6.2f),new Vector2(5.7f,6.2f)};
	


	// Use this for initialization
	void Start () {

		levelId = 0; //refers to the number of images that will appear and their x,y co-ordinates
		timeDecaySpeed = 0.2f; //how fast the time bar decreases
		imagesCanRotate = false; 
		imageDifficulty = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2[] getLevelCoordinates(){
		Vector2[][] levelLists = {threeImages,fourImages,nineImages,sixteenImages};
		return levelLists[levelId];
	}

	public void updateDifficulty(int score){
		//uses the players current score to activate and increase difficulty values
		if(score > 1800){
			imageDifficulty = GetRandomIndex(3);
			imagesCanRotate = true;
			increaseDecaySpeed(0.01f);
			levelId = 3;
		}
		else if(score > 1500){
			imageDifficulty = 2;
			increaseDecaySpeed(0.02f);
			levelId = 3;
		}
		else if(score > 800){
			imageDifficulty = 1;
			increaseDecaySpeed(0.015f);
			levelId = 2;	
		}
		else if(score > 300){
			imageDifficulty = 0;
			increaseDecaySpeed(0.01f);
			levelId = 1;	
		}
	}

	public string getOddAsset(){
		return assetDifficulties[imageDifficulty];
	}

	public float getTimeDecaySpeed(){
		return timeDecaySpeed;
	}

	public void increaseDecaySpeed(float amount){
		timeDecaySpeed += amount;
	}

	public bool canImagesRotate(){
		return imagesCanRotate;
	}

	private int GetRandomIndex(int length){
		return (int) Mathf.Round(Random.value * (length-1));
	}
}
