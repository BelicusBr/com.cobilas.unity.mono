using System;
using UnityEngine;
using UnityEditor;
using Cobilas.Unity.Mono;

namespace Cobilas.Unity.Editor.Draw {
    [CustomPropertyDrawer(typeof(RotuloAttribute))]
    public class RotuloAttributeDraw : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            RotuloAttribute rotulo = attribute as RotuloAttribute;

            object V = property.GetValue();
            string Valor = "Nada!!!";
            if (V != null)
                Valor = FloatFormat(V, rotulo.FloatingPointFormat);
            EditorGUI.LabelField(position, label, new GUIContent($"[{Valor}]"));
        }

        private string FloatFormat(object value, string format) {
            if (string.IsNullOrEmpty(format)) return value.ToString();
            string Res = null;
            Type T = value.GetType();
            if (T == typeof(float)) Res = ((float)value).ToString(format);
            else if (T == typeof(double)) Res = ((double)value).ToString(format);
            else if (T == typeof(decimal)) Res = ((decimal)value).ToString(format);
            else if (T == typeof(Color)) Res = ((Color)value).ToString(format);
            else if (T == typeof(Vector2)) Res = ((Vector2)value).ToString(format);
            else if (T == typeof(Vector3)) Res = ((Vector3)value).ToString(format);
            else if (T == typeof(Vector4)) Res = ((Vector4)value).ToString(format);
            else if (T == typeof(Quaternion)) Res = ((Quaternion)value).ToString(format);
            return Res;
        }
    }
}
