using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject pauseThingy;

    public void OnPausing()
    {
        pauseThingy.SetActive(true);
    }
    public void NoPauser()
    {
        pauseThingy.SetActive(false);
    }
}
