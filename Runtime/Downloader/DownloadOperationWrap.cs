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
    internal static unsafe partial class DownloadOperationWrap
    {
#if UNITY_IOS && !UNITY_EDITOR
        const string __DllName = "__Internal";
#else
        const string __DllName = "clientkit";
#endif
        



        [DllImport(__DllName, EntryPoint = "DownloadOperation_GetDownloadStatus", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte DownloadOperation_GetDownloadStatus(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_IsDone", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool DownloadOperation_IsDone(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_IsError", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool DownloadOperation_IsError(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_Error", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* DownloadOperation_Error(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_GetDownloadedSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ulong DownloadOperation_GetDownloadedSize(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_GetDownloadProgress", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double DownloadOperation_GetDownloadProgress(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_Bytes", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ByteBuffer* DownloadOperation_Bytes(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_Stop", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void DownloadOperation_Stop(DownloadOperationPtr* ptr);

        [DllImport(__DllName, EntryPoint = "DownloadOperation_Dispose", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void DownloadOperation_Dispose(DownloadOperationPtr* ptr);


    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct DownloadOperationPtr
    {
    }



}
    