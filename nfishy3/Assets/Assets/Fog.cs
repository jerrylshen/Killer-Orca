using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fog : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RenderSettings.fogColor = Camera.main.backgroundColor;
        RenderSettings.fogDensity = 0.01F;
        RenderSettings.fog = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
