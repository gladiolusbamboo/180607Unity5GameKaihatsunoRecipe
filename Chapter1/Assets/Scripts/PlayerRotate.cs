using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
  void Start()
  {

  }

  void Update()
  {
    // プレイヤーを回転させる
    transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

    // 親オブジェクト（プレイヤーの中心）を中心にカメラを回転させる
    GameObject CameraPoint = Camera.main.transform.parent.gameObject;
    CameraPoint.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);
  }
}
