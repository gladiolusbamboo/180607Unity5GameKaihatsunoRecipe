using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
  public GameObject explosion;

  public int damage = 200;

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

    // 威力減衰 最小でも１ダメージは与える
    damage--;

    if (damage <= 1)
      damage = 1;
  }

  private void OnCollisionEnter(Collision collider)
  {
    if(collider.gameObject.name == "Terrain")
    {
      // 地形とぶつかったら消滅させる
      Destroy(gameObject);
      // 衝突時にエフェクトを表示する
      Instantiate(explosion, transform.position, transform.rotation);
    }

    // 敵と衝突したら消滅させる
    if(collider.gameObject.tag == "Enemy")
    {
      Destroy(gameObject);
      // 衝突時にエフェクトを表示する
      Instantiate(explosion, transform.position, transform.rotation);
    }
  }
}
