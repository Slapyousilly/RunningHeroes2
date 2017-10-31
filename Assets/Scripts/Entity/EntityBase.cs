using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {

    public bool m_isDead;
    public int m_HP;
    public int m_maxHP;
    public int m_Damage;
    public string m_Name;
    public float m_atkSpd;
    public float m_flinchDt;
    public float m_resistance;
    public GameState state;

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
        int health = this.GetHealth() - dmg;
        this.SetHealth(health);
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
