using System.Collections;
using System.Collections.Generic;

using TMPro;

using UniRx;

using UnityEngine;

public class ApprovalRating : MonoBehaviour {

    // Use this for initialization
    void Start () {
        PlayerManager.Instance.ApprovalRating.Where(x => x <= 100.0f).Subscribe(
            x => {
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text =
                    (x / 100.0f).ToString("P");
            });
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
