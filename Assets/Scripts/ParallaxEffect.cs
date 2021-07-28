using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length;
    private Vector3 startPosition;
    public GameObject gameCamera;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = gameCamera.transform.position.x * (1 - parallaxEffect);
        float dist = gameCamera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPosition[0] + dist, startPosition[1], startPosition[2]);
    
        if (temp > startPosition[0] + (length / 2))
            startPosition[0] += length;
        else if (temp < startPosition[0] - (length /2))
                startPosition[0] -= length;
    }
}
