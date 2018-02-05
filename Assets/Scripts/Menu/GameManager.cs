using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  // It will be accessible outside the class
    //public UpgradeScript upgradeScript;

    public float stunDur = 0.5f;
    public float dmgRedDur = 0.8f;
    public float strikeMultiplier = 1.2f;
    public int skillPoints = 100;
    public int HighScore = 0;

    public float maxstunDur = 4.0f;
    public float maxdmgRedDur = 4.0f;
    public float maxstrikeMultiplier = 3.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // When load a new scene, this object won't be destroyed
        //upgradeScript = GetComponent<UpgradeScript>();
        InitGame();
    }

    //// Use this for initialization
    //void Start () {
    //    //upgradeScript = GetComponent<UpgradeScript>();
    //    InitGame();
    //}
    void InitGame()
    {
        //upgradeScript.dmgReduc = 30.0f;
    }
}
