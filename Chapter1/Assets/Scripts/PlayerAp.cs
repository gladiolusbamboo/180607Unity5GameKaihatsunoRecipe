using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAp : MonoBehaviour {
  int armorPoint;
  int armorPointMax = 5000;

  int damage = 100;

  public Text armorText;

	void Start () {
    armorPoint = armorPointMax;		
	}
	
	void Update () {
    // 体力をUI Textに表示する
    // armorText.text = armorPoint.ToString();
    // 現在の体力と最大体力をUI Textに表示する
    armorText.text = string.Format("{0:0000} / {1:0000}", armorPoint, armorPointMax);
    armorText.text = armorPoint + "/" + armorPointMax;
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
