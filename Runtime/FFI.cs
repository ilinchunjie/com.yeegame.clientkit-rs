using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URS.ClientKit {

    public static class FFIExtension {
        // ReSharper disable once InconsistentNaming
        public static unsafe UTF16String ToUTF16FFI(this string value) {
            var result = new UTF16String();
            if (string.IsNullOrEmpty(value)) {
                fixed (char* c = string.Empty) {
                    result.ptr = (ushort*)c;
                    result.len = (uint)0;
                }
                return result;
            }
            fixed (char* c = value) {
                result.ptr = (ushort*)c;
                result.len = (uint)value.Length;
            }
            return result;
        }
    }

}