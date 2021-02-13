using UnityEngine;

public class WallContact : MonoBehaviour
{

    private GameObject Environment;
    private CrackingWall CrackingWallScript;
    
    void Start()
    {
        Environment = GameObject.FindGameObjectWithTag("Environment");
        CrackingWallScript = Environment.GetComponent<CrackingWall>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Walls")
        {
            CrackingWallScript.isCollided = true;
            CrackingWallScript.NumberOfCollisions++;

            if (CrackingWallScript.crackedImagesNumber == CrackingWallScript.CrackedAlbedos.Length - 1)
                CrackingWallScript.isWallDestroyed = true;
        }
    }

}
