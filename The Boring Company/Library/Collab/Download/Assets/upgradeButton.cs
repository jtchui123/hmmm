using System.Collections;
using System.Collections.Generic;

using UniRx;

using UnityEngine;
using UnityEngine.UI;

public class upgradeButton : MonoBehaviour {
    private Button button;

	// Use this for initialization
	void Start () {
	    this.button = this.gameObject.GetComponent<Button>();
	    this.button.OnClickAsObservable().Subscribe(
	        _ => {
	            PlayerManager.Instance.UpgradeTrack();
	        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
