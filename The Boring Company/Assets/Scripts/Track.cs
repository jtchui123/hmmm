using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using TMPro;

using UniRx;

using UnityEngine;
using UnityEngine.EventSystems;

using Random = UnityEngine.Random;

public class Track : MonoBehaviour, IPointerClickHandler {
    public enum States {
        Good,
        Medium,
        Poor
    }

    [SerializeField]
    private float baseSpeedLimit;

    [SerializeField]
    private GameObject speedLimitText;

    public FloatReactiveProperty degradationCount;

    private float speedMulti;

    public ReactiveProperty<States> State { get; private set; }

    private HashSet<GameObject> collidedWith;

    public float SpeedLimit => this.baseSpeedLimit * this.speedMulti;

    private void OnCollisionExit(Collision other) {
        if (!this.collidedWith.Contains(other.gameObject)) {
            this.collidedWith.Add(other.gameObject);
            this.degradationCount.Value = this.degradationCount.Value + 1;
        }
    }

    private void OnCollisionEnter(Collision other) {
        OnCollisionExit(other);
    }

    public int NumCarsToDegrade { get; set; }
   

    // Use this for initialization
    void Start () {
        this.collidedWith = new HashSet<GameObject>();
        this.NumCarsToDegrade = TrackNodeManager.Instance.numCarsToDegrade;
        Observable.Interval(TimeSpan.FromMilliseconds(250)).Subscribe(
            _ => {
                switch (this.State.Value) {
                    case Track.States.Medium:
                        PlayerManager.Instance.ApprovalRating.Value +=
                            Random.Range(0.00f, 0.01f);
                        break;
                    case Track.States.Poor: {
                        break;
                    }
                    case Track.States.Good:
                        PlayerManager.Instance.ApprovalRating.Value +=
                            Random.Range(0.06f, 0.02f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        this.degradationCount = new FloatReactiveProperty();
        this.degradationCount.Subscribe(
            x => {
                if (x >= this.NumCarsToDegrade) {
                    if (this.NumCarsToDegrade <= 0) {
                        throw new InvalidOperationException();
                    }
                    if (this.State.Value == States.Good) {
                        this.State.Value = States.Medium;
                    } else if (this.State.Value == States.Medium) {
                        this.State.Value = States.Poor;
                    }

                    this.degradationCount.Value = 0;
                }
            });
        this.State = new ReactiveProperty<States> { Value = States.Good };
        var speedText = this.speedLimitText.GetComponent<TextMeshPro>();
        this.State.Subscribe(
            x => {
                this.degradationCount.Value = 0;
                var mat = this.GetComponent<MeshRenderer>().material;
                var textMat = this.speedLimitText.GetComponent<MeshRenderer>()
                    .material;
                if (x == States.Good) {
                    speedText.color = new Color(140 / 255f, 214 / 255f, 62 / 255f);
                    mat.color = Color.green;
                }

                if (x == States.Medium) {
                    speedText.color = Color.yellow;
                    mat.color = Color.yellow;
                }

                if (x == States.Poor) {
                    speedText.color = Color.red;
                    mat.color = Color.red;
                }
            });
        PlayerManager.Instance.CurrentSelectedTrack
            .Subscribe(
                x => {
                    if (x == this) {
                        this.gameObject.GetComponent<MeshRenderer>().material
                            .shader = Shader.Find(
                            "Legacy Shaders/Self-Illumin/Bumped Diffuse");
                    }
                    else {
                        this.gameObject.GetComponent<MeshRenderer>().material
                            .shader = Shader.Find(
                            "Legacy Shaders/Bumped Specular");
                    }
                });

    }
    
    // Update is called once per frame
    void Update () {
        switch (this.State.Value) {
            case Track.States.Medium:
                speedMulti = 1.0f;
                break;
            case Track.States.Poor: {
                speedMulti = 0.5f;
                break;
            }
            case Track.States.Good:
                speedMulti = 2.0f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        var speedText = this.speedLimitText.GetComponent<TextMeshPro>().text =
                            this.SpeedLimit.ToString(CultureInfo.InvariantCulture);
        if (this.SpeedLimit <= 0) {
            throw new InvalidOperationException();
        }
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        PlayerManager.Instance.CurrentSelectedTrack.Value = this;
    }
}
