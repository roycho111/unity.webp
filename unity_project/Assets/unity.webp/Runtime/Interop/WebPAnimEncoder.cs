using System;

namespace unity.libwebp.Interop
{
    public partial struct WebPAnimEncoder
    {
    }

    public unsafe partial struct WebPAnimEncoder
    {
        [NativeTypeName("const int")]
        public int canvas_width_;                  // Canvas width.
        [NativeTypeName("const int")]
        public int canvas_height_;                 // Canvas height.
        [NativeTypeName("const WebPAnimEncoderOptions")]
        public WebPAnimEncoderOptions options_;    // Global encoding options.

        public FrameRectangle prev_rect_;          // Previous WebP frame rectangle.
        public WebPConfig last_config_;            // Cached in case a re-encode is needed.
        public WebPConfig last_config_reversed_;   // If 'last_config_' uses lossless, then
                                                   // this config uses lossy and vice versa;
                                                   // only valid if 'options_.allow_mixed'
                                                   // is true.

        public WebPPicture* curr_canvas_;          // Only pointer; we don't own memory.

        // Canvas buffers.
        public WebPPicture curr_canvas_copy_;       // Possibly modified current canvas.
        public int curr_canvas_copy_modified_;      // True if pixels in 'curr_canvas_copy_'
                                                    // differ from those in 'curr_canvas_'.

        public WebPPicture prev_canvas_;            // Previous canvas.
        public WebPPicture prev_canvas_disposed_;   // Previous canvas disposed to background.

        // Encoded data.
        public EncodedFrame* encoded_frames_;      // Array of encoded frames.
        [NativeTypeName("size_t")]
        public UIntPtr size_;             // Number of allocated frames.
        [NativeTypeName("size_t")]
        public UIntPtr start_;            // Frame start index.
        [NativeTypeName("size_t")]
        public UIntPtr count_;            // Number of valid frames.
        [NativeTypeName("size_t")]
        public UIntPtr flush_count_;      // If >0, 'flush_count' frames starting from
                                          // 'start' are ready to be added to mux.

        // key-frame related.
        [NativeTypeName("int64_t")]
        public ulong best_delta_;       // min(canvas size - frame size) over the frames.
                                        // Can be negative in certain cases due to
                                        // transparent pixels in a frame.
        public int keyframe_;            // Index of selected key-frame relative to 'start_'.
        public int count_since_key_frame_;     // Frames seen since the last key-frame.

        public int first_timestamp_;           // Timestamp of the first frame.
        public int prev_timestamp_;            // Timestamp of the last added frame.
        public int prev_candidate_undecided_;  // True if it's not yet decided if previous
                                        // frame would be a sub-frame or a key-frame.

        // Misc.
        public int is_first_frame_;     // True if first frame is yet to be added/being added.
        public int got_null_frame_;     // True if WebPAnimEncoderAdd() has already been called
                                        // with a NULL frame.

        [NativeTypeName("size_t")]
        public UIntPtr in_frame_count_;     // Number of input frames processed so far.
        [NativeTypeName("size_t")]
        public UIntPtr out_frame_count_;    // Number of frames added to mux so far. This may be
                                            // different from 'in_frame_count_' due to merging.

        public WebPMux* mux_;        // Muxer to assemble the WebP bitstream.
        public fixed char error_str_[100];  // Error string. Empty if no error.
    }
}
