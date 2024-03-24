using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace URS.ClientKit {

    public unsafe class DownloadService : IDisposable {
        private void* ptr;
        private bool threadAlive;

        private Thread thread;

        private List<DownloadOperation> operations = new();

        public bool Disposed { get; private set; } = false;

        public DownloadService() {
            ptr = DownloadServiceWrap.DownloadService_Start(false, 4, 4);
            threadAlive = true;
            thread = new Thread(OnUpdate);
            thread.IsBackground = true;
            thread.Name = "DownloadService";
            thread.Start();
        }

        public DownloadOperation AddDownload(string url, string path) {
            var config = DownloadConfig.Create(url, path);
            return AddDownload(config);
        }

        public DownloadOperation AddDownload(DownloadConfig config) {
            var operationPtr = DownloadServiceWrap.DownloadService_AddDownload((DownloadServicePtr*)ptr, config);
            var downloadOperation = new DownloadOperation(operationPtr);
            lock (operations) {
                operations.Add(downloadOperation);
            }
            return downloadOperation;
        }

        private void OnUpdate() {
            while (threadAlive) {
                lock (operations) {
                    for (int i = 0; i < operations.Count; i++) {
                        if (!operations[i].IsDone) {
                            operations[i].Update();
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void Dispose() {
            threadAlive = false;
            thread = null;
            Disposed = true;
            for (int i = 0; i < operations.Count; i++) {
                operations[i].Stop();
                operations[i].Dispose();
            }
            operations.Clear();
            if (ptr != null) {
                DownloadServiceWrap.DownloadService_Stop((DownloadServicePtr*)ptr);
                ptr = null;
            }
        }
    }

}