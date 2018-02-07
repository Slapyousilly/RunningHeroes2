using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightScript : MonoBehaviour {

    public float elapsedTime;      // Elapsed time before slowly set to night
    public float dayTime = 30.0f;          // Time a day should last (night and day)
    public GameObject DayNightpic;
    public Text DayText;
    private Image picAlpha;
    private Color c;
    private bool toggleTransition = false;
    private int dayPassed;                  // Starting Default to 1
    public int scoreEarned;

    public GameObject ScoreThing;
    private Text ScoreText;
	// Use this for initialization
	void Start () {
        ScoreText = ScoreThing.GetComponent<Text>();
        picAlpha = DayNightpic.GetComponent<Image>();
        c = picAlpha.color;
        dayPassed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        DayNightTransition();
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= dayTime)
        {
            elapsedTime = 0.0f;

            TriggerTransition();
        }
        if (Time.timeScale == 1)
            CalculatePoints();
	}

    void CalculatePoints()
    {
        scoreEarned += 5;
        ScoreText.text = scoreEarned.ToString();
    }

    void DayNightTransition()
    {
        if (toggleTransition)
        {
            //Debug.Log("IT'S NIGHT");
            if (c.a <= 0.55f)
                c.a += 0.001f;
            picAlpha.color = c;
        }
        else
        {
            //Debug.Log("IT'S MORNING");
            if (c.a > 0)
                c.a -= 0.001f;
            picAlpha.color = c;
        }
    }
    protected void TriggerTransition()
    {
        toggleTransition = !toggleTransition;
        if (!toggleTransition)
            dayPassed += 1;
        DayText.text = dayPassed.ToString();
    }

    public int getDaysPassed()
    {
        return dayPassed;
    }

    public void SetDayTen()
    {
        dayPassed = 10;
    }
    //void FixedUpdate()
    //{

    //}
}
