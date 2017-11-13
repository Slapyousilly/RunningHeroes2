using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScrpt : EntityBase {

    private Animator anim;//! Animator of Player to set bool/triggers in Updates
    public float m_lifeSpan;
    private float m_maxlifeSpan;
    public GameObject m_playerHP;
    private Slider m_HPSlider;
    public GameObject m_playerLifespan;
    public Text m_playerLifespanText;

    private EnemyScript enemy;
    private bool is_Collided;
    private Rigidbody2D rb2D;

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
        rb2D = GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
        m_HPSlider = m_playerHP.GetComponent<Slider>();
        m_isDead = false;
        m_HP = 1000;
        m_maxHP = m_HP;
        m_HPSlider.maxValue = m_maxHP;
        m_HPSlider.value = m_HP;
        m_atkSpd = 1.1f;
        m_flinchDt = 0.5f;
        m_Name = "Hero";
        m_Damage = 200;
        m_resistance = 1.0f;
        m_lifeSpan = 100.0f;
        m_maxlifeSpan = m_lifeSpan;
	}
	
	// Update is called once per frame
	void Update () {
        //m_HPSlider.value = m_HP;
        //Time.timeScale = 0; //can use to pause game or do shit.
        m_lifeSpan -= Time.deltaTime;

        m_playerLifespan.GetComponent<Image>().fillAmount = (m_lifeSpan / m_maxlifeSpan);
        m_playerLifespanText.text = System.Math.Round(m_lifeSpan, 1) +"%";
        

        if (state.gameState != GameState.GAMESTATE.GS_TUTORIAL)
            Time.timeScale = 1;
            //anim.speed = 1;

        if (m_HP > 0 || m_lifeSpan > 0)
        {
            switch (state.gameState)
            {
                case GameState.GAMESTATE.GS_PLAY:
                    Run();
                    break;
                case GameState.GAMESTATE.GS_ENCOUNTER:

                    RunFSM();
                    //Idle();
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

    public override void RunFSM()
    {
        if (!enemy)
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();

        if (GetNearestTarget())
        {
            Debug.Log("Testing Chase");
            Run();
            if (!is_Collided)
                MoveTowardsTarget();
            else
                AttackTarget(m_atkSpd);
        }
        if (m_HP <= 0 || m_lifeSpan <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        is_Collided = true;
        Debug.Log("COLLIDE");
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collide Enemy!");
            col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<EntityBase>().m_Damage);
            //StartCoroutine(enemy.Knockback(0.02f, 350, new Vector3 (-1, 0, 0)));

            //Vector2 pew = new Vector2(240, 0);
            //col.rigidbody.AddRelativeForce(pew);
            //Debug.Log("Collide PEW!");
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide Player!");
            col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<PlayerScrpt>().m_Damage);
        }
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb2D.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }
        yield return 0;
    }

    //public void TakeDamage(int dmg)
    //{
    //    m_HP -= dmg;
    //    m_HPSlider.value = m_HP;
    //}

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


    #endregion
}
