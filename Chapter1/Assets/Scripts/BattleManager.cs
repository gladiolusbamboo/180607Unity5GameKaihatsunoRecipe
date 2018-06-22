using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
  int battleStatus;

  const int BATTLE_START = 0;
  const int BATTLE_PLAY = 1;
  const int BATTLE_END = 2;

  float timer;

  public Image messageStart;
  public Image messageWin;
  public Image messageLose;

  // 敵を倒した数。Enemyスクリプトでカウントアップ
  public static int score;

  // クリア条件となるスコア
  int clearScore;

  void Start()
  {
    battleStatus = BATTLE_START;

    timer = 0;

    messageStart.enabled = true;
    messageWin.enabled = false;
    messageLose.enabled = false;

    score = 0;

    // 敵の最大生成数をクリア数にする
    clearScore = EnemyInstantiate.instantiateValue;
  }

  void Update()
  {
    switch (battleStatus)
    {
      case BATTLE_START:
        timer += Time.deltaTime;

        // 時間経過でメッセージを消して状態移行
        if (timer > 3)
        {
          messageStart.enabled = false;
          
          battleStatus = BATTLE_PLAY;

          timer = 0;
        }
        break;
      case BATTLE_PLAY:
        // スコアが出現数に到達したら勝利
        if (score >= clearScore)
        {
          battleStatus = BATTLE_END;
          messageWin.enabled = true;
        }

        if (PlayerAp.armorPoint <= 0)
        {
          battleStatus = BATTLE_END;
          messageLose.enabled = true;
        }
        break;
      case BATTLE_END:
        timer += Time.deltaTime;

        if (timer > 3)
        {
          // 動きを止める
          Time.timeScale = 0;

          if (Input.GetButton("Fire1"))
          {
            SceneManager.LoadScene("Title");

            // 動きを再開する
            Time.timeScale = 1;
          }
        }
        break;
      default:
        break;
    }

  }
}
