using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    public GameObject tutorial1;
    //private FadeInOutScript tut1;
    private EncounterUI pew;
	// Use this for initialization
    private bool tut1;
	void Start () {
        pew = GameObject.FindGameObjectWithTag("UIEncounter").GetComponent<EncounterUI>();
        tut1 = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (tutorial1.activeSelf && tutorial1.GetComponent<FadeInOutScript>().alpha >= 0.7f && !tut1)
        {
            Debug.Log("HEHEHE");
            if (Time.timeScale == 1.0f)
                pew.pause = true;

            if (Input.GetMouseButtonDown(0))
            {
                pew.pause = false;
                tut1 = true;
                tutorial1.SetActive(false);
            }
        }
	}
}
