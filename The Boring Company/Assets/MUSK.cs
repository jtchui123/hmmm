using System.Collections;
using System.Collections.Generic;

using UniRx;

using UnityEngine;
using UnityEngine.UI;

public class MUSK : MonoBehaviour {
    [SerializeField]
    private Sprite normal;

    [SerializeField]
    private Sprite excuseMe;

    [SerializeField]
    private Sprite depressed;

    private Image image;

    // Use this for initialization
    void Start () {
        this.image = this.gameObject.GetComponent<Image>();
        PlayerManager.Instance.ApprovalRating.Subscribe(
            x => {
                if (x >= 75.0f) {
                    this.image.sprite = this.normal;
                } else if (x >= 35.0f) {
                    this.image.sprite = this.excuseMe;
                }
                else {
                    this.image.sprite = this.depressed;
                }
            });
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
