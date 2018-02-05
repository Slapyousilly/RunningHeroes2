using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    private Selectable SkillButton;
    public Image buttonCD;
    float m_timeCD, origDT;
    //public AudioClip spell;
    private AudioSource use;
	// Use this for initialization
	void Start () {
        use = GetComponent<AudioSource>();
        SkillButton = gameObject.GetComponent<Selectable>();
        SkillButton.interactable = true;
        m_timeCD = origDT = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        m_timeCD -= Time.deltaTime;

        if (m_timeCD <= 0)
        {
            if (!SkillButton.IsInteractable())
                SkillButton.interactable = true;
        }
        else
        {   
            buttonCD.fillAmount = m_timeCD / origDT;
        }
	}

    public void SkillUse(float cooldown)
    {
        use.Play();
        SkillButton.interactable = false;
        if (!SkillButton.IsInteractable())
            buttonCD.fillAmount = 1.0f;
        m_timeCD = cooldown;
        origDT = cooldown;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
