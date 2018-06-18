using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
  public float destroyTime = 0.1f;

  void Start()
  {
    Destroy(this.gameObject, destroyTime);
  }

  void Update()
  {

  }
}
