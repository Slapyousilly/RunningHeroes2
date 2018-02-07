using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : EntityBase {

    private Animator anim;//! Animator of Enemy to set bool/triggers in Updates
    public GameObject m_enemyHP;
    private Image m_HPSlider;
    private Image m_SkillSlider;
    private bool is_Collided;
    private Rigidbody2D rb2D;

    private PlayerScrpt player;
    public int m_goldworth;

    private AudioSource enemyAudio;
    public AudioClip orc;
    public AudioClip die;
    private float testfloat;

    public float skillCharge;
    private float maxSkillCharge = 2.0f;
    private float nextSkillUser;
    private float nextSkillUseMax = 5.0f;

    public GameObject stunOBJ;
	// Use this for initialization
	void Start () {
        enemyAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();

        GameObject healthbar = Instantiate(m_enemyHP) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
        healthbar.transform.SetParent(this.gameObject.transform);

        m_HPSlider = GameObject.FindGameObjectWithTag("healthTest").GetComponent<Image>();
        m_SkillSlider = GameObject.FindGameObjectWithTag("SkillCharge").GetComponent<Image>();
        //stunOBJ = GetComponentInChildren<GameObject>();
        skillCharge = 0.0f;
        nextSkillUser = 0.0f;
        state = GameObject.FindGameObjectWithTag("GameStateSystem").GetComponent<GameState>();
        m_isDead = false;
        //m_HP = 600;
        //m_maxHP = m_HP;
        //m_atkSpd = 1.1f;
        //m_flinchDt = 0.5f;
        //m_Name = "Orc";
        //m_Damage = 100;
        //m_goldworth = 100;
        //m_DamageRng = 30;
        //m_resistance = 1.0f;
        is_Collided = false;
	}
    //void Start (string m_Name, int m_HP, float m_atkSpd, float m_flinctDT, int m_Damage, float m_resistance)
    //{

    //}
	
	// Update is called once per frame
	void Update () {
        //m_HPSlider.fillAmount = 0.5f;
        m_HPSlider.fillAmount = ((float)m_HP / (float)m_maxHP);
        m_SkillSlider.fillAmount = (skillCharge / maxSkillCharge);
        RunFSM();
        StunThing();
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
                if (!StunState())
                {
                    stunOBJ.SetActive(false);
                    //skillChargeMax = maxSkillChargeMax;
                    //nextSkillUser = nextSkillUseMax;
                    if (nextSkillUser <= 0)
                    {
                        skillCharge += Time.deltaTime;
                        if (skillCharge >= maxSkillCharge)
                        {
                            int demDamage = Random.Range(m_Damage - m_DamageRng, m_Damage + m_DamageRng);
                            AttackTarget(demDamage, m_atkSpd);

                            skillCharge = 0;                    // Skill reset
                            nextSkillUser = nextSkillUseMax;    // Global Skill Reset
                        }
                    }

                    testfloat += Time.deltaTime;
                    if (testfloat >= m_atkSpd)
                    {
                        Debug.Log("I am still attacking");
                        Attack();
                        if (enemyAudio.clip != orc)
                            enemyAudio.clip = orc;
                        enemyAudio.Play();
                        testfloat = 0.0f;
                    }
                    else
                        Idle();
                    AttackTarget(m_atkSpd);
                }
                else
                {
                    stunOBJ.SetActive(true);
                    skillCharge = 0;
                    nextSkillUser = 4.0f;
                    Idle();
                }
            }
            nextSkillUser -= Time.deltaTime;
        }
        if (m_HP <= 0)
        {
            Die();
            enemyAudio.clip = die;
            enemyAudio.Play();
            Destroy(this.gameObject);
            DropGold(m_goldworth);
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
