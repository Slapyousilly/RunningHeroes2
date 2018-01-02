using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour {

    private ParticleSystem pewpew;
    float time = 0.0f;

	// Use this for initialization
	void Start () {
        pewpew = gameObject.GetComponent<ParticleSystem>();
        pewpew.Stop();
       
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            pewpew.Stop();
        }
	}

    public void PlayerBarrier()
    {
        //Color pew = new Color();
        //pew.g = 1.0f;
        //pewpew.startColor = pew;
        pewpew.Play();
        time = 3.0f;
    }
}
