using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ScreenOverlayManager : MonoBehaviour {
  ScreenOverlay screenOverlay;
  public static float intensity;

	void Start () {
    screenOverlay = GetComponent<ScreenOverlay>();
    intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
    screenOverlay.intensity = intensity;

    intensity -= Time.deltaTime;
    intensity = Mathf.Clamp(intensity, 0, 1.0f);
		
	}
}
