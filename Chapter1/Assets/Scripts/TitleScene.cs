using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
  public Text blinkText;

  void Start()
  {

  }

  void Update()
  {
    // ボタンを押したら遷移
    if (Input.anyKeyDown)
    {
      SceneManager.LoadScene("Main");
    }

    // ボタンを押させるためのメッセージを点滅させる
    blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));

  }
}
