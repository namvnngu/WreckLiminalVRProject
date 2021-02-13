using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class CrackingWall : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture[] CrackedAlbedos;
    public Texture[] CrackedNormals;
    public Material WallMaterial;
    public GameObject[] WallObjects;
    public GameObject[] ThrowableObjects;
    public int NumberOfCollisions;


    public int crackedImagesNumber = 0;
    public bool isWallDestroyed = false;
    public bool isCollided = false;


    void Start()
    {
        WallObjects = GameObject.FindGameObjectsWithTag("Walls");
        WallMaterial.SetTexture("_BumpMap", CrackedNormals[CrackedNormals.Length - 1]);
        WallMaterial.SetTexture("_BaseMap", CrackedAlbedos[CrackedAlbedos.Length - 1]);
    }

    void Update()
    {
        ThrowableObjects = GameObject.FindGameObjectsWithTag("Throwable");
        if (!isWallDestroyed && isCollided)
        {
            if (crackedImagesNumber >= CrackedNormals.Length - 1)
            {
                crackedImagesNumber = 0;
            }
            else
            {
                if (NumberOfCollisions > 4)
                {
                    WallMaterial.SetTexture("_BumpMap", CrackedNormals[crackedImagesNumber]);
                    WallMaterial.SetTexture("_BaseMap", CrackedAlbedos[crackedImagesNumber]);
                    crackedImagesNumber++;
                    NumberOfCollisions = 0;

                }
            }
            isCollided = false;
        }
    }
}

 





