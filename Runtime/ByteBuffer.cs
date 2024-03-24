using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace URS.ClientKit {

    internal static unsafe class ByteBufferExtension {
        internal static byte* UnityAlloc(ByteBuffer* byteBuffer, Allocator allocator) {
            var bytes = (byte*)UnsafeUtility.Malloc(byteBuffer->length, UnsafeUtility.AlignOf<byte>(), allocator);
            ByteBufferWrap.free_u8_buffer(byteBuffer);
            return bytes;
        }
    }

}