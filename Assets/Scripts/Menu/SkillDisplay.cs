using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour {

    public Text theText;

    private GameManager NANI;

	// Use this for initialization
	void Start () {
        NANI = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

    void Update()
    {
        if (gameObject.name == "StunDisplay")
        {
            theText.text = "Stuns Enemies for " + NANI.stunDur + " Seconds.";
        }
        else if (gameObject.name == "StrikeDisplay")
        {
            theText.text = "Strikes Enemies base on X" + NANI.strikeMultiplier + " of your base damage.";
        }
        else if (gameObject.name == "BlockDisplay")
        {
            theText.text = "Reduces Enemies Damage by Half for " + NANI.dmgRedDur + " Seconds.";
        }
        else if (gameObject.name == "SkillPoints")
        {
            theText.text = "SP: " + NANI.skillPoints;
        }
        else
        {
            theText.text = "Error 404";
        }
    }
}
