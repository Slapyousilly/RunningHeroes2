using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
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
    }

    void Start()
    {
        GameManager.instance.Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        GameData data = new GameData();
        data.stunDur = stunDur;
        data.dmgRedDur = dmgRedDur;
        data.strikeMultiplier = strikeMultiplier;
        data.skillPoints = skillPoints;
        data.HighScore = HighScore;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            stunDur = data.stunDur;
            dmgRedDur = data.dmgRedDur;
            strikeMultiplier = data.strikeMultiplier;
            skillPoints = data.skillPoints;
            HighScore = data.HighScore;
        }
    }

    public void Reset()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        GameData data = new GameData();

        data.stunDur = 0.5f;
        data.dmgRedDur = 0.8f;
        data.strikeMultiplier = 1.2f;
        data.skillPoints = 100;
        data.HighScore = 0;

        bf.Serialize(file, data);
        file.Close();

        Application.Quit();
    }

    [Serializable]
    class GameData
    {
        public float stunDur;
        public float dmgRedDur;
        public float strikeMultiplier;
        public int skillPoints;
        public int HighScore;
    }
}
