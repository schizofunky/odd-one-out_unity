using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public GUIText scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = LevelController.currentScore.ToString();
	}
}
