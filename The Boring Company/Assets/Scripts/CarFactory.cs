using UnityEngine;

public class CarFactory : MonoBehaviour {
    private static CarFactory instance;

    public static CarFactory Instance =>
        instance ?? (instance = FindObjectOfType<CarFactory>());

    private void Awake() {
        this.Destinations = GameObject.FindObjectsOfType<CarExitElevator>();
    }

    [SerializeField]
    private CarRunner[] CarRunners;

    [SerializeField]
    private CarExitElevator[] Destinations;

    public CarRunner GetCar(Transform spawnParent) {
        PlayerManager.Instance.Bitcoins.Value += Random.Range(0.0f, 1.0f);
        var runner = Instantiate(
            this.CarRunners[Random.Range(0, this.CarRunners.Length)],
            spawnParent);
        runner.transform.localPosition = new Vector3(0, 0, 0);
        runner.Target =
            this.Destinations[Random.Range(0, this.Destinations.Length)]
                .transform;
        return runner;
    }
}