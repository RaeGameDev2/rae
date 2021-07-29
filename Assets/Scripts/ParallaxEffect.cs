using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpritesData
{
   public SpriteRenderer spriteObj;
   public int layer = 0;
   [Range(0.0f, 1.0f)]
    public float parallaxEffect = 1.0f;
}

public class ParallaxEffect : MonoBehaviour
{
    private float[] length;
    private Vector3[] startPosition;
    public GameObject gameCamera;
    [Tooltip("Add background sprites")]
    public SpritesData[] spritesList;
    // Start is called before the first frame update
    void Start()
    {
        length = new float[spritesList.Length];
        startPosition = new Vector3[spritesList.Length];
        for (int i = 0; i < spritesList.Length; i++) {
            startPosition[i] = spritesList[i].spriteObj.transform.position;
            length[i] = spritesList[i].spriteObj.bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (SpritesData spData in spritesList) {
            float temp = gameCamera.transform.position.x * (1 - spData.parallaxEffect);
            float dist = gameCamera.transform.position.x * spData.parallaxEffect;
            spData.spriteObj.sortingOrder = spData.layer;
            spData.spriteObj.transform.position = new Vector3(startPosition[i].x + dist, startPosition[i].y, startPosition[i].z);

            if (temp > startPosition[i].x + (length[i] / 2))
                startPosition[i].x += length[i];
            else if (temp < startPosition[i].x - (length[i] /2))
                startPosition[i].x -= length[i];
            i++;
        }
    }
}
