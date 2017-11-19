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

    public GameObject Monster;

    public int spawnCount;

    private EncounterUI UIUIThing;

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
    public List<ENCOUNTERS> _encounters = new List<ENCOUNTERS>(); // List of Encounters

    // _encounters = new ENCOUNTERS[]{E_WEAPONUP, E_REPAIR};

	// Use this for initialization
	void Start () {
        spawnCount = 0;
        doEncounterCheck = true;
        m_gstate = GameStateThing.GetComponent<GameState>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();  // Finding player and getting details from player directly
        UIUIThing = GameObject.FindGameObjectWithTag("UIEncounter").GetComponent<EncounterUI>();
        _encounters.Add(ENCOUNTERS.E_WEAPONUP); //= new ENCOUNTERS[5] { ENCOUNTERS.E_WEAPONUP, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_SHRINE, ENCOUNTERS.E_ARMORUP };
        _encounters.Add(ENCOUNTERS.E_MONSTERS);
        _encounters.Add(ENCOUNTERS.E_MONSTERS);
        _encounters.Add(ENCOUNTERS.E_SHRINE);
        _encounters.Add(ENCOUNTERS.E_ARMORUP);

        //UIUIThing.InitialRun();
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

    void showRespectiveText()
    {
        switch (CurrEncounterCheck())
        {
            case ENCOUNTERS.E_ARMORUP:
                feedbackText.text = "Tap to Upgrade Armor";
                if (Input.GetMouseButtonDown(0))
                {
                    int newMaxHP = (int)((float)m_Player.m_maxHP * 1.2f);
                    m_Player.m_maxHP = newMaxHP;
                    feedbackText.text = "Armor Upgraded!";
                    m_Player.m_HP = m_Player.m_maxHP;
                }
                break;
            case ENCOUNTERS.E_WEAPONUP:
                feedbackText.text = "Tap to Upgrade Weapon";
                if (Input.GetMouseButtonDown(0))
                {
                    feedbackText.text = "Weapon Upgraded!";
                    int newDMG = (int)((float)m_Player.m_Damage * 1.2f);
                    m_Player.m_Damage = newDMG;
                }
                break;
            case ENCOUNTERS.E_REPAIR:
                feedbackText.text = "Tap to Repair Armor";
                if (Input.GetMouseButtonDown(0))
                {
                    feedbackText.text = "Armor Repaired!";
                    m_Player.m_HP = m_Player.m_maxHP;
                }
                break;
            case ENCOUNTERS.E_SHRINE:
                feedbackText.text = "Tap to Increase Lifespan";
                if (Input.GetMouseButtonDown(0))
                {
                    feedbackText.text = "Lifespan Recovered!";
                    m_Player.m_lifeSpan = 100;
                }
                break;
            case ENCOUNTERS.E_MONSTERS:
                feedbackText.text = "Use your skills!";
                m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                if (spawnCount < 1)
                {
                    Instantiate(Monster, new Vector3(10, -2.2f, 0), Quaternion.identity);   //Spawn Monster
                    spawnCount++;
                }
                break;
            case ENCOUNTERS.E_BOSS:
                feedbackText.text = "Use your skills!";
                m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                break;
        }
    }


    #region Encounter Checks
    private void AddEncounter(ENCOUNTERS enc)
    {
        _encounters.Add(enc);
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

    void EncounterReset()
    {
        spawnCount = 0;
        _encounters.RemoveAt(0);
        ENCOUNTERS nextToAdd = NextEncounterAdd();
        Debug.Log(nextToAdd);
        _encounters.Add(nextToAdd);
        UIUIThing.AddUINextEncounter(nextToAdd);
        UIUIThing.RemoveUIEncounter();
        feedbackText.text = "";
        m_nextEncounterDt = 2.0f;
        m_bufferdt = 2.0f;
    }

    public ENCOUNTERS NextEncounterAdd()
    {
        _encounters.Contains(ENCOUNTERS.E_SHRINE); // Returns true if it does contain inside List.
        //_encounters[0];
        if (_encounters.Count < 5)
        {
            // Add new encounter logic
            // Determine next encounter

            if (m_Player.m_lifeSpan < 40.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
                return ENCOUNTERS.E_SHRINE;

            float randfVal = Random.RandomRange(0.0f, 1.2f);
            Debug.Log(randfVal);

            if (randfVal <= 0.2f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
                return ENCOUNTERS.E_SHRINE;
            else
                while (randfVal <= 0.2f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.2f && randfVal <= 0.4f)
                return ENCOUNTERS.E_MONSTERS;

            if (randfVal > 0.4f && randfVal <= 0.6f && !_encounters.Contains(ENCOUNTERS.E_WEAPONUP))
                return ENCOUNTERS.E_WEAPONUP;
            else
                while (randfVal > 0.4f && randfVal <= 0.6f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.6f && randfVal <= 0.8f)
                return ENCOUNTERS.E_ARMORUP;
            else
                while (randfVal > 0.6f && randfVal <= 0.8f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.8f && randfVal <= 1.0f && m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
                return ENCOUNTERS.E_REPAIR;
            else
                while (randfVal > 0.8f && randfVal <= 1.0f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);


            //if (m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
            //    return ENCOUNTERS.E_REPAIR;
            //else
            //    return ENCOUNTERS.E_MONSTERS;
        }

        return ENCOUNTERS.E_MONSTERS;
    }

    public ENCOUNTERS CurrEncounterCheck()
    {
        return _encounters[0];
    }

    protected List<ENCOUNTERS> EncounterList()
    {
        return _encounters;
    }
    #endregion 
}
