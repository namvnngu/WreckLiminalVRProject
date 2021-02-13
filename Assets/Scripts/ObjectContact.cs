using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContact : MonoBehaviour
{
  private GameObject CollidingObject;
  private GameObject CollidedObject;
  private Vector3 OriginalObjectPosition;
    private bool hasObject = false;

  // Start is called before the first frame update

  public void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Throwable")
    {
      CollidingObject = other.gameObject;
    }
  }
  public void OnTriggerExit(Collider other)
  {
    CollidingObject = null;
  }
  private void Update()
  {
        if (hasObject == false)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.1 && CollidingObject)
            {
                PickUpObject();
            }
        }
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0 && CollidingObject)
            {
                ThrowObject();
                hasObject = false;
            }
    }
  public void PickUpObject()
  {
    CollidedObject = CollidingObject;
    CollidedObject.transform.SetParent(this.transform);
    CollidedObject.GetComponent<Rigidbody>().isKinematic = true;
    hasObject = true;
  }
  private void ThrowObject()
  {
    OriginalObjectPosition = CollidedObject.transform.position;
    CollidedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    CollidedObject.GetComponent<Rigidbody>().useGravity = true;
    CollidedObject.GetComponent<Rigidbody>().isKinematic = false;
    CollidedObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 200, OriginalObjectPosition);
    CollidedObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.up * 5, OriginalObjectPosition);
    CollidedObject.transform.SetParent(null);
    CollidedObject = null;
  }
}
