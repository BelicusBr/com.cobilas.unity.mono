using System;
using UnityEngine;
using System.Text;
using System.Reflection;
using Cobilas.Collections;
using System.Collections.Generic;
#if UNITY_UI_ACTIVATED
using UnityEngine.EventSystems;
#else
#warning com.unity.ugui package required
#endif
using UEObject = UnityEngine.Object;

namespace Cobilas.Unity.Mono {
    public class CobilasBehaviour : MonoBehaviour {
        private static object[] ObjetosDeDespejo;
        public Transform parent { get => transform.parent; set => transform.parent = value; }

        public CobilasBehaviour() : base() { }

        public static new void print(object OBJ = null)
            => MonoBehaviour.print((OBJ == null) ? "Nada!!!" : OBJ);

        public static void prints(object[] OBJ, bool MostraIndice = false, bool IndiceZero = true) {
            for (int I = 0; I < OBJ.Length; I++) {
                string Res = OBJ[I].ToString();
                if (MostraIndice)
                    Res = $"Indice: {(I + (IndiceZero ? 0 : 1))} Char: {Res}";
                print(Res);
            }
        }

        public static void prints<T>(T[] OBJ, bool MostraIndice = false, bool IndiceZero = true) {
            for (int I = 0; I < OBJ.Length; I++) {
                string Res = OBJ[I].ToString();
                if (MostraIndice)
                    Res = $"Indice: {(I + (IndiceZero ? 0 : 1))} Char: {Res}";
                print(Res);
            }
        }

        public static void printChar(char obj) => print(obj.EscapeSequenceToString());

        public static void printChars(char[] obj, bool MostraIndice = false, bool IndiceZero = true) {
            for (int I = 0; I < obj.Length; I++) {
                string Res = obj[I].EscapeSequenceToString();
                if (MostraIndice)
                    Res = $"Indice: {(I + (IndiceZero ? 0 : 1))} Char: {Res}";
                print(Res);
            }
        }

        public static void AddPrint(object obj)
            => ArrayManipulation.Add<object>(obj, ref ObjetosDeDespejo);

        public static void PrintObjetosDeDespejo(bool Mesclar = false) {
            if (Mesclar) {
                StringBuilder builder = new StringBuilder();
                for (int I = 0; I < ArrayManipulation.ArrayLength(ObjetosDeDespejo); I++) {
                    if (I != 0) builder.Insert(builder.Length, '|');
                    builder.Insert(builder.Length, ObjetosDeDespejo[I].ToString());
                }
            } else
                for (int I = 0; I < ArrayManipulation.ArrayLength(ObjetosDeDespejo); I++)
                    print(ObjetosDeDespejo[I].ToString());

            ArrayManipulation.ClearArraySafe<object>(ref ObjetosDeDespejo);
        }

        public static void ClearLog() {
            ArrayManipulation.ClearArraySafe<object>(ref ObjetosDeDespejo);
#if UNITY_EDITOR
            try
            {
                Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
                Type type = assembly.GetType("UnityEditor.LogEntries");
                MethodInfo method = type.GetMethod("Clear");
                method.Invoke(new object(), null);
            } catch (Exception E) {
                Debug.LogError($"O metodo não e compativel com a versão:{Application.unityVersion} da unity.\n" +
                    $"{E}");
            }
#endif
        }
        /*
        public static string InterpretarChar(char obj) {
            switch (obj) {
                case '\a': return "\\a";
                case '\b': return "\\b";
                case '\n': return "\\n";
                case '\t': return "\\t";
                case '\r': return "\\r";
                case '\f': return "\\f";
                case '\v': return "\\v";
                case ' ': return "Espaço";
            }
            return obj.ToString();
        }
        */
        public void SpecificPrint(string NameGameObject, object OBJ = null)
        { if (name == NameGameObject) print(OBJ); }

        public void SpecificPrints(string NameGameObject, object[] OBJ, bool MostraIndice = false, bool IndiceZero = true)
        { if (name == NameGameObject) prints(OBJ, MostraIndice, IndiceZero); }

        public static new void Destroy(UEObject OBJ) 
            => MonoBehaviour.Destroy(OBJ);

        public static new void Destroy(UEObject OBJ, float T)
            => MonoBehaviour.Destroy(OBJ, T);

        public void Destroy(UEObject[] OBJ)
            => gameObject.Destroy(OBJ);

        public void Destroy()
            => Destroy(gameObject);

        public void Destroy<T>() where T : UEObject
            => Destroy(gameObject.GetComponent<T>());

        public static GameObject BuscarGameObject(string Nome)
            => GameObject.Find(Nome);

        public static T BuscarGameObject<T>(string Nome) where T : UEObject {
            GameObject Res = BuscarGameObject(Nome);
            if(Res != null) return BuscarGameObject(Nome).GetComponent<T>();
            return (T)null;
        }

        public static T[] BuscarGameObjects<T>(bool Todos = false) where T : UEObject {
            GameObject[] OBJs = BuscarGameObjects(Todos);
            T[] Res = null;
            for (int I = 0; I < ArrayManipulation.ArrayLength(OBJs); I++) {
                T Temp = OBJs[I].GetComponent<T>();
                if (Temp != null)
                    ArrayManipulation.Add<T>(Temp, ref Res);
            }

            return Res;
        }

        public static GameObject[] BuscarGameObjects(bool Todos = false) {
            GameObject[] OBJs = FindObjectsOfType<GameObject>();
            GameObject[] Res = null;
            if (Todos) Res = OBJs;
            else
                for (int I = 0; I < ArrayManipulation.ArrayLength(OBJs); I++)
                    if (OBJs[I].GetParent() == null)
                        ArrayManipulation.Add<GameObject>(OBJs[I], ref Res);
            return Res;
        }

        public static bool GameObjectExiste(string nome)
            => BuscarGameObject(nome) != (GameObject)null;

        public static GameObject BuscarGameObjectRaiz(string nome) {
            GameObject[] OBJs = BuscarGameObjects();
            for (int I = 0; I < ArrayManipulation.ArrayLength(OBJs); I++)
                if (OBJs[I].transform.FilhoExiste(nome))
                    return OBJs[I];
            
            return null;
        }

        public static T BuscarGameObjectRaiz<T>(string nome) where T : UEObject {
            GameObject Res = BuscarGameObjectRaiz(nome);
            if (Res != null) return Res.GetComponent<T>();
            return (T)null;
        }

        public static RectTransform[] BuscarTodosOsCanvas(bool Todos = true) {
            Canvas[] canvas = BuscarGameObjects<Canvas>(Todos);
            RectTransform[] Res = null;
            for (int I = 0; I < ArrayManipulation.ArrayLength(canvas); I++)
                ArrayManipulation.Add<RectTransform>(canvas[I].GetComponent<RectTransform>(), ref Res);
            return Res;
        }
#if UNITY_UI_ACTIVATED
        public static GameObject BuscarEventSystem() {
            EventSystem[] OBJs = BuscarGameObjects<EventSystem>();
            if (ArrayManipulation.ArrayLength(OBJs) > 0)
                return OBJs[0].gameObject;
            return null;
        }
#endif
        public static GameObject CreatePrimitive(PrimitiveType type)
            => GameObject.CreatePrimitive(type);

        public static GameObject CreatePrimitive()
            => new GameObject("GameObject");

        public static KeyValuePair<int,T[]> QuantosGameObjectTem<T>() where T : UEObject {
            T[] Res = BuscarGameObjects<T>(true);
            return new KeyValuePair<int, T[]>(Res.Length, Res);
        }
    }
}
