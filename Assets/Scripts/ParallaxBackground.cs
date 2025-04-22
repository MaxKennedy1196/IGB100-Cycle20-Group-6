using UnityEngine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour
{
    public GameObject playerCamera;

    public int tilesX = 5;
    public int tilesY = 5;

    public float tileWidth = 2.5f;
    public float tileHeight = 2.5f;

    private Transform[,] tiles;

    private Vector2 centerIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tiles = new Transform[tilesX, tilesY];

        int childIndex = 0;
        for (int x = 0; x < tilesX; x++)
        {
            for (int y = 0; y < tilesY; y++)
            {
                if (childIndex < transform.childCount)
                {
                    tiles[x, y] = transform.GetChild(childIndex);
                    childIndex++;
                }
            }
        }

        centerIndex = new Vector2(tilesX / 2, tilesY / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = playerCamera.transform.position;

        for (int x = 0; x < tilesX; x++)
        {
            for (int y = 0; y < tilesY; y++)
            {
                Transform tile = tiles[x, y];
                Vector3 tilePos = tile.position;

                float dx = camPos.x - tilePos.x;
                float dy = camPos.y - tilePos.y;

                if (Mathf.Abs(dx) > tileWidth * (tilesX / 2))
                {
                    float offsetX = tileWidth * tilesX * Mathf.Sign(dx);
                    tile.position += new Vector3(offsetX, 0, 0);
                }

                if (Mathf.Abs(dy) > tileHeight * (tilesY / 2))
                {
                    float offsetY = tileHeight * tilesY * Mathf.Sign(dy);
                    tile.position += new Vector3(0, offsetY, 0);
                }
            }
        }
    }
}
