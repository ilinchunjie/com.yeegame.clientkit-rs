using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace URS.ClientKit {

    public unsafe class ZipArchive : IDisposable {
        public static ZipArchive Mount(string path) {
            var ptr = ZipArchiveWrap.ZipArchive_New(path.ToUTF16FFI());
            return new ZipArchive(ptr);
        }

        private ZipArchivePtr* ptr;

        private ZipArchive(ZipArchivePtr* ptr) {
            this.ptr = ptr;
        }

        public bool FileExist(string path) {
            return ZipArchiveWrap.ZipArchive_FileExist(ptr, path.ToUTF16FFI());
        }
        
        public ZipFile Open(string path) {
            var filePtr = ZipArchiveWrap.ZipArchive_Open(ptr, path.ToUTF16FFI());
            return new ZipFile(filePtr);
        }

        public byte[] ReadAllBytes(string path) {
            var byteBuffer = ZipArchiveWrap.ZipArchive_ReadAllBytes(ptr, path.ToUTF16FFI());
            var length = byteBuffer->length;
            var bytes = new byte[length];
            UnsafeUtility.MemCpy(UnsafeUtility.AddressOf(ref bytes[0]), byteBuffer->ptr, byteBuffer->length);
            ByteBufferWrap.free_u8_buffer(byteBuffer);
            return bytes;
        }

        public void Dispose() {
            if (ptr != null) {
                ZipArchiveWrap.ZipArchive_Dispose(ptr);
                ptr = null;
            }
        }
    }

}