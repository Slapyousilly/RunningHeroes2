using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public enum GAMESTATE
    {
        GS_PLAY = 0,
        GS_ENCOUNTER,
        GS_VICTORY,
        GS_DEFEAT,
        GS_START,
        GS_TUTORIAL,
        GS_PAUSE,
        GS_LAST,
    }

    public GAMESTATE gameState;
    public bool m_tutorialdone;

    public GameObject BackgroundScroll;
    public GameObject EncounterThing;
    private EncounterSystem m_enc;
    public float delay = 0;

	// Use this for initialization
	void Start () {
        // for now put done
        m_tutorialdone = true;
        gameState = GAMESTATE.GS_START;
        m_enc = EncounterThing.GetComponent<EncounterSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_tutorialdone)
            gameState = GAMESTATE.GS_TUTORIAL;

        switch (gameState)
        {
            case GAMESTATE.GS_PLAY:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = true;
                m_enc.doEncounterCheck = true;
                delay = 0;
                break;
            case GAMESTATE.GS_ENCOUNTER:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                m_enc.doEncounterCheck = false;
                
                    //b_Scrolling = false;
                break;
            case GAMESTATE.GS_VICTORY:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                break;
            case GAMESTATE.GS_DEFEAT:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                break;
            case GAMESTATE.GS_START:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                m_enc.doEncounterCheck = false;
                
                delay += Time.fixedTime;
                if (delay > 2.0f)
                    gameState = GAMESTATE.GS_PLAY;
                break;
            case GAMESTATE.GS_TUTORIAL:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                //Trigger Tutorial Image
                break;
        }
	}

    public void GamePlay()
    {
        gameState = GAMESTATE.GS_PLAY;
    }
    
    public void RandomEncounter()
    {
        gameState = GAMESTATE.GS_ENCOUNTER;
    }
}
