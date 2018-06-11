using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // 弾を前進させる
    transform.position += transform.forward * Time.deltaTime * 100;
  }
}
