using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
  Image marker;
  public Image markerImage;
  GameObject compass;

  GameObject target;

  void Start()
  {
    // PlayerTargetはプレイヤーの中心
    target = GameObject.Find("PlayerTarget");
    // マーカーをレーダー（コンパス）上に表示する
    compass = GameObject.Find("CompassMask");
    // マーカーイメージを取得して
    marker = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
    // コンパスのマスクの子要素にする（各エネミーごとに実行される）
    marker.transform.SetParent(compass.transform, false);
  }

  void Update()
  {
    // マーカーをプレイヤーの相対位置に配置する
    Vector3 position = transform.position - target.transform.position;
    marker.transform.localPosition = new Vector3(position.x, position.z, 0);
    /*
    // レーダーの範囲外に出たら表示しない
    if (Vector3.Distance(target.transform.position, transform.position) <= 150)
      marker.enabled = true;
    else
      marker.enabled = false;
    */
  }

  // 敵が消滅したら敵のマーカーも消滅させる
  void OnDestroy()
  {
    Destroy(marker);  
  }
}
