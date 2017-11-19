using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : EntityBase {

    private Animator anim;//! Animator of Enemy to set bool/triggers in Updates
    public GameObject m_enemyHP;
    private Slider m_HPSlider;
    private bool is_Collided;
    private Rigidbody2D rb2D;

    private PlayerScrpt player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();
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
        is_Collided = false;
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
            //Debug.Log("Enemy Chase");
            Run();
            if (!is_Collided)
                MoveTowardsTarget();
            else
            {
                Attack();
                AttackTarget(m_atkSpd);
            }
        }
        if (m_HP <= 0)
        {
            Die();
            Destroy(this.gameObject);
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

    void OnCollisionEnter2D(Collision2D col)
    {
        is_Collided = true;
        //Debug.Log("OH I never check properly lol");

        //if (col.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("Collide Enemy!");
        //    col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<EntityBase>().m_Damage);

        //    //Vector2 pew = new Vector2(240, 0);
        //    //col.rigidbody.AddRelativeForce(pew);
        //    //Debug.Log("Collide PEW!");
        //}
        //else if (col.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("Collide Player!");
        //    col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<PlayerScrpt>().m_Damage);
        //    //StartCoroutine(player.Knockback(0.02f, 350, new Vector3(1, 0, 0)));
        //}
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
