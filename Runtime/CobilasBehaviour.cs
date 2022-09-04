using System;
using UnityEngine;
using System.Reflection;
using Cobilas.Collections;
using UEObject = UnityEngine.Object;

namespace Cobilas.Unity.Mono {
    public class CobilasBehaviour : MonoBehaviour {
        public Transform parent { get => transform.parent; set => transform.parent = value; }

        public CobilasBehaviour() : base() { }

        public static new void print(object OBJ = null)
            => MonoBehaviour.print(OBJ == null ? "Empty!!!" : OBJ);

        /// <param name="showIndex">Mostrar os indices dos itens.</param>
        /// <param name="fromZero">Mostrar o indice a partir de zero.</param>
        public static void prints(object[] obj, bool showIndex = false, bool fromZero = true) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(obj); I++)
                print(showIndex ? $"Index: {I + (fromZero ? 0 : 1)} Item: {obj[I]}" : obj[I]);
        }

        public static void printChar(char obj) => print(obj.EscapeSequenceToString());

        /// <param name="showIndex">Mostrar os indices dos itens.</param>
        /// <param name="fromZero">Mostrar o indice a partir de zero.</param>
        public static void printChars(char[] obj, bool showIndex = false, bool fromZero = true) {
            for (int I = 0; I < obj.Length; I++)
                print(showIndex ? $"Index: {I + (fromZero ? 0 : 1)} Char: {obj[I].EscapeSequenceToString()}" : obj[I].EscapeSequenceToString());
        }

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
