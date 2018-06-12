using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
  public GameObject explosion;

  // Use this for initialization
  void Start()
  {
    // 出現後一定時間で自動的に消滅させる
    Destroy(gameObject, 2.0f);
  }

  // Update is called once per frame
  void Update()
  {
    // 弾を前進させる
    transform.position += transform.forward * Time.deltaTime * 100;
  }

  private void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.name == "Terrain")
    {
      // 地形とぶつかったら消滅させる
      Destroy(gameObject);
      Instantiate(explosion, transform.position, transform.rotation);
    }
  }
}
