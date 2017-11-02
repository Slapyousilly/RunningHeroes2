using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EncounterSystem : MonoBehaviour {

    // Check for Player status and give next encounter to be the one to benefit more (still chance)

    public PlayerScrpt m_Player;

    public enum ENCOUNTERS
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
    List<ENCOUNTERS> _encounters = new List<ENCOUNTERS>();

    // _encounters = new ENCOUNTERS[]{E_WEAPONUP, E_REPAIR};

	// Use this for initialization
	void Start () {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();
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
            if (m_Player.m_lifeSpan < 25.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
            {
                _encounters.Add(ENCOUNTERS.E_SHRINE);
            }
            // Determine next encounter

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void AddEncounter(ENCOUNTERS enc)
    {
        _encounters.Add(enc);
    }
}
