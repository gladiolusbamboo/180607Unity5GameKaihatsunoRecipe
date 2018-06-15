using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
  GameObject CameraParent;
  Quaternion defaultCameraRot;
  float timer = 0;

  void Start()
  {
    CameraParent = Camera.main.transform.parent.gameObject;
    defaultCameraRot = CameraParent.transform.localRotation;
  }

  void Update()
  {
    // プレイヤーを回転させる
    transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

    // 親オブジェクト（プレイヤーの中心）を中心にカメラを回転させる
    GameObject CameraPoint = Camera.main.transform.parent.gameObject;
    CameraPoint.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);

    // カメラの回転をリセットする
    if (Input.GetButton("CamReset"))
    {
      // 0.5f secでカメラ位置を戻している気がする
      timer = 0.5f;
    }

    // スムーズにカメラの回転を戻す
    if (timer > 0)
    {
      CameraParent.transform.localRotation = Quaternion.Slerp(CameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

      timer -= Time.deltaTime;
    }
  }
}
