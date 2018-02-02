using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class StatsPanel : MonoBehaviour {
    
    [SerializeField]
    private TextMeshProUGUI maxDegradationText;
    
    [SerializeField]
    private TextMeshProUGUI degradationText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (PlayerManager.Instance.CurrentSelectedTrack.Value == null) {
	        return;
	    }
	    this.maxDegradationText.text = PlayerManager.Instance.CurrentSelectedTrack.Value
	        .NumCarsToDegrade.ToString();
	    this.degradationText.text = PlayerManager.Instance.CurrentSelectedTrack.Value
	        .degradationCount.ToString();
	}
}
