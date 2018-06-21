using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerMove : MonoBehaviour
{
  public float speed = 15.0f;
  public float jumpSpeed = 8.0f;
  public float gravity = 20.0f;
  private Vector3 moveDirection = Vector3.zero;

  int boostPoint;
  int boostPointMax = 100;

  public Image gaugeImage;

  Vector3 moveSpeed;

  // 通常時の加速度
  const float addNormalSpeed = 1;
  // ブースト時の加算速度
  const float addBoostSpeed = 2;
  // 通常時の最大速度
  const float moveSpeedMax = 20;
  // ブースト時の最大速度
  const float boostSpeedMax = 40;

  bool isBoost;

  void Start()
  {
    boostPoint = boostPointMax;

    moveSpeed = Vector3.zero;

    isBoost = false;
  }

  void Update()
  {
    // プレイヤーを移動させる
    CharacterController controller = GetComponent<CharacterController>();

    // 接地している場合Y軸方向の移動はさせない
    if (controller.isGrounded)
      moveDirection.y = 0;

    // ブーストボタンが押されていればフラグを立て、ブーストポイントを消費
    if(Input.GetButton("Boost") && boostPoint > 1)
    {
      boostPoint -= 1;
      isBoost = true;
    }
    else
    {
      isBoost = false;
    }

    // 目標速度
    Vector3 targetSpeed = Vector3.zero;
    // 加算速度
    Vector3 addSpeed = Vector3.zero;

    // 左右移動時の目標速度と加算速度
    // 左右方向にキーが入ってない時
    if (Input.GetAxis("Horizontal") == 0)
    {
      // 押していないときは目標速度は０
      targetSpeed.x = 0;

      // 接地している時と空中にいるときは減速値を変える
      // 接地しているときは
      if (controller.isGrounded)
      {
        // 加算（減速）速度は通常
        addSpeed.x = addNormalSpeed;
      }
      // 空中にいるときは
      else
      {
        // 加算（減速）速度は1/4
        addSpeed.x = addNormalSpeed / 4;
      }
    }
    // 左右方向にキーが入っている時
    else
    {
      // 通常時とブースト時で変化
      // ブースト時なら
      if (isBoost)
      {
        // 目標速度はブースト最大速度
        targetSpeed.x = boostSpeedMax;
        // 加算（加速）速度はブースト加算速度
        addSpeed.x = addBoostSpeed;
      }
      // 通常移動時なら
      else
      {
        // 目標速度は通常移動の最大速度
        targetSpeed.x = moveSpeedMax;
        // 加算（加速）速度は通常移動の加算速度
        addSpeed.x = addNormalSpeed;
      }
      // Mathf.Signは正負によって1か-1を返す
      // 目標速度に左右のどちらに向かっているかの情報を加える
      targetSpeed.x *= Mathf.Sign(Input.GetAxis("Horizontal"));
    }

    // 左右移動の速度
    // MoveTowards関数を使用して自然な移動速度にする
    moveSpeed.x = Mathf.MoveTowards(moveSpeed.x, targetSpeed.x, addSpeed.x);
    moveDirection.x = moveSpeed.x;

    // 前後移動の目標速度と加算速度
    // 前後方向にキーが入ってない時
    if (Input.GetAxis("Vertical") == 0)
    {
      // 押していないときは目標速度は０
      targetSpeed.z = 0;

      // 接地している時と空中にいるときは減速値を変える
      // 接地しているときは
      if (controller.isGrounded)
      {
        // 加算（減速）速度は通常
        addSpeed.z = addNormalSpeed;
      }
      // 空中にいるときは
      else
      {
        // 加算（減速）速度は1/4
        addSpeed.z = addNormalSpeed / 4;
      }
    }
    // 前後方向にキーが入っている時
    else
    {
      // ブースト時なら
      if (isBoost)
      {
        // 目標速度はブースト最大速度
        targetSpeed.z = boostSpeedMax;
        // 加算（加速）速度はブースト加算速度
        addSpeed.z = addBoostSpeed;
      }
      // 通常移動時なら
      else
      {
        // 目標速度は通常移動の最大速度
        targetSpeed.z = moveSpeedMax;
        // 加算（加速）速度は通常移動の加算速度
        addSpeed.z = addNormalSpeed;
      }

      // Mathf.Signは正負によって1か-1を返す
      // 目標速度に前後のどちらに向かっているかの情報を加える
      targetSpeed.z *= Mathf.Sign(Input.GetAxis("Vertical"));
    }

    // 前後移動の速度
    // MoveTowards関数を使用して自然な移動速度にする
    moveSpeed.z = Mathf.MoveTowards(moveSpeed.z, targetSpeed.z, addSpeed.z);
    moveDirection.z = moveSpeed.z;

    // ローカル座標からワールド座標の情報に変換する
    moveDirection = transform.TransformDirection(moveDirection);

    // ジャンプキーによる上昇
    if (Input.GetButton("Jump") && boostPoint > 1)
    {
      // 高度100以上は上昇しない
      if (transform.position.y > 100)
        moveDirection.y = 0;
      else
      {
        // ブーストで重力の逆方向分上昇
        moveDirection.y += gravity * Time.deltaTime;
      }
      boostPoint -= 1;
    }
    else
    {
      // 重力を考慮する
      moveDirection.y -= gravity * Time.deltaTime;
    }

    // ブーストもしくはジャンプモードでなければboostPoint回復
    if (!Input.GetButton("Boost") && !Input.GetButton("Jump"))
      boostPoint += 2;
    // boostPointが規定値におさまるように調整
    boostPoint = Mathf.Clamp(boostPoint, 0, boostPointMax);

    // 動かす
    controller.Move(moveDirection * Time.deltaTime);

    // 移動速度に合わせてモーションブラーの値を変える
    float motionBlurValue = Mathf.Max(Mathf.Abs(moveSpeed.x), Mathf.Abs(moveSpeed.z)) / 20;
    motionBlurValue = Mathf.Clamp(motionBlurValue, 0, 5);

    Camera.main.GetComponent<CameraMotionBlur>().velocityScale = motionBlurValue;

    // ブーストゲージの伸縮
    gaugeImage.transform.localScale = new Vector3((float)boostPoint / boostPointMax, 1, 1);    
  }
}
