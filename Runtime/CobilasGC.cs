using System;
using UnityEngine;

namespace Cobilas.Unity.Mono {
    /// <summary>Cobilas Garbage Collector.</summary>
    public class CobilasGC : CobilasBehaviour {

        [SerializeField, Range(20f, 1500f)]
        private int collectionInterval = 30;

        private void LateUpdate() {
            if ((Time.frameCount % collectionInterval) == 0) {
#if UNITY_EDITOR
                print("Garbage Collector");
#endif
                GC.Collect();
            }
        }
    }
}