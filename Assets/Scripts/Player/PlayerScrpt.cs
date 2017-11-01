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

    public enum PLAYER_STATES
    {
        S_IDLE = 0,
        S_RUN,
        S_ATTACK,
        S_SKILL1,
        S_SKILL2,
        S_DIE,

        S_END,
    }

    public PLAYER_STATES p_State = PLAYER_STATES.S_IDLE;
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

        //Time.timeScale = 0; //can use to pause game or do shit.

        Debug.Log(state);
        if (state.gameState != GameState.GAMESTATE.GS_TUTORIAL)
            Time.timeScale = 1;
            //anim.speed = 1;

        if (m_HP > 0)
        {
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
                    Idle();
                    break;
                case GameState.GAMESTATE.GS_TUTORIAL:
                    //anim.speed = 0;
                    Time.timeScale = 0;
                    break;
            }
        }
        else
        {
            Die();
        }
        //RunFSM(GetComponent<GameState>().gameState);
        //anim.SetTrigger("RUN");
    }
    /*       S_IDLE = 0,
        S_RUN,
        S_ATTACK,
        S_SKILL1,
        S_SKILL2,
        S_DIE,*/
    public override void RunFSM()
    {
        if (m_HP <= 0)
        {
            Die();
        }
    }

    #region Animation Trigger States
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
    void Die()
    {
        anim.SetTrigger("DIE");
    }
    #endregion

    #region Player Targetting
    protected GameObject GetNearestTarget()
    {
        float closetdistance = 900;
        GameObject temp = null;
        GameObject go = GameObject.FindGameObjectWithTag("Enemy");
        float dist = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
        if (dist < closetdistance)
        {
            closetdistance = dist;
            temp = go;
        }
        return temp;

    }
    #endregion
}
