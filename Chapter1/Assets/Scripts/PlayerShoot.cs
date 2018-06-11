using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
  void Start()
  {

  }

  public GameObject shot;

  void Update()
  {
    // 弾を発射する
    if (Input.GetButton("Fire1"))
    {
      Instantiate(shot, transform.position, transform.rotation);
    }
  }
}
