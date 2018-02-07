using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    //private FadeInOutScript tut1;
    private EncounterUI pew;
    private EncounterSystem pewpew;
	// Use this for initialization
    private bool tut1;
    private bool tut2;
    private bool tut3;
    private bool tut4;
	void Start () {
        pew = GameObject.FindGameObjectWithTag("UIEncounter").GetComponent<EncounterUI>();
        pewpew = GameObject.FindGameObjectWithTag("EncounterSystem").GetComponent<EncounterSystem>();
        tut1 = tut2 = tut3 = tut4 = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (tutorial1.activeSelf && tutorial1.GetComponent<FadeInOutScript>().alpha >= 0.7f && !tut1)
        {
            if (Time.timeScale == 1.0f)
                pew.pause = true;

            if (Input.GetMouseButtonDown(0))
            {
                pew.pause = false;
                tut1 = true;
                tutorial1.SetActive(false);
            }
        }
        if (pewpew.testtyy && !tut2)
            tutorial2.SetActive(true);
        if (tutorial2.GetComponent<FadeInOutScript>().alpha >= 0.7f && !tut2 && pewpew.testtyy)
        {
            if (Time.timeScale == 1.0f)
                pew.pause = true;

            if (Input.GetMouseButtonDown(0))
            {
                pew.pause = false;
                tut2 = true;
                tutorial2.SetActive(false);
            }
        }
        if (pewpew.uhohuhohlowhp && !tut3)
            tutorial3.SetActive(true);
        if (tutorial3.GetComponent<FadeInOutScript>().alpha >= 0.7f && !tut3 && pewpew.uhohuhohlowhp)
        {
            if (Time.timeScale == 1.0f)
                pew.pause = true;

            if (Input.GetMouseButtonDown(0))
            {
                pew.pause = false;
                tut3 = true;
                tutorial3.SetActive(false);
            }
        }

        if (pewpew.wewewoww && !tut4)
            tutorial4.SetActive(true);
        if (tutorial4.GetComponent<FadeInOutScript>().alpha >= 0.7f && !tut4 && pewpew.wewewoww)
        {
            if (Time.timeScale == 1.0f)
                pew.pause = true;

            if (Input.GetMouseButtonDown(0))
            {
                pew.pause = false;
                tut4 = true;
                tutorial4.SetActive(false);
            }
        }

	}
}
