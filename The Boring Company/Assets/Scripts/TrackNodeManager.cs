using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pathfinding;

using UnityEngine;

public class TrackNodeManager : MonoBehaviour {
    private static TrackNodeManager instance;

    public static TrackNodeManager Instance =>
        instance ?? (instance = FindObjectOfType<TrackNodeManager>());

    [SerializeField]
    private float baseCostPerDistance;

    [SerializeField]
    public int numCarsToDegrade;

    private void Awake() {
        if (this.baseCostPerDistance == 0) {
            throw new DivideByZeroException();
        }
    }


    public float GetCost(Track track, TrackNode start, TrackNode end) {
        if (start.GetComponent<NodeLink>() == end.GetComponent<NodeLink>()) {
            return Int32.MaxValue / 2;
        }

        var startPosition = start.transform.position;
        var endPosition = end.transform.position;
        var distance = Math.Abs(startPosition.x - endPosition.x)
                       + Math.Abs(startPosition.y - endPosition.y)
                       + Math.Abs(startPosition.z - endPosition.z);
        var cost = distance / this.baseCostPerDistance;
        cost *= 1.0f / track.SpeedLimit;
        return cost;
    }
}