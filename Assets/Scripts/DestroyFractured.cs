using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFractured : MonoBehaviour
{
    private Vector3 OriginalPosition;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(DestroyTimer());
        OriginalPosition = this.gameObject.transform.position - transform.position;
        this.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * -500, OriginalPosition.normalized);
        this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(-500, OriginalPosition, 500);
       
  }

  // Update is called once per frame
  void Update()
  {
      
        
  }

  IEnumerator DestroyTimer()
  {
    yield return new WaitForSeconds(5);
    Destroy(gameObject);
  }


}
