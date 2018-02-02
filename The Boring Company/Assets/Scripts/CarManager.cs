using System;

using UniRx;

using UnityEngine;

public class CarManager : MonoBehaviour {
    private static CarManager instance;

    public static CarManager Instance =>
        instance ?? (instance = FindObjectOfType<CarManager>());

    [SerializeField]
    private int spawnLimit;

    [SerializeField]
    private int spawnInterval;

    public int SpawnLimit => this.spawnLimit;

    public float SpawnInterval => this.spawnInterval;

    private void Awake() {
        Observable.Interval(TimeSpan.FromSeconds(30)).Subscribe(
            _ => { this.spawnLimit++; });
    }
}