namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPMuxImage
    {
        public WebPChunk* header_;      // Corresponds to WEBP_CHUNK_ANMF.
        public WebPChunk* alpha_;       // Corresponds to WEBP_CHUNK_ALPHA.
        public WebPChunk* img_;         // Corresponds to WEBP_CHUNK_IMAGE.
        public WebPChunk* unknown_;     // Corresponds to WEBP_CHUNK_UNKNOWN.
        public int width_;
        public int height_;
        public int has_alpha_;   // Through ALPH chunk or as part of VP8L.
        public int is_partial_;  // True if only some of the chunks are filled.
        public WebPMuxImage* next_;
    }
}
