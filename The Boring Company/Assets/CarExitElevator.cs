using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExitElevator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other) {
        GameObject.Destroy(other.gameObject);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
