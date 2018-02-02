using System;

using UniRx;
using UniRx.Triggers;

using UnityEngine;

using Random = UnityEngine.Random;

public class SpawnCarElevator : MonoBehaviour {
    [SerializeField]
    private GameObject elevator;

    private CompositeDisposable disposables;

    private void Awake() {
        this.disposables = new CompositeDisposable();
    }

    private bool isSpawning = false;

    // Use this for initialization
    void Start () {
        this.elevator.gameObject.OnTriggerEnterAsObservable().Subscribe(
            x => {
                if (isSpawning) {
                    return;
                }

                if (x.gameObject.GetComponent<ElevatorPlatform>() != null) {
                    var carCount = 0;
                    var spawnLimit = Random.Range(
                        0,
                        CarManager.Instance.SpawnLimit + 1);
                    isSpawning = true;
                    this.elevator.GetComponent<Rigidbody>().velocity =
                        Vector3.zero;

                    Observable
                        .Interval(
                            TimeSpan.FromMilliseconds(
                                CarManager.Instance.SpawnInterval))
                        .Subscribe(
                            _ => {
                                CarFactory.Instance.GetCar(this.transform);
                                carCount++;
                                if (carCount >= spawnLimit) {
                                    this.disposables.Clear();
                                    this.elevator.GetComponent<Rigidbody>().velocity =
                                        this.elevator.gameObject.transform.up * 5;
                                    isSpawning = false;
                                }
                            }).AddTo(this.disposables);

                }

                if (x.gameObject.GetComponent<ElevatorStopper>() != null) {
                    this.elevator.GetComponent<Rigidbody>().velocity =
                        this.elevator.gameObject.transform.up * -10;
                }
            });
    }

    
    
    // Update is called once per frame
    void Update () {

/*        if (Random.value < .5)*/
            
    }

    private void OnTriggerEnter(Collider other) {
    }
}
