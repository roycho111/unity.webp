namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPMux
    {
        WebPMuxImage* images_;
        WebPChunk* iccp_;
        WebPChunk* exif_;
        WebPChunk* xmp_;
        WebPChunk* anim_;
        WebPChunk* vp8x_;

        WebPChunk* unknown_;
        int canvas_width_;
        int canvas_height_;
    }
}
