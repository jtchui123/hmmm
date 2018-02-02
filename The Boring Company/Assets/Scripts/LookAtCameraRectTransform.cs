using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts {
    using UnityEngine;
    class LookAtCameraRectTransform : MonoBehaviour {
        // Use this for initialization
        void Start () {
        }
	
        // Update is called once per frame
        void Update () {
            this.GetComponent<RectTransform>().LookAt(Camera.main.transform.position);
        }
    }
}
