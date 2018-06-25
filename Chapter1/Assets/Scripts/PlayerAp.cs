using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets.ImageEffects;

public class PlayerAp : MonoBehaviour
{
  public static int armorPoint;
  int armorPointMax = 5000;

  int damage = 100;

  public Text armorText;

  int displayArmorPoint;

  public Color myWhite;
  public Color myYellow;
  public Color myRed;

  public Image gaugeImage;

  void Start()
  {
    armorPoint = armorPointMax;
    displayArmorPoint = armorPoint;

    Camera.main.GetComponent<NoiseAndScratches>().enabled = false;
  }

  void Update()
  {
    // 現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
    if (displayArmorPoint != armorPoint)
      displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1f);

    // 現在の体力と最大体力をUI Textに表示する
    armorText.text = string.Format("{0:0000} / {1:0000}", displayArmorPoint, armorPointMax);

    // 残り体力の割合により文字の色を変える
    float percentageArmorpoint = (float)displayArmorPoint / armorPointMax;

    if (percentageArmorpoint > 0.5f)
    {
      armorText.color = myWhite;
      gaugeImage.color = new Color(0.25f, 0.7f, 0.6f);
    }
    else if (percentageArmorpoint > 0.3f)
    {
      armorText.color = myYellow;
      gaugeImage.color = myYellow;
    }
    else
    {
      armorText.color = myRed;
      gaugeImage.color = myRed;

      // プレイヤーの体力が一定以下になったらノイズを有効にする
      Camera.main.GetComponent<NoiseAndScratches>().enabled = true;
    }

    // ゲージの長さを体力の割合に合わせて伸縮させる
    gaugeImage.transform.localScale = new Vector3(percentageArmorpoint, 1, 1);
  }

  private void OnCollisionEnter(Collision collider)
  {
    // 敵の弾と衝突したらダメージ
    if (collider.gameObject.tag == "ShotEnemy")
    {
      armorPoint -= damage;
      // armorPointが0から最大値の間に収まるようにする
      armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
    }
  }
}
