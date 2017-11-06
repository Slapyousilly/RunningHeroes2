using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EncounterSystem : MonoBehaviour {

    // Check for Player status and give next encounter to be the one to benefit more (still chance)

    public PlayerScrpt m_Player;            // Player Script object.
    private float m_RandomChance;           // Random factor to determine next encounter
    private float m_Encounterdt;            // current encounter time
    public float m_nextEncounterDt = 4.0f;  // fixed next encounter time

    public float m_bufferdt = 4.0f;
    private float m_nextbufferDt;     

    public Text feedbackText;               // Text to ask player to tap
    public bool doEncounterCheck;

    public GameObject GameStateThing;
    private GameState m_gstate;

    public enum ENCOUNTERS                  // Encounters Players will get
    {
        E_WEAPONUP,
        E_ARMORUP,
        E_REPAIR,
        E_SHRINE,
        E_MONSTERS,
        E_BOSS,
        E_END,
    }
    //public ENCOUNTERS[] _encounters; //
    List<ENCOUNTERS> _encounters = new List<ENCOUNTERS>(); // List of Encounters

    // _encounters = new ENCOUNTERS[]{E_WEAPONUP, E_REPAIR};

	// Use this for initialization
	void Start () {
        doEncounterCheck = true;
        m_gstate = GameStateThing.GetComponent<GameState>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();  // Finding player and getting details from player directly
        _encounters.Add(ENCOUNTERS.E_WEAPONUP); //= new ENCOUNTERS[5] { ENCOUNTERS.E_WEAPONUP, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_SHRINE, ENCOUNTERS.E_ARMORUP };
        _encounters.Add(ENCOUNTERS.E_MONSTERS);
        _encounters.Add(ENCOUNTERS.E_MONSTERS);
        _encounters.Add(ENCOUNTERS.E_SHRINE);
        _encounters.Add(ENCOUNTERS.E_ARMORUP);
    }

    void EncounterCheck()
    {
        _encounters.Contains(ENCOUNTERS.E_SHRINE); // Returns true if it does contain inside List.
        //_encounters[0];
        if (_encounters.Count < 5)
        {
            // Add new encounter logic
            if (m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
            {
                _encounters.Add(ENCOUNTERS.E_REPAIR);
            }
            if (m_Player.m_lifeSpan < 30.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
            {
                _encounters.Add(ENCOUNTERS.E_SHRINE);
            }
            // Determine next encounter
        }
    }

    ENCOUNTERS NextEncounterAdd()
    {
        _encounters.Contains(ENCOUNTERS.E_SHRINE); // Returns true if it does contain inside List.
        //_encounters[0];
        if (_encounters.Count < 5)
        {
            // Add new encounter logic
            // Determine next encounter
            if (m_Player.m_lifeSpan < 30.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
                return ENCOUNTERS.E_SHRINE;
            if (m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
                return ENCOUNTERS.E_REPAIR;
            else
                return ENCOUNTERS.E_MONSTERS;
        }
        else
            return ENCOUNTERS.E_MONSTERS;

        return ENCOUNTERS.E_END;
    }

    ENCOUNTERS CurrEncounterCheck()
    {
        return _encounters[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (doEncounterCheck)
        {
            //if (CurrEncounterCheck() != ENCOUNTERS.E_MONSTERS || CurrEncounterCheck() != ENCOUNTERS.E_BOSS)
            m_nextEncounterDt -= Time.deltaTime;

            if (m_nextEncounterDt <= 0.0f)
            {
                // do the Touch to be like "hey u got this encounter"
                // then after pass by let the encounter goneee
                if (m_bufferdt > 0.0f)
                {
                    m_bufferdt -= Time.deltaTime;
                    showRespectiveText();

                    if (m_bufferdt <= 0.0f)
                        EncounterReset();
                }
                //_encounters[0]
            }
        }
	}

    void EncounterReset()
    {
        _encounters.RemoveAt(0);
        Debug.Log(NextEncounterAdd());
        _encounters.Add(NextEncounterAdd());
        feedbackText.text = "";
        m_nextEncounterDt = 2.0f;
        m_bufferdt = 2.0f;
    }

    void showRespectiveText()
    {
        switch (CurrEncounterCheck())
        {
            case ENCOUNTERS.E_ARMORUP:
                feedbackText.text = "Tap to Upgrade Armor";
                break;
            case ENCOUNTERS.E_WEAPONUP:
                feedbackText.text = "Tap to Upgrade Weapon";
                break;
            case ENCOUNTERS.E_REPAIR:
                feedbackText.text = "Tap to Repair Armor";
                break;
            case ENCOUNTERS.E_SHRINE:
                feedbackText.text = "Tap to Increase Lifespan";
                break;
            case ENCOUNTERS.E_MONSTERS:
                feedbackText.text = "Use your skills!";
                m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                break;
            case ENCOUNTERS.E_BOSS:
                feedbackText.text = "Use your skills!";
                m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                break;
        }
    }

    private void AddEncounter(ENCOUNTERS enc)
    {
        _encounters.Add(enc);
    }
}
