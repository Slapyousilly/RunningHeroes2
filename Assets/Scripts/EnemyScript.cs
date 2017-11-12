using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : EntityBase {

    private Animator anim;//! Animator of Enemy to set bool/triggers in Updates
    public GameObject m_enemyHP;
    private Slider m_HPSlider;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
        m_HPSlider = m_enemyHP.GetComponent<Slider>();
        state = GameObject.FindGameObjectWithTag("GameStateSystem").GetComponent<GameState>();
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
	}
    //void Start (string m_Name, int m_HP, float m_atkSpd, float m_flinctDT, int m_Damage, float m_resistance)
    //{

    //}
	
	// Update is called once per frame
	void Update () {
        RunFSM();
	}

    public override void RunFSM()
    {
        if (GetNearestTarget())
        {
            Debug.Log("Testing Chase");
            Run();
            MoveTowardsTarget();
        }
        if (m_HP <= 0)
        {
            Die();
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
}
