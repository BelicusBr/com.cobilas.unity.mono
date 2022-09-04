using System;
using UnityEngine;
using System.Reflection;
using Cobilas.Collections;
using UEObject = UnityEngine.Object;

namespace Cobilas.Unity.Mono {
    public class CobilasBehaviour : MonoBehaviour {
        public Transform parent { get => transform.parent; set => transform.parent = value; }

        public static new void print(object OBJ = null)
            => MonoBehaviour.print(OBJ == null ? "Empty!!!" : OBJ);

        public static void print(char obj) => print((object)obj.EscapeSequenceToString());

        public static void ClearLog() {
#if UNITY_EDITOR
            try {
                Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
                Type type = assembly.GetType("UnityEditor.LogEntries");
                MethodInfo method = type.GetMethod("Clear");
                _ = method.Invoke((object)null, (object[])null);
            } catch (Exception E) {
                Debug.LogError($"O metodo não e compativel com a versão:{Application.unityVersion} da unity.\n" +
                    $"{E}");
            }
#endif
        }

        public static GameObject FindObjectByName(string name) {
            GameObject[] temp = FindObjectsOfType<GameObject>();
            for (int I = 0; I < ArrayManipulation.ArrayLength(temp); I++)
                if (temp[I].name == name)
                    return temp[I];
            return (GameObject)null;
        }

        public static T FindObjectByName<T>(string name) where T : UEObject {
            T[] temp = FindObjectsOfType<T>();
            for (int I = 0; I < ArrayManipulation.ArrayLength(temp); I++)
                if (temp[I].name == name)
                    return temp[I];
            return (T)null;
        }

        public static void Destroy(UEObject[] OBJ) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(OBJ); I++)
                UEObject.Destroy(OBJ[I]);
        }

        public static void Destroy(UEObject[] OBJ, float t) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(OBJ); I++)
                UEObject.Destroy(OBJ[I], t);
        }

        public static GameObject CreatePrimitive(PrimitiveType type)
            => GameObject.CreatePrimitive(type);

        public static GameObject CreatePrimitive()
            => new GameObject("GameObject Empty");
    }
}
