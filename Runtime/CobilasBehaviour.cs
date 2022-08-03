using System;
using UnityEngine;
using System.Reflection;
using Cobilas.Collections;
using UEObject = UnityEngine.Object;

namespace Cobilas.Unity.Mono {
    public class CobilasBehaviour : MonoBehaviour {
        public Transform parent { get => transform.parent; set => transform.parent = value; }

        public CobilasBehaviour() : base() { }
        #region Position
        public void SetPosition(float x, float y, float z) => SetPosition(new Vector3(x, y, z));

        public void SetPosition(float x, float y) => SetPosition(x, y, 0f);

        public void SetPosition(Vector3 pos) => transform.position = pos;

        public void SetPosition(Vector2 pos) => SetPosition(pos.x, pos.y);

        public Vector3 GetPosition() => transform.position;

        public void SetLocalPosition(float x, float y, float z) => SetLocalPosition(new Vector3(x, y, z));

        public void SetLocalPosition(float x, float y) => SetLocalPosition(x, y, 0f);

        public void SetLocalPosition(Vector3 pos) => transform.localPosition = pos;

        public void SetLocalPosition(Vector2 pos)=> SetLocalPosition(pos.x, pos.y);

        public Vector3 GetLocalPosition() => transform.localPosition;
        #endregion
        #region EulerAngles
        public void SetEulerAngles(float x, float y, float z) => SetEulerAngles(new Vector3(x, y, z));

        public void SetEulerAngles(float x, float y) => SetEulerAngles(x, y, 0f);

        public void SetEulerAngles(Vector3 pos) => transform.eulerAngles = pos;

        public void SetEulerAngles(Vector2 pos) => SetEulerAngles(pos.x, pos.y);

        public Vector3 GetEulerAngles() => transform.eulerAngles;

        public void SetLocalEulerAngles(float x, float y, float z) => SetLocalEulerAngles(new Vector3(x, y, z));

        public void SetLocalEulerAngles(float x, float y) => SetLocalEulerAngles(x, y, 0f);

        public void SetLocalEulerAngles(Vector3 pos) => transform.localEulerAngles = pos;

        public void SetLocalEulerAngles(Vector2 pos) => SetLocalEulerAngles(pos.x, pos.y);

        public Vector3 GetLocalEulerAngles() => transform.localEulerAngles;
        #endregion
        #region LocalScale
        public Vector3 GetLossyScale() => transform.lossyScale;

        public void SetLocalScale(float x, float y, float z) => SetLocalScale(new Vector3(x, y, z));

        public void SetLocalScale(float x, float y) => SetLocalScale(x, y, 0f);

        public void SetLocalScale(Vector3 pos) => transform.localScale = pos;

        public void SetLocalScale(Vector2 pos) => SetLocalScale(pos.x, pos.y);

        public Vector3 GetLocalScale() => transform.localScale;
        #endregion
        #region Rotation
        public void SetRotation(float x, float y, float z, float w) => SetRotation(new Quaternion(x, y, z, w));

        public void SetRotation(Quaternion rot) => transform.rotation = rot;

        public void SetRotation(Vector4 rot) => SetRotation(rot.x, rot.y, rot.z, rot.w);

        public Quaternion GetRotation() => transform.rotation;

        public void SetLocalRotation(float x, float y, float z, float w) => SetLocalRotation(new Quaternion(x, y, z, w));

        public void SetLocalRotation(Quaternion rot) => transform.localRotation = rot;

        public void SetLocalRotation(Vector4 rot) => SetLocalRotation(rot.x, rot.y, rot.z, rot.w);

        public Quaternion GetLocalRotation() => transform.localRotation;
        #endregion

        public void Destroy(UEObject[] OBJ)
            => gameObject.Destroy(OBJ);

        public void Destroy()
            => Destroy(gameObject);

        public void Destroy<T>() where T : UEObject
            => Destroy(gameObject.GetComponent<T>());

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

        public static new void Destroy(UEObject OBJ) 
            => UEObject.Destroy(OBJ);

        public static new void Destroy(UEObject OBJ, float T)
            => UEObject.Destroy(OBJ, T);

        public static GameObject CreatePrimitive(PrimitiveType type)
            => GameObject.CreatePrimitive(type);

        public static GameObject CreatePrimitive()
            => new GameObject("GameObject Empty");
    }
}
