using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public GameObject[] WallObjects;
    public GameObject[] ObjectsToHide;
    public GameObject[] ObjectsToDrop;
    public GameObject FracturedWall;
    public GameObject FinalEffect;

    void Start()
    {
        WallObjects = GameObject.FindGameObjectsWithTag("Walls");
    }
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            //painting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //painting.GetComponent<Rigidbody>().useGravity = true;
            foreach (GameObject droppedObject in ObjectsToDrop)
            {
                droppedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                droppedObject.GetComponent<Rigidbody>().useGravity = true;
                
            }


            foreach (GameObject wall in WallObjects) Destroy(wall);
            foreach (GameObject destroyedObject in ObjectsToHide)
            {
                Destroy(destroyedObject);
            }
            GameObject crackedWall = Instantiate(FracturedWall, FracturedWall.transform.position, Quaternion.identity);
            foreach (Transform child in crackedWall.transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<Rigidbody>().AddExplosionForce(500, FracturedWall.transform.position, 500);
            }
            Instantiate(FinalEffect, new Vector3(0f, 0f, 0f), FinalEffect.transform.rotation);
        }
    }

}
       




