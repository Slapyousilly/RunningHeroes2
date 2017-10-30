using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour
{
    public bool b_Scrolling;

    public float scrollSpeed;
    public float tileSizeX;

    private Vector3 startPosition;

    private float t_dt;

    void Start()
    {
        b_Scrolling = true;
        startPosition = transform.position;
    }

    void Update()
    {
        if (b_Scrolling)
        {
            t_dt += Time.deltaTime;
            float newPosition = Mathf.Repeat(t_dt * scrollSpeed, tileSizeX);
            transform.position = startPosition + Vector3.right * newPosition;
        }
    }
}