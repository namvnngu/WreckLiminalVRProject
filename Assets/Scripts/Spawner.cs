using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

  public GameObject[] SpawnableObjects;
  public GameObject[] BelowSpawnableObjects;
  public GameObject BowlingBall;
  public GameObject Environment;
  private Vector3[] Positions;
  public GameObject effect;
  bool isSpawning = false;
  bool dontrunagain = false;

  void Update()
  {
        if (!BowlingBall.activeSelf)
        {
            foreach (GameObject currentObject in SpawnableObjects)
            {
                Vector3 currentPosition = currentObject.transform.position;
                if (checkIfPositionEmpty(currentPosition) && !isSpawning)
                {
                    isSpawning = true;

                    InstantiateEffect(currentPosition);
                    StartCoroutine(InstansitateObject(currentPosition));
                }
            }
        }
        else if (BowlingBall.activeSelf && dontrunagain == false)
        {
            //BowlingBall.transform.position = new Vector3(0.016f, 1f, 0.096f);
            Environment.GetComponent<Spawner>().enabled = false;
            GameObject[] spawnedobjects = GameObject.FindGameObjectsWithTag("Throwable");
            foreach (GameObject throwable in spawnedobjects)
            {
                GameObject.Destroy(throwable);
            }
            GameObject newBowlingObject = Instantiate(BowlingBall, new Vector3(0.016f, 0.9f, 0.096f), Quaternion.identity);
            newBowlingObject.GetComponent<BowlingBall>().enabled = true;
            newBowlingObject.GetComponent<Fracture>().enabled = true;
            dontrunagain = true;
        }
  }

  private bool checkIfPositionEmpty(Vector3 position)
  {
    GameObject[] currentSpawnableObjects = GameObject.FindGameObjectsWithTag("Throwable");
    foreach (GameObject currentObject in currentSpawnableObjects)
    {
      Vector3 currentObjectPosition = currentObject.transform.position;
      if (currentObjectPosition.x == position.x && currentObjectPosition.z == position.z)
      {
        return false;
      }
    }
    return true;
  }

  private void InstantiateEffect(Vector3 position)
  {
    Instantiate(
        effect,
        position,
        Quaternion.Euler(-90, 0, 0)
    );
  }

  IEnumerator InstansitateObject(Vector3 position)
  {
    yield return new WaitForSeconds(1);

    // Pick the proper object
    GameObject chosenSpawnableObject;
    Vector3 chosenSpawnableObjectPosition, properSpawnableObjectOpsition;
    if (position.y < 0.5)
    {
      chosenSpawnableObject = pickRandomObject(BelowSpawnableObjects);
      chosenSpawnableObjectPosition = chosenSpawnableObject.transform.position;
      if (chosenSpawnableObjectPosition.y > 0.5)
      {
        properSpawnableObjectOpsition = new Vector3(position.x, 0.3529738f, position.z);
      }
      else
      {
        properSpawnableObjectOpsition = new Vector3(position.x, chosenSpawnableObjectPosition.y, position.z);
      }
    }
    else
    {
      chosenSpawnableObject = pickRandomObject(SpawnableObjects);
      chosenSpawnableObjectPosition = chosenSpawnableObject.transform.position;
      while (chosenSpawnableObjectPosition.y < 0.5 )
      {
        chosenSpawnableObject = pickRandomObject(SpawnableObjects);
        chosenSpawnableObjectPosition = chosenSpawnableObject.transform.position;
      }
      properSpawnableObjectOpsition = new Vector3(position.x, chosenSpawnableObjectPosition.y, position.z);
    }

    // Instantiate the random spawnable object
    chosenSpawnableObject = Instantiate(
      chosenSpawnableObject,
      properSpawnableObjectOpsition,
      transform.rotation
    );
    chosenSpawnableObject.transform.parent = gameObject.transform;

    // Delete effect
    yield return new WaitForSeconds(3);
    GameObject[] circleEffects = GameObject.FindGameObjectsWithTag("CircleEffect");
    foreach (GameObject effect in circleEffects)
    {
      Destroy(effect);
    }

    isSpawning = false;
  }

  private GameObject pickRandomObject(GameObject[] objects)
  {
    int randomIndex = Random.Range(0, objects.Length);
    return objects[randomIndex];
  }

  private bool checkCollider(GameObject chosenObject)
  {
    Collision collision = chosenObject.GetComponent<Collision>();

    if (collision.gameObject.tag == "Throwable")
      return false;

    return true;
  }
}
