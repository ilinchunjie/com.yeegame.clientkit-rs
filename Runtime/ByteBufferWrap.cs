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
    internal static unsafe partial class ByteBufferWrap
    {
#if UNITY_IOS && !UNITY_EDITOR
        const string __DllName = "__Internal";
#else
        const string __DllName = "clientkit";
#endif
        



        [DllImport(__DllName, EntryPoint = "free_u8_buffer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void free_u8_buffer(ByteBuffer* buffer);


    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct ByteBuffer
    {
        public byte* ptr;
        public int length;
        public int capacity;
    }



}
    