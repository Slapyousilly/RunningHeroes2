using UnityEngine;
using System.Collections;

public class PlayerScrpt : EntityBase {

    private Animator anim;//! Animator of Player to set bool/triggers in Updates

    public enum PLAYER_TYPE
    {
        P_KNIGHT = 0,
        P_ARCHER,
        P_MAGE
    }

    public PLAYER_TYPE p_Type = PLAYER_TYPE.P_KNIGHT;

	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        m_isDead = false;
        m_HP = 1000;
        m_maxHP = m_HP;
        m_atkSpd = 1.1f;
        m_flinchDt = 0.5f;
        m_Name = "Hero";
        m_Damage = 200;
        m_resistance = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(state);
        switch (state.gameState)
        {
            case GameState.GAMESTATE.GS_PLAY:
                Run();
                break;
            case GameState.GAMESTATE.GS_ENCOUNTER:
                RunFSM();
                Idle();
                break;
            case GameState.GAMESTATE.GS_VICTORY:
                break;
            case GameState.GAMESTATE.GS_DEFEAT:
                break;
            case GameState.GAMESTATE.GS_START:
                break;
            case GameState.GAMESTATE.GS_TUTORIAL:
                break;
        }
        //RunFSM(GetComponent<GameState>().gameState);
        //anim.SetTrigger("RUN");
    }

    public override void RunFSM()
    {

    }

    void Idle()
    {
        anim.SetTrigger("IDLE");
    }
    void Run()
    {
        //if (anim.name != "RUN")
        //    anim.Stop();

        anim.SetTrigger("RUN");
    }
    void Skill1()
    {
        anim.SetTrigger("SKILL1");
    }
    void Skill2()
    {
        anim.SetTrigger("SKILL2");
    }
    void Attack()
    {
        anim.SetTrigger("ATTACK");
    }
}
