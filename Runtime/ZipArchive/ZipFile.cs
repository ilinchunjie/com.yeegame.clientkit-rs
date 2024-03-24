using System.IO;
using Unity.Collections.LowLevel.Unsafe;

namespace URS.ClientKit {

    public unsafe class ZipFile : Stream {
        private ZipFilePtr* ptr;

        internal ZipFile(ZipFilePtr* ptr) {
            this.ptr = ptr;
        }

        public override void Flush() {
            throw new System.NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            var btyeBuffer = ZipFileWrap.ZipFile_Read(ptr, count);
            var length = btyeBuffer->length;
            if (length == 0) {
                return 0;
            }
            UnsafeUtility.MemCpy(UnsafeUtility.AddressOf(ref buffer[offset]), btyeBuffer->ptr, length);
            ByteBufferWrap.free_u8_buffer(btyeBuffer);
            return length;
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin) {
            return ZipFileWrap.ZipFile_Seek(ptr, offset, (SeekOrigin)origin);
        }

        public override void SetLength(long value) {
            throw new System.NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new System.NotSupportedException();
        }

        public override void Close() {
            base.Close();
            if (ptr != null) {
                ZipFileWrap.ZipFile_Dispose(ptr);
                ptr = null;
            }
        }

        public override bool CanRead {
            get {
                return true;
            }
        }
        public override bool CanSeek {
            get {
                return true;
            }
        }
        public override bool CanWrite {
            get {
                return false;
            }
        }

        public override long Length {
            get {
                return ZipFileWrap.ZipFile_Length(ptr);
            }
        }

        public override long Position {
            get {
                return ZipFileWrap.ZipFile_Position(ptr, 0, SeekOrigin.Current);
            }
            set {
                Seek(value, System.IO.SeekOrigin.Begin);
            }
        }
    }

}