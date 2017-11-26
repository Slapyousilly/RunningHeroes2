﻿using UnityEngine;
using System.Collections;

public class FadeInOutScript : MonoBehaviour
{
    public bool fadeOut = true;
    public bool halfway = true;
    public Texture2D fadeTexture;
    public float fadeSpeed = 0.9f;
    public int drawDepth = -1000;
    private float alpha = 0.0f;
    private float alphaIn = 1.0f;
    private float alphaPew = 0.7f;
    private int fadeDir = -1;

    // Use this for initialization
    void Start()
    {
        //fade = true;
        if (!fadeOut)
        {
            alpha = alphaIn;
            if (halfway)
                alpha = alphaPew;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGUI()
    {
        if (fadeOut)
        {
            if (halfway && alpha <= alphaPew)
            {
                alpha -= fadeDir * fadeSpeed * (Time.deltaTime * 0.2f);
                alpha = Mathf.Clamp01(alpha);
            }
                Color thisAlpha = GUI.color;
                thisAlpha.a = alpha;
                GUI.color = thisAlpha;

                GUI.depth = drawDepth;

                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);

        }
        else
        {

            alpha += fadeDir * fadeSpeed * (Time.deltaTime * 0.2f);
            alpha = Mathf.Clamp01(alpha);

            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}