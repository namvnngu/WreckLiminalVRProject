using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
  public GameObject FracturedVersion;
  public float BreakForce = 4.5f;
  protected Rigidbody rb;
  private int active = 0;
  public ParticleSystem ExplosionEffect;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.relativeVelocity.magnitude > BreakForce && active == 0)
    {
      active++;
      gameObject.transform.position += new Vector3(0.01f, 0.01f, 0.01f);
      Explode();  
      Instantiate(FracturedVersion, transform.position, transform.rotation);
      rb.AddExplosionForce(100f, Vector3.zero, 5f);
      FracturedVersion.GetComponent<Rigidbody>().AddExplosionForce(500f, Vector3.zero, 100f);
    }
  }
  private void OnCollisionExit(Collision collision)
  {
    Destroy(gameObject);
  }

    private void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
    }
}
