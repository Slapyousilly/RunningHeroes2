using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour {

    //private GameObject display;
    public GameObject textdisplay;
    private Text display;

    private float displayDt;
    private float oldXPos;
    public float newXPos = -465.0f;
    private bool isShown, isHidden;

	// Use this for initialization
	void Start () {
        // = this.gameObject;
        display = textdisplay.GetComponent<Text>();
        displayDt = 0.0f;
        oldXPos = -Screen.width * 0.25f;//gameObject.transform.position.x;
        newXPos = Screen.width * 0.25f;//-465.0f;
        isShown = false;
        isHidden = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (displayDt > 0)
        {
            displayDt -= Time.deltaTime;
            DisplayText();
        }
        else
            HideText();
	}

    public void TextToDisplay(string text, float dur)
    {
        display.text = text;
        displayDt = dur;
    }

    protected void DisplayText()
    {
        //transition out
        //gameObject.transform.position.x += Time.deltaTime;
        if (!isShown)
        {
            gameObject.transform.Translate(newXPos, 0, 0);
            isShown = true;
            isHidden = false;
        }
    }

    protected void HideText()
    {
        //transition back
        if (!isHidden)
        {
            gameObject.transform.Translate(oldXPos, 0, 0);
            isHidden = true;
            isShown = false;
        }
    }
}
