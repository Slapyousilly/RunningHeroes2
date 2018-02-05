using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject QuitObject;
    public GameObject UpgradeObject;
    public GameObject CreditObject;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void SetQuitTrue()
    {
        QuitObject.SetActive(true);
    }
    public void SetQuitFalse()
    {
        QuitObject.SetActive(false);
    }
    public void SetUpgradeTrue()
    {
        UpgradeObject.SetActive(true);
    }
    public void SetUpgradeFalse()
    {
        UpgradeObject.SetActive(false);
    }
    public void SetCreditTrue()
    {
        CreditObject.SetActive(true);
    }
    public void SetCreditFalse()
    {
        CreditObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
