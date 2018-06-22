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

  // いわゆるヒットポイント
  public int armorPoint;
  public int armorPointMax = 1000;
  int damage = 100;

  float timer = 0;

  int enemyLevel = 0;

  void Start()
  {
    // ターゲットを取得
    target = GameObject.Find("PlayerTarget");

    armorPoint = armorPointMax;
  }

  void Update()
  {
    timer += Time.deltaTime;

    // 経過時間に応じてレベルを上げる
    if (timer < 5)
      enemyLevel = 1;
    else if (timer < 10)
      enemyLevel = 2;
    else if (timer < 15)
      enemyLevel = 3;
    else if (timer >= 15)
    {
      enemyLevel = 4;
      // レベル４：攻撃間隔が短くなる
      shotIntervalMax = 0.5f;
    }

    // レベル２：プレイヤーが一定範囲に近づいたら攻撃
    if (enemyLevel >= 2)
    {
      // プレイヤーとの距離が近づくと攻撃してくる
      if (Vector3.Distance(target.transform.position, transform.position) <= 30)
      {
        // スムーズにターゲットの方向を向く
        // エネミーの向くべき方向を算出する
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        // スムーズに方向を変える。第三引数はスピード
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        // 一定間隔でショット
        shotInterval += Time.deltaTime;

        if (shotInterval > shotIntervalMax)
        {
          Instantiate(shot, transform.position, transform.rotation);
          shotInterval = 0;
        }
      }
      else
      {
        // レベル３：プレイヤーに自分から近づく
        if (enemyLevel >= 3)
        {
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 5);
          transform.position += transform.forward * Time.deltaTime * 20;
        }
      }
    }

  }

  private void OnCollisionEnter(Collision collider)
  {
    // プレイヤーの弾と衝突したら消滅する
    if(collider.gameObject.tag == "Shot")
    {
      // ダメージをランダムで変える
      // damage = Random.Range(50, 150);

      // プレイヤーの弾からダメージを取得する
      damage = collider.gameObject.GetComponent<ShotPlayer>().damage;

      // プレイヤーの弾と衝突したらダメージ
      armorPoint -= damage;
      //Debug.Log("armorPoint = " + armorPoint);

      // 体力が０以下になったら消滅する
      if (armorPoint <= 0)
      {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);

        // リザルト用のスコアを加算する
        BattleManager.score++;
      }
    }
  }
}
