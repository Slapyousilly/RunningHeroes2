using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour {
    bool initRunDone;
    bool addNextDone;
    private EncounterSystem hehe;
    public List<GameObject> UIStuff = new List<GameObject>();
    private GameObject UIAdd;
    public List<GameObject> UIShow = new List<GameObject>();
	// Use this for initialization
    public bool pause;
	void Start () {
        hehe = GameObject.FindGameObjectWithTag("EncounterSystem").GetComponent<EncounterSystem>();
        initRunDone = addNextDone = pause = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Time.timeScale = 0.0f; //DO HERE IDK WHY IT ONLY WORKS HERE
        pauseUnpauseGame();

        if (!initRunDone)
            InitialRun();

        ////hehe.CurrEncounterCheck();
        //if (!addNextDone)
        //    AddUINextEncounter();

        //if (Input.GetMouseButtonDown(0))
        //    RemoveUIEncounter();
        //if (Input.GetMouseButtonDown(1))
        //    AddUINextEncounter();
        //EncounterList()[0];
	}

    public void pauseUnpauseGame()
    {
        if (pause)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    public void InitialRun()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i + " " + hehe._encounters[i]);
            switch (hehe._encounters[i])
            {
                case EncounterSystem.ENCOUNTERS.E_ARMORUP:
                    UIAdd = UIStuff[2];
                    break;
                case EncounterSystem.ENCOUNTERS.E_WEAPONUP:
                    UIAdd = UIStuff[0];
                    break;
                case EncounterSystem.ENCOUNTERS.E_REPAIR:
                    UIAdd = UIStuff[1];
                    break;
                case EncounterSystem.ENCOUNTERS.E_SHRINE:
                    UIAdd = UIStuff[3];
                    break;
                case EncounterSystem.ENCOUNTERS.E_MONSTERS:
                    UIAdd = UIStuff[4];
                    break;
                case EncounterSystem.ENCOUNTERS.E_BOSS:
                    UIAdd = UIStuff[4];
                    break;
            }

            //UIShow.Add(Instantiate(UIAdd, new Vector3(60 + (30 * i), 82, 0), Quaternion.identity) as GameObject);

            GameObject UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
            //UIUI.GetComponent<RectTransform>().transform.position = new Vector3(180, 82, 0);
            UIUI.transform.SetParent(this.gameObject.transform);
            UIUI.transform.localPosition = new Vector3(130 + 50 * i, 165, 0);
            UIUI.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            UIUI.SetActive(true);
            UIShow.Add(UIUI);
        }
        Debug.Log("Run done");
        initRunDone = true;
    }

    public void AddUINextEncounter(EncounterSystem.ENCOUNTERS pew)
    {

        //EncounterSystem.ENCOUNTERS pew = hehe.NextEncounterAdd();
        switch (pew)
        {
            case EncounterSystem.ENCOUNTERS.E_ARMORUP:
                AddUI(UIStuff[2]);
                Debug.Log("Adding Armor UI");
                //UIAdd = UIStuff[2];
                //UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
                break;
            case EncounterSystem.ENCOUNTERS.E_WEAPONUP:
                AddUI(UIStuff[0]);
                Debug.Log("Adding Weapon up UI");
                //UIAdd = UIStuff[0];
                //UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
                break;
            case EncounterSystem.ENCOUNTERS.E_REPAIR:
                Debug.Log("Adding repair UI");
                AddUI(UIStuff[1]);
                //UIAdd = UIStuff[1];
                //UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
                break;
            case EncounterSystem.ENCOUNTERS.E_SHRINE:
                Debug.Log("Adding shrine UI");
                AddUI(UIStuff[3]);
                //UIAdd = UIStuff[3];
                //UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
                break;
            case EncounterSystem.ENCOUNTERS.E_MONSTERS:
                Debug.Log("Adding monsters UI" + UIStuff[4].name);
                AddUI(UIStuff[4]);
                //UIAdd = UIStuff[4];
                //UIUI = Instantiate(UIAdd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
                break;
            case EncounterSystem.ENCOUNTERS.E_BOSS:
                AddUI(UIStuff[4]);
                break;
        }
    }

    void AddUI(GameObject demUI)
    {
        GameObject uipewpewadd = demUI;
        GameObject UIUI = Instantiate(uipewpewadd) as GameObject; //, new Vector3(0, 0, 0), Quaternion.identity
        UIUI.transform.SetParent(this.gameObject.transform);
        UIUI.transform.localPosition = new Vector3(130 + 50 * 5, 165, 0);
        UIUI.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        UIUI.SetActive(true);
        UIShow.Add(UIUI);
    }

    public void RemoveUIEncounter()
    {
        GameObject toDestroy = UIShow[0];
        UIShow.RemoveAt(0);
        Destroy(toDestroy);
        ShiftUIEncounter();
    }

    void ShiftUIEncounter()
    {
        for (int i = 0; i < 5; i++)
        {
            UIShow[i].transform.localPosition = new Vector3(130 + 50 * i, 165, 0);
        }
    }
}
