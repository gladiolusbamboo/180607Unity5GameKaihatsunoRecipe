using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour {

  public GameObject boostLight;

	void Start () {
    boostLight.SetActive(false);		
	}
	
	void Update () {
    bool flgBoost = false;

    if (Input.GetButton("Boost") || Input.GetButton("Jump"))
      flgBoost = true;

    boostLight.SetActive(flgBoost);		
	}
}
