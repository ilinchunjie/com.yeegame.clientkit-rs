using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace URS.ClientKit {

    public enum DownloadStatus {
        None,
        Pending,
        Head,
        Download,
        DownloadPost,
        FileVerify,
        Complete,
        Failed,
        Stop,
    }

    public unsafe class DownloadOperation : IDisposable {
        private void* ptr;
        private bool isDone;
        private bool isError;
        private string error = string.Empty;
        private ulong downloadedSize;
        private DownloadStatus downloadStatus;

        private DownloadOperationAwaiter awaiter;

        public bool Disposed {
            get {
                return ptr == null;
            }
        }

        public byte[] Bytes {
            get {
                if (Disposed) {
                    return null;
                }
                if (DownloadStatus != DownloadStatus.Complete) {
                    return null;
                }
                var byteBuffer = DownloadOperationWrap.DownloadOperation_Bytes((DownloadOperationPtr*)ptr);
                var length = byteBuffer->length;
                var bytes = new byte[length];
                UnsafeUtility.MemCpy(UnsafeUtility.AddressOf(ref bytes[0]), byteBuffer->ptr, byteBuffer->length);
                ByteBufferWrap.free_u8_buffer(byteBuffer);
                return bytes;
            }
        }

        public bool IsDone {
            get {
                return isDone;
            }
        }

        public ulong DownloadedSize {
            get {
                return downloadedSize;
            }
        }

        public DownloadStatus DownloadStatus {
            get {
                return downloadStatus;
            }
        }

        public DownloadOperation(void* ptr) {
            this.ptr = ptr;
        }

        public DownloadOperationAwaiter GetAwaiter() {
            if (awaiter == null) {
                awaiter = new DownloadOperationAwaiter();
                awaiter.SetStatus(IsDone, DownloadStatus == DownloadStatus.Complete);
            }
            return awaiter;
        }

        internal void Update() {
            if (Disposed) {
                return;
            }
            downloadStatus = (DownloadStatus)DownloadOperationWrap.DownloadOperation_GetDownloadStatus((DownloadOperationPtr*)ptr);
            downloadedSize = DownloadOperationWrap.DownloadOperation_GetDownloadedSize((DownloadOperationPtr*)ptr);
            isDone = DownloadOperationWrap.DownloadOperation_IsDone((DownloadOperationPtr*)ptr);
            awaiter?.SetStatus(isDone, downloadStatus == DownloadStatus.Complete);
        }

        public void Stop() {
            if (Disposed) {
                return;
            }
            DownloadOperationWrap.DownloadOperation_Stop((DownloadOperationPtr*)ptr);
        }

        public void Dispose() {
            if (ptr != null) {
                DownloadOperationWrap.DownloadOperation_Dispose((DownloadOperationPtr*)ptr);
                ptr = null;
            }
        }
    }

    public class DownloadOperationAwaiter : INotifyCompletion {
        private Action continuation;
        public bool IsCompleted { get; private set; }

        public bool Success { get; private set; }

        internal DownloadOperationAwaiter() {
        }

        public void OnCompleted(Action continuation) {
            if (IsCompleted) {
                continuation.Invoke();
                return;
            }
            this.continuation += continuation;
        }

        public bool GetResult() {
            return Success;
        }

        internal void SetStatus(bool completed, bool success) {
            IsCompleted = completed;
            Success = success;
            if (IsCompleted) {
                this.continuation?.Invoke();
                this.continuation = null;
            }
        }
    }

}