using TMPro;

using UniRx;

using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;

    public static PlayerManager Instance =>
        instance ?? (instance = FindObjectOfType<PlayerManager>());

    [SerializeField]
    private TextMeshProUGUI BitcoinsText;

    public ReactiveProperty<Track> CurrentSelectedTrack { get; set; }

    public FloatReactiveProperty Bitcoins { get; private set; }

    public FloatReactiveProperty ApprovalRating { get; private set; }

    private void Awake() {
        this.ApprovalRating = new FloatReactiveProperty(85.0f);
        this.ApprovalRating.Where(x => x > 100.0f).Subscribe(
            _ => { this.ApprovalRating.Value = 100.0f; });
        this.ApprovalRating.Where(x => x <= 0).Subscribe(
            _ => {
                Time.timeScale = 0.0f;
                foreach (var go in FindObjectsOfType<GameObject>()) {
                    GameObject.Destroy(go);
                }
            });
        this.Bitcoins = new FloatReactiveProperty();
        this.CurrentSelectedTrack = new ReactiveProperty<Track>();
        this.Bitcoins.Subscribe(
            x => { this.BitcoinsText.text = x.ToString("F2"); });

    }

    public void RepairTrack(float cost) {
        if (this.Bitcoins.Value >= cost
                && CurrentSelectedTrack.Value.State.Value != Track.States.Good) {
            this.Bitcoins.Value = this.Bitcoins.Value - cost;
            var currentState = CurrentSelectedTrack.Value.State;
            if (currentState.Value == Track.States.Medium) {
                currentState.Value = Track.States.Good;
            }
            else {
                currentState.Value = Track.States.Medium;
            }
        }
    }

    public void UpgradeTrack() {
        if (this.Bitcoins.Value >= 1.0f) {
            this.Bitcoins.Value = this.Bitcoins.Value - 1.0f;
            CurrentSelectedTrack.Value.NumCarsToDegrade += 10;
        }
    }

    public void Disapprove() {
        this.ApprovalRating.Value =
            this.ApprovalRating.Value -= Random.Range(0.01f, 0.03f);
    }

    private void Update() {
/*        this.Bitcoins.Value += Random.Range(0.0f, 1.0f);*/
    }
}