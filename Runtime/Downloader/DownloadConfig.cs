using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URS.ClientKit {

    public unsafe partial struct DownloadConfig {
        private static DownloadConfig Create(string url, string path, bool donwloadInMemory) {
            return new DownloadConfig() {
                url = url.ToUTF16FFI(),
                path = path.ToUTF16FFI(),
                chunk_download = false,
                chunk_size = 0,
                retry_times = 3,
                timeout = 10,
                version = 0,
                download_in_memory = donwloadInMemory,
            };
        }

        public static DownloadConfig Create(string url, string path) {
            return Create(url, path, false);
        }

        public static DownloadConfig Create(string url) {
            return Create(url, string.Empty, true);
        }
    }

}