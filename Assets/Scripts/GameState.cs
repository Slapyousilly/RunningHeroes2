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
    }

    public GAMESTATE gameState;

    public GameObject BackgroundScroll;

	// Use this for initialization
	void Start () {
        gameState = GAMESTATE.GS_START;
	}
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case GAMESTATE.GS_PLAY:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = true;
                break;
            case GAMESTATE.GS_ENCOUNTER:
                BackgroundScroll.GetComponent<BackgroundScrolling>().b_Scrolling = false;
                    //b_Scrolling = false;
                break;
            case GAMESTATE.GS_VICTORY:
                break;
            case GAMESTATE.GS_DEFEAT:
                break;
            case GAMESTATE.GS_START:
                break;
            case GAMESTATE.GS_TUTORIAL:
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
