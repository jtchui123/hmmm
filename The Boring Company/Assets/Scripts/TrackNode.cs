using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Pathfinding;

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(NodeLink))]
public class TrackNode : MonoBehaviour {
    private Track track;

    private void Awake() {
        this.GetComponent<MeshRenderer>().enabled = false;
        var closestTrack = FindClosestTarget();
        if (closestTrack.GetComponent<Track>() != null) {
            this.track = closestTrack.GetComponent<Track>();
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        Gizmos.DrawWireCube(this.transform.position, Vector3.one);
        GL.PopMatrix();
    }

    private GameObject FindClosestTarget() {
        var position = this.transform.position;
        return GameObject.FindGameObjectsWithTag("TRACK")
            .OrderBy(o => (o.transform.position - position).sqrMagnitude)
            .FirstOrDefault();
    }

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        var link = this.GetComponent<NodeLink>();
        link.costFactor = TrackNodeManager.Instance.GetCost(
            this.track,
            this,
            link.End.gameObject.GetComponent<TrackNode>());
    }
}
