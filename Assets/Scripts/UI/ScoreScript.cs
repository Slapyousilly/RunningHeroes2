using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private GameManager scoringPew;
    public Text currentScore;
    public Text highScore;
    private DayNightScript scococo;

	// Use this for initialization
	void Start () {
        scoringPew = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scococo = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightScript>();
	}
	
	// Update is called once per frame
	void Update () {
        highScore.text = "Highscore: " + scoringPew.HighScore;
        currentScore.text = "Score: " + scococo.scoreEarned;
	}
}
