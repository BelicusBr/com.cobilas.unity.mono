using System;
using UnityEngine;

namespace Cobilas.Unity.Mono {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RotuloAttribute : PropertyAttribute {
        public string FloatingPointFormat;
        protected Guid instanceGUID;
        public RotuloAttribute() {
            FloatingPointFormat = null;
            this.instanceGUID = Guid.NewGuid();
        }

        public RotuloAttribute(string format) {
            FloatingPointFormat = format;
            this.instanceGUID = Guid.NewGuid();
        }

        public override object TypeId => (object)instanceGUID;
    }
}
