using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
  float timer = 3;
  // 生成間隔
  float instantiateInterval = 3;

  // 生成する残数
  public static int instantiateValue;

  public GameObject enemy;

  void Awake()
  {
    instantiateValue = 50;
  }

  // Update is called once per frame
  void Update()
  {
    timer -= Time.deltaTime;

    // 一定時間ごとに敵を生成
    if (timer < 0)
    {
      if (instantiateValue > 0)
      {
        // Debug.Log("Enemy Advent!");
        // 敵をランダムな位置に生成
        Instantiate(enemy, new Vector3(Random.Range(0.0f, 500.0f), Random.Range(10, 50.0f), Random.Range(0.0f, 500.0f)), Quaternion.identity);
        instantiateValue--;
      }

      // 生成間隔を減らしていく
      instantiateInterval -= 0.1f;
      instantiateInterval = Mathf.Clamp(instantiateInterval, 1.0f, float.MaxValue);

      timer = instantiateInterval;
    }

  }
}
