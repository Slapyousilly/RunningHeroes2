using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {

    public bool m_isDead;
    public int m_HP;
    public int m_maxHP;
    public int m_Damage;
    public int m_DamageRng;         // This value will be the min max 
    public string m_Name;
    public float m_atkSpd;
    public float m_flinchDt;
    public float m_resistance;
    public GameState state;
    private float timer = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collide Enemy!");
            col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<EntityBase>().m_Damage);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide Player!");
            col.gameObject.SendMessage("TakeDamage", col.gameObject.GetComponent<PlayerScrpt>().m_Damage);
        }
    }

    protected void TakeDamage(int dmg)
    {
        // Pop up a Text floating up the damage dealt
        int health = this.GetHealth() - dmg;
        this.SetHealth(health);
    }

    protected void MoveTowardsTarget()
    {
        //Vector2.Distance(this.gameObject.transform.position, GetNearestTarget().transform.position);
        Vector3 Dir = (GetNearestTarget().transform.position - this.transform.position).normalized;
        Dir.z = 0;
        this.transform.position += Dir * Time.deltaTime * 3.0f;
    }

    protected void MoveToPosition(Vector3 newPos)
    {
        //Vector2.Distance(this.gameObject.transform.position, GetNearestTarget().transform.position);
        Vector3 Dir = (newPos - this.transform.position).normalized;
        Dir.z = 0;
        this.transform.position += Dir * Time.deltaTime * 3.0f;
    }

    protected void AttackTarget(float atkSpd)
    {
        timer += Time.deltaTime;
        while (timer > atkSpd)
        {
            int demDamage = Random.Range(this.m_Damage - this.m_DamageRng, this.m_Damage + this.m_DamageRng);
            Debug.Log("Damage Dealt from: " + this.m_Name + " To: " + GetTarget().GetComponent<EntityBase>().m_Name);
            GetTarget().SendMessage("TakeDamage", demDamage);
            timer = 0;
        }
        //PlayerScrpt temp;
        //EnemyScript temp1;
        //if (GetTarget().CompareTag("Player"))
        //    temp = GetTarget().GetComponent<PlayerScrpt>();
        //else
        //    temp1 = GetTarget().GetComponent<EnemyScript>();

        //if (this.tag == "Enemy")
        //    GetTarget().SendMessage("TakeDamage", this.m_Damage);
        //else
    }

    protected void AttackTarget(int atkDmg, float resist)
    {
        Debug.Log("Spell DMG from: " + this.m_Name + " To: " + GetTarget().GetComponent<EntityBase>().m_Name);
        GetTarget().SendMessage("TakeDamage", atkDmg);
    }

    protected GameObject GetTarget()
    {
        GameObject go = null;
        if (this.tag == "Enemy")
            go = GameObject.FindGameObjectWithTag("Player");
        else
            go = GameObject.FindGameObjectWithTag("Enemy");

        return go;
    }

    protected GameObject GetNearestTarget()
    {
        float closetdistance = 900;
        GameObject temp = null;
        GameObject go = null;
        if (this.tag == "Enemy")
             go = GameObject.FindGameObjectWithTag("Player");
        else
             go = GameObject.FindGameObjectWithTag("Enemy");

        float dist = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
        if (dist < closetdistance)
        {
            closetdistance = dist;
            temp = go;
        }
        return temp;

    }

    public abstract void RunFSM(); ///! act upon any change in behaviour   

    public float GetAttackSpeed()
    {
        return m_atkSpd;
    }
    public int GetAttackDamage()
    {
        return m_Damage;
    }

    public int GetHealth()
    {
        return m_HP;
    }
    public int GetMaxHealth()
    {
        return m_maxHP;
    }

    public void SetHealth(int health)
    {
        m_HP = health;
    }
    public void SetMaxHealth(int maxhealth)
    {
        m_maxHP = maxhealth;
    }
}
