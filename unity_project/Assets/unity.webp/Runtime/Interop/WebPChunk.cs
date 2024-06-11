namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPChunk
    {
        [NativeTypeName("uint32_t")]
        public uint offset_;
        public int owner_;

        public WebPData data_;
        public WebPChunk* next_;
    }
}

