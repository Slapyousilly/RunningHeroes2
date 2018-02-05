using UnityEngine;
using System.Collections;

public class UpgradeScript : MonoBehaviour {
    public GameObject gameManager;

    public GameObject stunUp;
    public GameObject reducUp;
    public GameObject strikeUp;

    private GameManager upgradeSkills;
    // I WANT LOAD GAME MANAGER
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
	// Use this for initialization
	void Start () {
        upgradeSkills = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

    public void UpgradeStun()
    {
        if (upgradeSkills.stunDur < upgradeSkills.maxstunDur && upgradeSkills.skillPoints >= 1)
        {
            upgradeSkills.skillPoints -= 1;
            upgradeSkills.stunDur += 0.1f;
        }
    }
    public void UpgradeReduc()
    {
        if (upgradeSkills.dmgRedDur < upgradeSkills.maxdmgRedDur && upgradeSkills.skillPoints >= 1)
        {
            upgradeSkills.skillPoints -= 1;
            upgradeSkills.dmgRedDur += 0.1f;
        }
    }
    public void UpgradeDMG()
    {
        if (upgradeSkills.strikeMultiplier < upgradeSkills.maxstrikeMultiplier && upgradeSkills.skillPoints >= 1)
        {
            upgradeSkills.skillPoints -= 1;
            upgradeSkills.strikeMultiplier += 0.1f;
        }
    }

    public void DisplayStun()
    {
        stunUp.SetActive(true);
    }
    public void DisplayReduc()
    {
        reducUp.SetActive(true);
    }
    public void DisplayDMG()
    {
        strikeUp.SetActive(true);
    }

    public void NoDisplayStun()
    {
        stunUp.SetActive(false);
    }
    public void NoDisplayReduc()
    {
        reducUp.SetActive(false);
    }
    public void NoDisplayDMG()
    {
        strikeUp.SetActive(false);
    }
}
