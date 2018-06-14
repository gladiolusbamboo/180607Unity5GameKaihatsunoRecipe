using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
  GameObject target = null;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetButtonDown("Lock"))
    {
      // ロックを解除する
      if (target != null)
        target = null;
      else
        // 一番近いターゲットを取得する
        target = FindClosestEnemy();
        // ターゲットを取得する
        //target = GameObject.FindWithTag("Enemy");
    }

    if (target != null)
    {
      // ターゲットの方向を向く
      // transform.LookAt(target.transform);

      // スムーズにターゲットの方向を向く
      Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
      // ただしX軸とZ軸は回転させない
      transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

      // カメラをターゲットに向ける
      Transform cameraParent = Camera.main.transform.parent;
      // カメラをどう回転させるかを取得
      Quaternion targetRotation2 = Quaternion.LookRotation(target.transform.position - cameraParent.position);
      // 速度を指定してカメラを回転させる
      cameraParent.localRotation = Quaternion.Slerp(cameraParent.localRotation, targetRotation2, Time.deltaTime * 10);
      // ただしY軸とZ軸は回転させない
      cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);
    }
  }

  // 一番近い敵を探して取得する
  private GameObject FindClosestEnemy()
  {
    GameObject[] gos;
    gos = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject closest = null;
    float distance = Mathf.Infinity;
    Vector3 position = transform.position;

    foreach(GameObject go in gos)
    {
      Vector3 diff = go.transform.position - position;
      float curDistance = diff.sqrMagnitude;

      if(curDistance < distance)
      {
        closest = go;
        distance = curDistance;
      }
    }

    return closest;
  }
}
