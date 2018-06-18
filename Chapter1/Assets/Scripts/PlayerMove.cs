﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public float speed = 15.0f;
  public float jumpSpeed = 8.0f;
  public float gravity = 20.0f;
  private Vector3 moveDirection = Vector3.zero;

  void Start()
  {

  }

  void Update()
  {
    // プレイヤーを移動させる
    CharacterController controller = GetComponent<CharacterController>();

    if (controller.isGrounded)
    {
      // 動く方向を取得するGetAxis()は-1～+1の値を返す
      moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      // ローカル座標をワールド座標に変換する
      moveDirection = transform.TransformDirection(moveDirection);
      moveDirection *= speed;

      // ジャンプ処理
      if (Input.GetButton("Jump"))
        moveDirection.y = jumpSpeed;
    }

    // 重力を考慮する
    moveDirection.y -= gravity * Time.deltaTime;

    // 動かす
    controller.Move(moveDirection * Time.deltaTime);
  }
}
