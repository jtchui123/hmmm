using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UniRx;
using UniRx.Triggers;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class RepairButton : MonoBehaviour {
    private Button button;

    private float repairCost;

    private TextMeshProUGUI costText;
    // Use this for initialization
    void Start () {
        
        this.repairCost = Random.value;
        this.button = this.gameObject.GetComponent<Button>();
        this.costText = this.GetComponentInChildren<TextMeshProUGUI>();
        this.costText.text = "$" + this.repairCost.ToString("F") + " BTC";
/*        this.gameObject.GetComponent<Button>().enabled = false;*/
        Observable.Interval(TimeSpan.FromSeconds(5)).Subscribe(
            _ => {
                this.repairCost += Random.Range(0.03f, 0.08f);
                this.costText.text = "$" + this.repairCost.ToString("F") + " BTC";
            });
        this.button.OnClickAsObservable().Subscribe(
            _ => {
                PlayerManager.Instance.RepairTrack(this.repairCost);
            });
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Jump")) {
            PlayerManager.Instance.RepairTrack(this.repairCost);
        }

        /*        if (this.button.enabled) {
                    this.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                }
                else {
                    this.gameObject.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);
                }*/
    }
}
