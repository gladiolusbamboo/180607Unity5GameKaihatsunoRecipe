using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
  public Image compassImage;

  void Start()
  {

  }

  void Update()
  {
    // 方角の回転
    // プレイヤーのY軸に対する回転をコンパスのZ軸回転に対応させる
    // transform.rotationはQuaternion角で表されているので
    // Euler角をQuaternion角に変換している
    compassImage.transform.rotation = Quaternion.Euler(compassImage.transform.rotation.x, compassImage.transform.rotation.y, transform.eulerAngles.y);
  }
}
