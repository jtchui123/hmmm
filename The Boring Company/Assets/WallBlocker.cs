using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlocker : MonoBehaviour {

	// Use this for initialization
	private void Start () {
	    this.GetComponent<MeshRenderer>().enabled = false;
	}

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        Gizmos.DrawWireCube(this.transform.position, Vector3.one);
        GL.PopMatrix();
    }

    // Update is called once per frame
	void Update () {
		
	}
}
