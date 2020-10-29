using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField]
    Texture2D ground;

    [SerializeField]
    int width;
    [SerializeField]
    int height;

    [SerializeField, Range(0, 20.0f)]
    float scale = 20.0f;


    [SerializeField]
    float offsetX = 10.0f;
    [SerializeField]
    float offsetY = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateTexture();
        GetComponent<MeshRenderer>().material.mainTexture = ground;
    }

    void GenerateTexture() {
        ground = new Texture2D(width, height);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Color color = ComputeColor(x,y);
                ground.SetPixel(x, y, color);
            }
        }

        ground.Apply();
    
    }


    Color ComputeColor(int x, int y) {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(sample, sample, sample);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
