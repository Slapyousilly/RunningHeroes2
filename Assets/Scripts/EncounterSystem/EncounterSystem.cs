using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EncounterSystem : MonoBehaviour {

    // Check for Player status and give next encounter to be the one to benefit more (still chance)
    public List<GameObject> UIBackground = new List<GameObject>();
    private GameObject background;

    public PlayerScrpt m_Player;            // Player Script object.
    private float m_RandomChance;           // Random factor to determine next encounter
    private float m_Encounterdt;            // current encounter time
    public float m_nextEncounterDt = 4.0f;  // fixed next encounter time

    public float m_bufferdt = 4.5f;
    private float m_nextbufferDt;     

    public Text feedbackText;               // Text to ask player to tap
    public GameObject feedbackImg;          // Img to ask player to tap
    public bool doEncounterCheck;

    public GameObject GameStateThing;
    private GameState m_gstate;

    public GameObject Monster;
    public GameObject Boss;

    public GameObject theButton;
    public Button purchaseButton;

    private DayNightScript dayNightSystem;

    public int spawnCount;
    private int totalSpawn;
    public bool testtyy;
    public bool uhohuhohlowhp;
    public bool wewewoww;

    private EncounterUI UIUIThing;
    private bool bgCreate;
    private GameObject BGBG;
    private AudioSource theBGM;

    public int shrineCost = 50;                  // Shrine Cost
    public int repairCost = 75;                  // Armor Repair Cost
    public int AupgradeCost = 200;               // Armor Upgrade Cost
    public int WupgradeCost = 200;               // Weapon Upgrade Cost
    public int baseGoldDrop = 30;               // 100 + this * multiplier

    private bool displayIt;
    private bool isBossSpawned;
    private bool spawnDaBoss;
    private bool initRunDone;

    private bool buttonClicked;

    private int m_ArmUp;
    private int m_ArmRe;
    private int m_WeaUp;
    private int m_Shrine;
    private int m_MonEn;
    private int m_BossEn;

    public enum ENCOUNTERS                  // Encounters Players will get
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
    public List<ENCOUNTERS> _encounters = new List<ENCOUNTERS>(); // List of Encounters

    // _encounters = new ENCOUNTERS[]{E_WEAPONUP, E_REPAIR};

	// Use this for initialization
	void Start () {
        testtyy = false;
        displayIt = true;
        bgCreate = true;
        spawnCount = 0;
        doEncounterCheck = true;
        isBossSpawned = false;
        initRunDone = false;
        buttonClicked = false;
        spawnDaBoss = false;
        m_gstate = GameStateThing.GetComponent<GameState>();
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScrpt>();  // Finding player and getting details from player directly
        UIUIThing = GameObject.FindGameObjectWithTag("UIEncounter").GetComponent<EncounterUI>();
        dayNightSystem = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightScript>();
        purchaseButton = theButton.GetComponent<Button>();
        theBGM = GetComponent<AudioSource>();
        m_ArmUp = m_ArmRe = m_WeaUp = m_Shrine = m_MonEn = m_BossEn = 0;
        totalSpawn = 2;
        //background = GameObject.FindGameObjectWithTag("Background");
        //_encounters.Add(ENCOUNTERS.E_BOSS);
        //_encounters.Add(ENCOUNTERS.E_WEAPONUP); //= new ENCOUNTERS[5] { ENCOUNTERS.E_WEAPONUP, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_MONSTERS, ENCOUNTERS.E_SHRINE, ENCOUNTERS.E_ARMORUP };
        //_encounters.Add(ENCOUNTERS.E_SHRINE);
        //_encounters.Add(ENCOUNTERS.E_ARMORUP);
        //_encounters.Add(ENCOUNTERS.E_REPAIR);
        theBGM.loop = true;
        theBGM.Play();
        if (!initRunDone)
            InitialRun();

        purchaseButton.onClick.AddListener(TaskOnClick);
        //UIUIThing.InitialRun();
    }

    void TaskOnClick()
    {
        buttonClicked = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_Player.m_HP < m_Player.m_maxHP)
            wewewoww = true;

        if (m_Player.m_lifeSpan <= 30.0f)
            uhohuhohlowhp = true;
        bossSpawnCheck();
        if (doEncounterCheck)
        {
            //if (CurrEncounterCheck() != ENCOUNTERS.E_MONSTERS || CurrEncounterCheck() != ENCOUNTERS.E_BOSS)
            m_nextEncounterDt -= Time.deltaTime;

            if (m_nextEncounterDt <= 0.0f)
            {
                purchaseButton.interactable = true;
                // do the Touch to be like "hey u got this encounter"
                // then after pass by let the encounter goneee
                if (m_bufferdt > 0.0f)
                {
                    m_bufferdt -= Time.deltaTime;
                    showRespectiveText();

                    if (m_bufferdt <= 0.0f)
                        EncounterReset();
                }
                //_encounters[0]
            }
        }
	}

    void showRespectiveText()
    {
        feedbackImg.SetActive(true);
        switch (CurrEncounterCheck())
        {
            case ENCOUNTERS.E_ARMORUP:
                //Instantiate(UIBackground[1]);
                createBackground(1);
                if (displayIt)
                    feedbackText.text = "Tap to Upgrade Armor Cost: " + AupgradeCost;
                if (buttonClicked && m_Player.m_money >= AupgradeCost && displayIt)
                {
                    displayIt = false;
                    m_Player.m_money -= AupgradeCost;
                    int newMaxHP = (int)((float)m_Player.m_maxHP * 1.2f);
                    m_Player.m_maxHP = newMaxHP;
                    feedbackText.text = "Armor Upgraded!";
                    m_Player.m_HP = m_Player.m_maxHP;
                    AupgradeCost += 50;
                }
                break;
            case ENCOUNTERS.E_WEAPONUP:
                //Instantiate(UIBackground[2]);
                createBackground(2);
                if (displayIt)
                    feedbackText.text = "Tap to Upgrade Weapon Cost: " + WupgradeCost;
                if (buttonClicked && m_Player.m_money >= WupgradeCost && displayIt)
                {
                    displayIt = false;
                    m_Player.m_money -= WupgradeCost;
                    feedbackText.text = "Weapon Upgraded!";
                    int newDMG = (int)((float)m_Player.m_Damage * 1.2f);
                    m_Player.m_Damage = newDMG;
                    WupgradeCost += 50;
                }
                break;
            case ENCOUNTERS.E_REPAIR:
                //Instantiate(UIBackground[0]);
                createBackground(0);
                if (displayIt)
                    feedbackText.text = "Tap to Repair Armor Cost: " + repairCost;
                if (buttonClicked && m_Player.m_money >= repairCost && displayIt)
                {
                    displayIt = false;
                    m_Player.m_money -= repairCost;
                    feedbackText.text = "Armor Repaired!";
                    m_Player.m_HP = m_Player.m_maxHP;
                    repairCost += 20;
                }
                break;
            case ENCOUNTERS.E_SHRINE:
                //Instantiate(UIBackground[3]);
                createBackground(3);
                if (displayIt)
                    feedbackText.text = "Tap to Increase Lifespan Cost: " + shrineCost;
                if (buttonClicked && m_Player.m_money >= shrineCost && displayIt)
                {
                    displayIt = false;
                    m_Player.m_money -= shrineCost;
                    feedbackText.text = "Lifespan Recovered!";
                    m_Player.m_lifeSpan = 100;
                    shrineCost += 20;
                }
                break;
            case ENCOUNTERS.E_MONSTERS:
                if (spawnCount < 1)
                {
                    feedbackText.text = "Use your skills!";
                    m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                    createMonster();
                    testtyy = true;
                }
                break;
            case ENCOUNTERS.E_BOSS:
                if (spawnCount < 1)
                {
                    feedbackText.text = "Use your skills!"; 
                    m_gstate.gameState = GameState.GAMESTATE.GS_ENCOUNTER;
                    createBoss();
                }
                break;
        }
    }

    void createMonster()
    {
        GameObject go = Instantiate(Monster, new Vector3(10, -2.2f, 0), Quaternion.identity) as GameObject;
        EnemyScript scripty = go.GetComponent<EnemyScript>();
        scripty.m_goldworth += (int)((double)baseGoldDrop * (0.4 * (double)totalSpawn));
        scripty.SetMaxHealth(600 + (55 * totalSpawn));
        scripty.SetHealth(600 + (55 * totalSpawn));
        scripty.SetAttackDamage(50 + (totalSpawn * 7));

        Debug.Log(go.GetComponent<EnemyScript>().m_HP);
        //Debug.Log((int)((double)baseGoldDrop * (0.1 * (double)totalSpawn)));
        spawnCount++;
        totalSpawn++;
    }

    void createBoss()
    {
        GameObject go = Instantiate(Boss, new Vector3(10, -2.2f, 0), Quaternion.identity) as GameObject;
        go.GetComponent<EnemyScript>().m_goldworth += 2000 + ((baseGoldDrop / 2) * totalSpawn);
        go.GetComponent<EnemyScript>().SetMaxHealth(5000 + (10 * totalSpawn));
        go.GetComponent<EnemyScript>().SetHealth(5000 + (10 * totalSpawn));
        go.GetComponent<EnemyScript>().SetAttackDamage(95 + (totalSpawn * 20));
        spawnCount++;
    }

    void createBackground(int theNum)
    {
        if (bgCreate)
        {
             BGBG = Instantiate(UIBackground[theNum]) as GameObject;
             //BGBG.transform.SetParent(background.transform);
             BGBG.transform.localPosition = new Vector3(4, -4, 0);
             bgCreate = false;
            //background;
        }
    }

    #region Encounter Checks
    private void AddEncounter(ENCOUNTERS enc)
    {
        _encounters.Add(enc);
    }

    //void EncounterCheck()
    //{
    //    _encounters.Contains(ENCOUNTERS.E_SHRINE); // Returns true if it does contain inside List.
    //    //_encounters[0];
    //    if (_encounters.Count < 5)
    //    {
    //        // Add new encounter logic
    //        if (m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
    //        {
    //            _encounters.Add(ENCOUNTERS.E_REPAIR);
    //        }
    //        if (m_Player.m_lifeSpan < 30.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
    //        {
    //            _encounters.Add(ENCOUNTERS.E_SHRINE);
    //        }
    //        // Determine next encounter
    //    }
    //}

    void EncounterReset()
    {
        feedbackImg.SetActive(false);
        Destroy(BGBG);
        bgCreate = displayIt = true;
        buttonClicked = false;
        purchaseButton.interactable = false; 
        spawnCount = 0;
        subtractAmount(_encounters[0]);
        _encounters.RemoveAt(0);
        ENCOUNTERS nextToAdd = NextEncounterAdd();
        addAmount(nextToAdd);
        Debug.Log(nextToAdd);
        _encounters.Add(nextToAdd);
        UIUIThing.AddUINextEncounter(nextToAdd);
        UIUIThing.RemoveUIEncounter();
        feedbackText.text = "";
        m_nextEncounterDt = 2.0f;
        m_bufferdt = 4.5f;
    }

    void bossSpawnCheck()
    {
        if (dayNightSystem.getDaysPassed() % 10 == 0)
        {
            spawnDaBoss = true;
        }
    }

    public ENCOUNTERS NextEncounterAdd()
    {
        _encounters.Contains(ENCOUNTERS.E_SHRINE); // Returns true if it does contain inside List.
        //_encounters[0];
        if (_encounters.Count < 5)
        {
            // Add new encounter logic
            // Determine next encounter

            if (spawnDaBoss && !isBossSpawned)
            {
                isBossSpawned = true;
                return ENCOUNTERS.E_BOSS;
            }

            if (m_Player.m_lifeSpan < 50.0f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
            {
                return ENCOUNTERS.E_SHRINE;
            }

            float randfVal = Random.RandomRange(0.0f, 1.32f);
            Debug.Log(randfVal);

            if (randfVal <= 0.2f && !_encounters.Contains(ENCOUNTERS.E_SHRINE))
                return ENCOUNTERS.E_SHRINE;
            else
                while (randfVal <= 0.2f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.2f && randfVal <= 0.4f && m_MonEn < 4)
                return ENCOUNTERS.E_MONSTERS;

            if (randfVal > 0.4f && randfVal <= 0.6f && m_WeaUp < 2/*&& !_encounters.Contains(ENCOUNTERS.E_WEAPONUP)*/)
                return ENCOUNTERS.E_WEAPONUP;
            else
                while (randfVal > 0.4f && randfVal <= 0.6f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.6f && randfVal <= 0.8f && m_ArmUp < 3)
                return ENCOUNTERS.E_ARMORUP;
            else
                while (randfVal > 0.6f && randfVal <= 0.8f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            if (randfVal > 0.8f && randfVal <= 1.0f && m_Player.m_HP <= m_Player.m_maxHP * 0.8f && m_ArmRe < 2)
                return ENCOUNTERS.E_REPAIR;
            else
                while (randfVal > 0.8f && randfVal <= 1.0f)
                    randfVal = Random.RandomRange(0.0f, 1.0f);

            //if (m_Player.m_HP <= m_Player.m_maxHP * 0.75f)
            //    return ENCOUNTERS.E_REPAIR;
            //else
            //    return ENCOUNTERS.E_MONSTERS;
            if (randfVal >1.0f && randfVal <= 1.3f)
            {
                ENCOUNTERS randEnc;
                randEnc = (ENCOUNTERS)Random.Range(0, 4);
                return randEnc;
            }

        }

        return ENCOUNTERS.E_MONSTERS;
    }

    public void addAmount(ENCOUNTERS wow)
    {
        for (int i = 0; i < 5; i++)
        {
            switch (wow)
            {
                case ENCOUNTERS.E_ARMORUP:
                    m_ArmUp++;
                    break;
                case ENCOUNTERS.E_WEAPONUP:
                    m_WeaUp++;
                    break;
                case ENCOUNTERS.E_REPAIR:
                    m_ArmRe++;
                    break;
                case ENCOUNTERS.E_SHRINE:
                    m_Shrine++;
                    break;
                case ENCOUNTERS.E_MONSTERS:
                    m_MonEn++;
                    break;
                case ENCOUNTERS.E_BOSS:
                    m_BossEn++;
                    break;
            }
        }
    }

    public void subtractAmount(ENCOUNTERS wow)
    {
        for (int i = 0; i < 5; i++)
        {
            switch (wow)
            {
                case ENCOUNTERS.E_ARMORUP:
                    m_ArmUp--;
                    break;
                case ENCOUNTERS.E_WEAPONUP:
                    m_WeaUp--;
                    break;
                case ENCOUNTERS.E_REPAIR:
                    m_ArmRe--;
                    break;
                case ENCOUNTERS.E_SHRINE:
                    m_Shrine--;
                    break;
                case ENCOUNTERS.E_MONSTERS:
                    m_MonEn--;
                    break;
                case ENCOUNTERS.E_BOSS:
                    m_BossEn--;
                    break;
            }
        }
    }

    public void InitialRun()
    {
        for (int i = 0; i < 5; i++)
        {
            ENCOUNTERS randEnc;
            int theRandNum = Random.Range(0, 4);

            randEnc = (ENCOUNTERS)theRandNum;
            if (i == 0)
            {
                addAmount(ENCOUNTERS.E_MONSTERS);
                _encounters.Add(ENCOUNTERS.E_MONSTERS);
            }
            else
            {
                addAmount(randEnc);
                _encounters.Add(randEnc);
            }
        }

        initRunDone = true;

    }

    public ENCOUNTERS CurrEncounterCheck()
    {
        return _encounters[0];
    }

    protected List<ENCOUNTERS> EncounterList()
    {
        return _encounters;
    }
    #endregion 
}
