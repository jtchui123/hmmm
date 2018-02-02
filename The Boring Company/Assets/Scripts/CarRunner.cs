using System;
using System.Collections;
using System.Collections.Generic;

using Pathfinding;

using UniRx;

using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(AIPath))]
public class CarRunner : MonoBehaviour {
    [Serializable]
    public enum States {
        Normal,
        BoosterI
    }

    [SerializeField]
    private Seeker seeker;

    [SerializeField]
    private AudioClip[] honks;

    public Transform Target { get; set; }

    private Track track;

    private CompositeDisposable disposables;

    private void Awake() {
        this.disposables = new CompositeDisposable();

    }

    [SerializeField]
    private States state;

    public States State => this.state;

    private void OnDestroy() {
        this.disposables.Dispose();
    }

    // Use this for initialization
    void Start () {
        
        this.seeker.StartPath(
            this.transform.position,
            this.Target.position,
            this.OnPathComplete);
/*        Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(
            _ => {
/*               this.seeker.StartPath(
                            this.transform.position,
                            this.Target.position,
                            this.OnPathComplete);#1#
            }).AddTo(this.disposables);*/

    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("sadasdsad");
        this.seeker.StartPath(
            this.transform.position,
            this.Target.position,
            this.OnPathComplete);
    }

    private void OnPathComplete(Path path) {
        
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnCollisionStay(Collision other) {
        this.track = other.gameObject.GetComponent<Track>();
        if (this.track == null) {
            AudioSource.PlayClipAtPoint(
                this.honks[Random.Range(0, this.honks.Length)],
                this.transform.position);
            PlayerManager.Instance.Disapprove();
            return;
        }

        this.GetComponent<AIPath>().maxSpeed = this.track.SpeedLimit;
        if (this.state == States.BoosterI) {
            this.GetComponent<AIPath>().maxSpeed += Random.value;
        }
    }
}
