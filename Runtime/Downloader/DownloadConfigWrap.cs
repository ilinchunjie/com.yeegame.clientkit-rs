// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;


namespace URS.ClientKit
{
    public static unsafe partial class DownloadConfigWrap
    {
#if UNITY_IOS && !UNITY_EDITOR
        const string __DllName = "__Internal";
#else
        const string __DllName = "clientkit";
#endif
        



        [DllImport(__DllName, EntryPoint = "DownloadConfig_Extern", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void DownloadConfig_Extern(DownloadConfig config);


    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DownloadConfig
    {
        public UTF16String url;
        public UTF16String path;
        public byte retry_times;
        [MarshalAs(UnmanagedType.U1)] public bool chunk_download;
        [MarshalAs(UnmanagedType.U1)] public bool download_in_memory;
        public long version;
        public ulong chunk_size;
        public ulong timeout;
    }



}
    