using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  GameObject target;

  public GameObject shot;

  float shotInterval = 0;
  float shotIntervalMax = 1.0f;

  public GameObject explosion;

  void Start()
  {
    // ターゲットを取得
    target = GameObject.Find("PlayerTarget");
  }

  void Update()
  {
    // プレイヤーとの距離が近づくと攻撃してくる
    if (Vector3.Distance(target.transform.position, transform.position) <= 30)
    {
      // スムーズにターゲットの方向を向く
      // エネミーの向くべき方向を算出する
      Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
      // スムーズに方向を変える。第三引数はスピード
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

      shotInterval += Time.deltaTime;

      if (shotInterval > shotIntervalMax)
      {
        Instantiate(shot, transform.position, transform.rotation);
        shotInterval = 0;
      }
    }
  }

  private void OnCollisionEnter(Collision collider)
  {
    // プレイヤーの弾と衝突したら消滅する
    if(collider.gameObject.tag == "Shot")
    {
      Destroy(gameObject);
      Instantiate(explosion, transform.position, transform.rotation);
    }
  }
}
