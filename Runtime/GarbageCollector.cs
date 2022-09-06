using System;
using UnityEngine;
using System.Collections;

namespace Cobilas.Unity.Mono {
    /// <summary>Cobilas Garbage Collector.</summary>
    public class GarbageCollector : CobilasBehaviour, ISerializationCallbackReceiver {

        [SerializeField, Range(10f, 1500f)]
        private int collectionInterval = 30;
        private Coroutine coroutine;
        private bool AfterDeserialize;
        private static GarbageCollector garbage;
#if UNITY_EDITOR
        [SerializeField] private bool printLog;
#endif

        private void Awake() {
            if (garbage != null) {
                Destroy(gameObject);
                return;
            }
            garbage = FindObjectOfType<GarbageCollector>();
            DontDestroyOnLoad(gameObject);
            AfterDeserialize = true;
            OnEnable();
        }

        private void OnEnable() {
            if (!AfterDeserialize) return;
            AfterDeserialize = false;
            coroutine = StartCoroutine(Collect());
            garbage = garbage == null ? FindObjectOfType<GarbageCollector>() : garbage;
        }

//        private void LateUpdate() {
//            if ((Time.frameCount % collectionInterval) == 0) {
//#if UNITY_EDITOR
//                print("Garbage Collector");
//#endif
//                GC.Collect();
//            }
//        }

        private IEnumerator Collect() {
            while (true) {
                if ((Time.frameCount % collectionInterval) == 0) {
#if UNITY_EDITOR
                    if (printLog)
                        print("Garbage Collector");
#endif
                    GC.Collect();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
            => AfterDeserialize = true;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}