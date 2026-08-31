#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenCVForUnity.Editor
{
    public class OpenCVForUnityExampleAssetsDownloaderWindow : EditorWindow
    {
        // Public Fields
        public static bool IsDownloading { get; private set; }

        // Private Fields
        private bool _isDebugMode = false;
        private static Dictionary<string, string>[] _files = {
            //ColorizationExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ColorizationExample" }, { "url", "https://github.com/richzhang/colorization/raw/caffe/demo/imgs/ansel_adams3.jpg" } , { "filename", "ansel_adams3.jpg" }, { "sha", "ca4af64f5cd4adc180d167f09f617319871d4608" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ColorizationExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/releases/download/dnn%2FColorizationExample/colorization_release_v2.caffemodel" } , { "filename", "colorization_release_v2.caffemodel" }, { "sha", "21e61293a3fa6747308171c11b6dd18a68a26e7f" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ColorizationExample" }, { "url", "https://github.com/richzhang/colorization/raw/caffe/models/colorization_deploy_v2.prototxt" } , { "filename", "colorization_deploy_v2.prototxt" }, { "sha", "f528334e386a69cbaaf237a7611d833bef8e5219" } },
            //ObjectTrackingDaSiamRPNExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectTrackingDaSiamRPNExample" }, { "url", "https://www.dropbox.com/s/rr1lk9355vzolqv/dasiamrpn_model.onnx?dl=1" } , { "filename", "dasiamrpn_model.onnx" }, { "sha", "91b774fce7df4c0e4918469f0f482d9a27d0e2d4" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectTrackingDaSiamRPNExample" }, { "url", "https://www.dropbox.com/s/999cqx5zrfi7w4p/dasiamrpn_kernel_r1.onnx?dl=1" } , { "filename", "dasiamrpn_kernel_r1.onnx" }, { "sha", "bb64620a54348657133eb28be2d3a2a8c76b84b3" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectTrackingDaSiamRPNExample" }, { "url", "https://www.dropbox.com/s/qvmtszx5h339a0w/dasiamrpn_kernel_cls1.onnx?dl=1" } , { "filename", "dasiamrpn_kernel_cls1.onnx" }, { "sha", "e9ccd270ce8059bdf7ed0d1845c03ef4a951ee0f" } },
            //FastNeuralStyleTransferExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "FastNeuralStyleTransferExample" }, { "url", "https://cs.stanford.edu/people/jcjohns/fast-neural-style/models/instance_norm/mosaic.t7" } , { "filename", "mosaic.t7" }, { "sha", "f4d3e2a5e3060b3c39a9648ad009de3e09cd0001" } },
            //FaceDetectionYuNetExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "FaceDetectionYuNetExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/4563a91ba98172b14d7af8bce621b6d1ae7ae0c6/models/face_detection_yunet/face_detection_yunet_2022mar.onnx" }, { "filename", "face_detection_yunet_2022mar.onnx" }, { "sha", "dfe691ae0c8e38d39d1a437e3f7e5fda7b256bdd" } },
            //FaceDetectionYuNetV2Example
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "FaceDetectionYuNetV2Example" }, { "url", "https://github.com/opencv/opencv_zoo/raw/f872270fbb034ac326ee5a8d6343299bde765bc4/models/face_detection_yunet/face_detection_yunet_2023mar.onnx" }, { "filename", "face_detection_yunet_2023mar.onnx" }, { "sha", "acbe4b5976ade60c4b866a30d0720d71589c8bbc" } },
            //FacialExpressionRecognitionExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "FacialExpressionRecognitionExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/f872270fbb034ac326ee5a8d6343299bde765bc4/models/face_detection_yunet/face_detection_yunet_2023mar.onnx" }, { "filename", "face_detection_yunet_2023mar.onnx" }, { "sha", "acbe4b5976ade60c4b866a30d0720d71589c8bbc" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "FacialExpressionRecognitionExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/c3a51f7049977ae29fb4b4781645fc79925b7dde/models/facial_expression_recognition/facial_expression_recognition_mobilefacenet_2022july.onnx" } , { "filename", "facial_expression_recognition_mobilefacenet_2022july.onnx" }, { "sha", "c2ded863504333117c57fddf6941e8c860134183" } },
            //PoseEstimationMediaPipeExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "PoseEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/person_detection_mediapipe/person_detection_mediapipe_2023mar.onnx" } , { "filename", "person_detection_mediapipe_2023mar.onnx" }, { "sha", "c99fdade615dbda34c4b51462947e21b2797864f" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "PoseEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/pose_estimation_mediapipe/pose_estimation_mediapipe_2023mar.onnx" } , { "filename", "pose_estimation_mediapipe_2023mar.onnx" }, { "sha", "9ecbfab8dec975ba02d8436a65cd69755238be20" } },
            //HandPoseEstimationMediaPipeExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HandPoseEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/6c68bc48c6f96042b29b3425174e431ccac38376/models/palm_detection_mediapipe/palm_detection_mediapipe_2023feb.onnx" } , { "filename", "palm_detection_mediapipe_2023feb.onnx" }, { "sha", "b9e6df1d4f93ee1b0b4f5c99a2f88716ccd7ca9a" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HandPoseEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/05a07912a619f3dd491ba22ca489245c7847c9ff/models/handpose_estimation_mediapipe/handpose_estimation_mediapipe_2023feb.onnx" } , { "filename", "handpose_estimation_mediapipe_2023feb.onnx" }, { "sha", "48cfa3de98f30986ae2be6ed55e80d46e06713ab" } },
            //HumanPoseStreamEstimationMediaPipeExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HumanPoseStreamEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/person_detection_mediapipe/person_detection_mediapipe_2023mar.onnx" } , { "filename", "person_detection_mediapipe_2023mar.onnx" }, { "sha", "c99fdade615dbda34c4b51462947e21b2797864f" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HumanPoseStreamEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/0d619617a8e9a389150d8c76e417451a19468150/models/pose_estimation_mediapipe/pose_estimation_mediapipe_2023mar.onnx" } , { "filename", "pose_estimation_mediapipe_2023mar.onnx" }, { "sha", "9ecbfab8dec975ba02d8436a65cd69755238be20" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HumanPoseStreamEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/6c68bc48c6f96042b29b3425174e431ccac38376/models/palm_detection_mediapipe/palm_detection_mediapipe_2023feb.onnx" } , { "filename", "palm_detection_mediapipe_2023feb.onnx" }, { "sha", "b9e6df1d4f93ee1b0b4f5c99a2f88716ccd7ca9a" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HumanPoseStreamEstimationMediaPipeExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/05a07912a619f3dd491ba22ca489245c7847c9ff/models/handpose_estimation_mediapipe/handpose_estimation_mediapipe_2023feb.onnx" } , { "filename", "handpose_estimation_mediapipe_2023feb.onnx" }, { "sha", "48cfa3de98f30986ae2be6ed55e80d46e06713ab" } },
            //HumanSegmentationPPHumanSegExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "HumanSegmentationPPHumanSegExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/2027dd2f5a8a5746b5d4964900a0465afc6d3a53/models/human_segmentation_pphumanseg/human_segmentation_pphumanseg_2023mar.onnx" } , { "filename", "human_segmentation_pphumanseg_2023mar.onnx" }, { "sha", "f0fec695ab7b716eeab4c58b125e98fc3826bb72" } },
            //ImageClassificationMobilenetExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ImageClassificationMobilenetExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/6c59fd8aaedf1728138e3c982f3351bf9ee3013a/models/image_classification_mobilenet/image_classification_mobilenetv2_2022apr.onnx" } , { "filename", "image_classification_mobilenetv2_2022apr.onnx" }, { "sha", "128ea9978b4b46a62a1918040137ee9b0a030c91" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ImageClassificationMobilenetExample" }, { "url", "https://raw.githubusercontent.com/opencv/opencv_zoo/326e15b31a70812eb6d616406d6e6a17ceaddb6f/models/image_classification_ppresnet/imagenet_labels.txt" } , { "filename", "imagenet_labels.txt" }, { "sha", "18673fc90a50d60938a931747ed6d0bfcf40c894" } },
            //ImageClassificationPPResnetExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ImageClassificationPPResnetExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/c8812a7668ea3f285797c0c450d0912add9248f2/models/image_classification_ppresnet/image_classification_ppresnet50_2022jan.onnx" } , { "filename", "image_classification_ppresnet50_2022jan.onnx" }, { "sha", "00fb57af9def8fd00e4f01635d0c4c901268aa02" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ImageClassificationPPResnetExample" }, { "url", "https://raw.githubusercontent.com/opencv/opencv_zoo/326e15b31a70812eb6d616406d6e6a17ceaddb6f/models/image_classification_ppresnet/imagenet_labels.txt" } , { "filename", "imagenet_labels.txt" }, { "sha", "18673fc90a50d60938a931747ed6d0bfcf40c894" } },
            //ObjectDetectionDAMOYOLOExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionDAMOYOLOExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/raw/506b6532c8fcde592147ba60577f1d939dd575b8/dnn/ObjectDetectionDAMOYOLOExample/004545.jpg" } , { "filename", "004545.jpg" }, { "sha", "2b0c65f59a9f9071f1e7de452f0c2004e8d55b7b" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionDAMOYOLOExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/raw/38bd3c1334b2d2e6b79b44d2ccf8923d15ab5476/dnn/ObjectDetectionDAMOYOLOExample/damoyolo_tinynasL18_Ns.onnx" } , { "filename", "damoyolo_tinynasL18_Ns.onnx" }, { "sha", "ab37fb8fd77f3fe63d7ccc367a4d001bd75d56aa" } },
            //ObjectDetectionYOLOXExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionYOLOXExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/raw/506b6532c8fcde592147ba60577f1d939dd575b8/dnn/ObjectDetectionDAMOYOLOExample/004545.jpg" } , { "filename", "004545.jpg" }, { "sha", "2b0c65f59a9f9071f1e7de452f0c2004e8d55b7b" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionYOLOXExample" }, { "url", "https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_tiny.onnx" } , { "filename", "yolox_tiny.onnx" }, { "sha", "45985579a307aae54c7b54ca257bc0b48606deac" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionYOLOXExample" }, { "url", "https://github.com/AlexeyAB/darknet/raw/0faed3e60e52f742bbef43b83f6be51dd30f373e/data/coco.names" } , { "filename", "coco.names" }, { "sha", "b769c7d769385f7640be484dd9c7537b6fb2f35e" } },
            //ObjectDetectionNanoDetPlusExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionNanoDetPlusExample" }, { "url", "https://github.com/AlexeyAB/darknet/raw/0faed3e60e52f742bbef43b83f6be51dd30f373e/data/person.jpg" } , { "filename", "person.jpg" }, { "sha", "19281b65c5bd43381dfe04e637e78d0cf0b05cbe" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionNanoDetPlusExample" }, { "url", "https://github.com/RangiLyu/nanodet/releases/download/v1.0.0-alpha-1/nanodet-plus-m_416.onnx" } , { "filename", "nanodet-plus-m_416.onnx" }, { "sha", "79ed0edaab7ea00fe76662aa9d954f0fa5ff5d28" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "ObjectDetectionNanoDetPlusExample" }, { "url", "https://github.com/AlexeyAB/darknet/raw/0faed3e60e52f742bbef43b83f6be51dd30f373e/data/coco.names" } , { "filename", "coco.names" }, { "sha", "b769c7d769385f7640be484dd9c7537b6fb2f35e" } },
            //TextRecognitionCRNNExample
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "TextRecognitionCRNNExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/6a66e0d6e47a693e6d0dd01bbb18e920f3fbae75/models/text_detection_db/text_detection_DB_IC15_resnet18_2021sep.onnx" } , { "filename", "text_detection_DB_IC15_resnet18_2021sep.onnx" }, { "sha", "19543ce09b2efd35f49705c235cc46d0e22df30b" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "TextRecognitionCRNNExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/8a42017a12fe9ed80279737c0b903307371b0e3d/models/text_recognition_crnn/text_recognition_CRNN_EN_2021sep.onnx" } , { "filename", "text_recognition_CRNN_EN_2021sep.onnx" }, { "sha", "dc8c70a52c6880f11859bf074bcd294a45860821" } },
            new Dictionary<string, string>() { { "module", "dnn" }, { "name", "TextRecognitionCRNNExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/8a42017a12fe9ed80279737c0b903307371b0e3d/models/text_recognition_crnn/charset_36_EN.txt" } , { "filename", "charset_36_EN.txt" }, { "sha", "e1562f54307e089f23ce7586d55c994666ed44ac" } },

            //FaceMarkExample
            new Dictionary<string, string>() { { "module", "face" }, { "name", "FaceMarkExample" }, { "url", "https://raw.githubusercontent.com/kurnianggoro/GSOC2017/master/data/lbfmodel.yaml" } , { "filename", "lbfmodel.yaml" }, { "sha", "2bcd8ce6cff58fa3ad74386b7b3f77b510198d27" }  },

            //FaceDetectorYNWebCamExample
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "FaceDetectorYNWebCamExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/f872270fbb034ac326ee5a8d6343299bde765bc4/models/face_detection_yunet/face_detection_yunet_2023mar.onnx" } , { "filename", "face_detection_yunet_2023mar.onnx" }, { "sha", "acbe4b5976ade60c4b866a30d0720d71589c8bbc" } },
            //FaceRecognizerSFExample
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "FaceRecognizerSFExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/bc04b1b1c2199fe041723122aa0b4b71032c398c/models/face_recognition_sface/face_recognition_sface_2021dec.onnx" } , { "filename", "face_recognition_sface_2021dec.onnx" }, { "sha", "316ca25772af10f61e356f81f0ec68caf6909a51" } },
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "FaceRecognizerSFExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/f872270fbb034ac326ee5a8d6343299bde765bc4/models/face_detection_yunet/face_detection_yunet_2023mar.onnx" } , { "filename", "face_detection_yunet_2023mar.onnx" }, { "sha", "acbe4b5976ade60c4b866a30d0720d71589c8bbc" } },
            //FaceIdentificationEstimatorExample
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "FaceIdentificationEstimatorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/f872270fbb034ac326ee5a8d6343299bde765bc4/models/face_detection_yunet/face_detection_yunet_2023mar.onnx" } , { "filename", "face_detection_yunet_2023mar.onnx" }, { "sha", "acbe4b5976ade60c4b866a30d0720d71589c8bbc" } },
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "FaceIdentificationEstimatorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/bc04b1b1c2199fe041723122aa0b4b71032c398c/models/face_recognition_sface/face_recognition_sface_2021dec.onnx" } , { "filename", "face_recognition_sface_2021dec.onnx" }, { "sha", "316ca25772af10f61e356f81f0ec68caf6909a51" } },

            //ArUcoCameraCalibrationExample
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "ArUcoCameraCalibrationExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/raw/c3758c7502a57419b68d3ac05f302fb30b032e00/objdetect/ArUcoCameraCalibrationExample/ArUcoCameraCalibrationExample_TestVideo_640_640_30fps.mp4" } , { "filename", "ArUcoCameraCalibrationExample_TestVideo_640_640_30fps.mp4" }, { "sha", "ccebe5a0abb247d787e00342e743304272042205" } },
            //ArUcoExample
            new Dictionary<string, string>() { { "module", "objdetect" }, { "name", "ArUcoExample" }, { "url", "https://github.com/EnoxSoftware/OpenCVForUnityExampleAssets/raw/c3758c7502a57419b68d3ac05f302fb30b032e00/objdetect/ArUcoExample/ArUcoExample_TestVideo_640_640_30fps.mp4" }, { "filename", "ArUcoExample_TestVideo_640_640_30fps.mp4" }, { "sha", "97f6a35b656a8f9810cdeceb69f5301711094107" } },

            //TrackingExample
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/80f7c6aa030a87b3f9e8ab7d84f62f13d308c10f/models/object_tracking_vittrack/object_tracking_vittrack_2023sep.onnx" } , { "filename", "object_tracking_vittrack_2023sep.onnx" }, { "sha", "50008bb4f6a27b1aa940ad886b1bd1936ac4ed3e" } },
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://www.dropbox.com/s/rr1lk9355vzolqv/dasiamrpn_model.onnx?dl=1" } , { "filename", "dasiamrpn_model.onnx" }, { "sha", "91b774fce7df4c0e4918469f0f482d9a27d0e2d4" } },
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://www.dropbox.com/s/999cqx5zrfi7w4p/dasiamrpn_kernel_r1.onnx?dl=1" } , { "filename", "dasiamrpn_kernel_r1.onnx" }, { "sha", "bb64620a54348657133eb28be2d3a2a8c76b84b3" } },
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://www.dropbox.com/s/qvmtszx5h339a0w/dasiamrpn_kernel_cls1.onnx?dl=1" } , { "filename", "dasiamrpn_kernel_cls1.onnx" }, { "sha", "e9ccd270ce8059bdf7ed0d1845c03ef4a951ee0f" } },
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://github.com/HonglinChu/SiamTrackers/raw/c2ff8479624b12ef2dcd830c47f2495a2c4852d4/NanoTrack/models/nanotrackv2/nanotrack_backbone_sim.onnx" } , { "filename", "nanotrack_backbone_sim.onnx" }, { "sha", "6e773a364457b78574f9f63a23b0659ee8646f8f" } },
            new Dictionary<string, string>() { { "module", "tracking" }, { "name", "TrackingExample" }, { "url", "https://github.com/HonglinChu/SiamTrackers/raw/c2ff8479624b12ef2dcd830c47f2495a2c4852d4/NanoTrack/models/nanotrackv2/nanotrack_head_sim.onnx" } , { "filename", "nanotrack_head_sim.onnx" }, { "sha", "39f168489671700cf739e402dfc67d41ce648aef" } },

            //WeChatQRCodeDetectorExample
            new Dictionary<string, string>() { { "module", "wechat_qrcode" }, { "name", "WeChatQRCodeDetectorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/661ca25ce59ccf7505cc79bf788bfb4a888ff314/models/qrcode_wechatqrcode/detect_2021nov.prototxt" } , { "filename", "detect_2021nov.prototxt" }, { "sha", "a6936962139282d300ebbf15a54c2aa94b144bb7" } },
            new Dictionary<string, string>() { { "module", "wechat_qrcode" }, { "name", "WeChatQRCodeDetectorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/661ca25ce59ccf7505cc79bf788bfb4a888ff314/models/qrcode_wechatqrcode/detect_2021nov.caffemodel" } , { "filename", "detect_2021nov.caffemodel" }, { "sha", "d587623a055cbd58a648de62a8c703c7abb05f6d" } },
            new Dictionary<string, string>() { { "module", "wechat_qrcode" }, { "name", "WeChatQRCodeDetectorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/661ca25ce59ccf7505cc79bf788bfb4a888ff314/models/qrcode_wechatqrcode/sr_2021nov.prototxt" } , { "filename", "sr_2021nov.prototxt" }, { "sha", "39e1f1031c842766f1cc126615fea8e8256facd2" } },
            new Dictionary<string, string>() { { "module", "wechat_qrcode" }, { "name", "WeChatQRCodeDetectorExample" }, { "url", "https://github.com/opencv/opencv_zoo/raw/661ca25ce59ccf7505cc79bf788bfb4a888ff314/models/qrcode_wechatqrcode/sr_2021nov.caffemodel" } , { "filename", "sr_2021nov.caffemodel" }, { "sha", "2b181b55d1d7af718eaca6cabdeb741217b64c73" } },

        };
        private Vector2 _scrollPosition = Vector2.zero;
        private string[] _exampleNames = null;
        private UnityWebRequest _unityWebRequest = null;
        private List<Dictionary<string, string>> _downloadingFiles = null;
        private int _downloadingFileIndex = 0;
        private int _downloadSuccessFileCount = 0;
        private int _downloadFailureFileCount = 0;
        private int _skippedFileCount = 0;
        private string _streamingAssetsFolderPath = null;
        private int _currentProgressID;
        private bool _isProcessingFiles = false;
        private string _processingStatus = "";
        private float _processingProgress = 0f;
        private List<string> _processingFileList = new List<string>();

        // Unity Lifecycle Methods
        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_streamingAssetsFolderPath == null) _streamingAssetsFolderPath = GetStreamingAssetsFolderPath();
            if (_exampleNames == null) _exampleNames = GetExampleNames(_files);

            if (_isProcessingFiles)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.HelpBox(_processingStatus, MessageType.Info);

                // Display the list of files being processed
                if (_processingFileList.Count > 0)
                {
                    EditorGUI.indentLevel++;
                    foreach (var file in _processingFileList)
                    {
                        EditorGUILayout.LabelField(file);
                    }
                    EditorGUI.indentLevel--;
                }

                Rect rect = EditorGUILayout.GetControlRect(false, 20);
                EditorGUI.ProgressBar(rect, _processingProgress, "Processing files...");
            }

            using (new EditorGUI.DisabledScope(IsDownloading || _isProcessingFiles))
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.HelpBox(
                    "Automatically downloads the files needed to run Example Scenes. After downloading, please run \"Move StreamingAssets Folder\".",
                    MessageType.Info
                );
                EditorGUILayout.Space(10);

                GUILayout.BeginVertical("box");
                {
                    if (GUILayout.Button("All Download"))
                    {
                        StartDownloadFilesAsync(new List<Dictionary<string, string>>(_files));
                    }
                }
                GUILayout.EndVertical();

                EditorGUILayout.Space(10);

                for (int i = 0; i < _exampleNames.Length; i++)
                {
                    if (!_exampleNames[i].EndsWith("Example"))
                    {
                        if (i != 0) GUILayout.EndVertical();
                        GUILayout.BeginVertical("box");

                        EditorGUILayout.LabelField(_exampleNames[i]);
                        continue;
                    }

                    if (GUILayout.Button(_exampleNames[i]))
                    {
                        StartDownloadFilesAsync(ExtractFiles(_files, _exampleNames[i]));
                    }

                    if (i == _exampleNames.Length - 1) GUILayout.EndVertical();
                }
            }

            if (IsDownloading)
            {
                ShowProgressBar(_downloadingFiles[_downloadingFileIndex]);
            }

            EditorGUILayout.EndScrollView();
        }

        // Public Methods
        [MenuItem("Tools/OpenCV for Unity/Open Example Assets Downloader", false, 12)]
        public static void OpenExampleAssetsDownloaderWindow()
        {
            OpenCVForUnityExampleAssetsDownloaderWindow m_Window = GetWindow<OpenCVForUnityExampleAssetsDownloaderWindow>(true, "OpenCV for Unity | Example Assets Downloader");
            //m_Window.minSize = new Vector2(320, 535);
            //m_Window.maxSize = new Vector2(320, 800);
        }

        // Private Methods
        private string GetStreamingAssetsFolderPath()
        {
            string[] guids = AssetDatabase.FindAssets("OpenCVForUnityExampleAssetsDownloaderWindow");
            if (guids.Length == 0)
            {
                Debug.LogWarning("OpenCVForUnityExampleAssetsDownloaderWindow.cs is missing.");
                return null;
            }

            string currentScriptPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            string opencvForUnityFolderPath = currentScriptPath.Substring(0, currentScriptPath.LastIndexOf("/Editor/EditorWindow"));
            string streamingAssetsFolderPath = opencvForUnityFolderPath + "/StreamingAssets/OpenCVForUnityExamples";

            return streamingAssetsFolderPath;
        }

        private string[] GetExampleNames(Dictionary<string, string>[] files)
        {
            List<string> names = new List<string>();
            foreach (var item in files)
            {
                if (!names.Contains(item["module"]))
                {
                    names.Add(item["module"]);
                }
                if (!names.Contains(item["name"]))
                {
                    names.Add(item["name"]);
                }
            }

            return names.ToArray();
        }

        private List<Dictionary<string, string>> ExtractFiles(Dictionary<string, string>[] files, string exampleName)
        {
            List<Dictionary<string, string>> extractFiles = new List<Dictionary<string, string>>();
            foreach (var item in files)
            {
                if (item["name"].Equals(exampleName))
                    extractFiles.Add(item);
            }

            return extractFiles;
        }

        private void ShowProgressBar(Dictionary<string, string> file)
        {
            float progress = (float)_downloadingFileIndex / (float)_downloadingFiles.Count + (1 / (float)_downloadingFiles.Count) * _unityWebRequest.downloadProgress;

            Progress.Report(_currentProgressID, progress, string.Format("{0} / {1} " + file["name"] + " : " + file["filename"], _downloadingFileIndex + 1, _downloadingFiles.Count));
        }

        private void StartDownloadFilesAsync(List<Dictionary<string, string>> downloadFiles)
        {
            _isProcessingFiles = true;
            _processingStatus = "Preparing download list...";
            _processingProgress = 0f;
            _processingFileList.Clear();

            // Use EditorApplication.delayCall to process files asynchronously
            EditorApplication.delayCall += () =>
            {
                ProcessFilesAsync(downloadFiles);
            };
        }

        private void ProcessFilesAsync(List<Dictionary<string, string>> downloadFiles)
        {
            // Filter only files that need to be downloaded
            List<Dictionary<string, string>> filesToDownload = new List<Dictionary<string, string>>();
            _skippedFileCount = 0;

            // Dictionary to exclude duplicates (key: module/filename, value: file information)
            Dictionary<string, Dictionary<string, string>> uniqueFiles = new Dictionary<string, Dictionary<string, string>>();

            int totalFiles = downloadFiles.Count;
            int processedFiles = 0;

            // Process files in batches to update UI
            int batchSize = 5; // Process 5 files at a time
            int currentBatch = 0;

            void ProcessNextBatch()
            {
                int startIndex = currentBatch * batchSize;
                int endIndex = Mathf.Min(startIndex + batchSize, totalFiles);

                // Clear the file list for this batch
                _processingFileList.Clear();

                for (int i = startIndex; i < endIndex; i++)
                {
                    var file = downloadFiles[i];
                    processedFiles++;
                    _processingProgress = (float)processedFiles / totalFiles;
                    _processingStatus = $"Checking files {processedFiles}/{totalFiles}";

                    // Add file to the processing list
                    string fileInfo = $"{file["module"]}/{file["filename"]}";
                    _processingFileList.Add(fileInfo);

                    string path = _streamingAssetsFolderPath + "/" + file["module"] + "/" + file["filename"];

                    // Skip if file exists and hash matches
                    if (File.Exists(path) && CheckSHA1Hash(file))
                    {
                        Debug.Log("File already exists and hash matches: " + path);
                        _skippedFileCount++;
                        continue;
                    }

                    // Create a key for duplicate checking (module/filename combination to uniquely identify)
                    string uniqueKey = file["module"] + "/" + file["filename"];

                    // Add only if the same file has not been added yet
                    if (!uniqueFiles.ContainsKey(uniqueKey))
                    {
                        uniqueFiles.Add(uniqueKey, file);
                    }
                    else
                    {
                        Debug.Log("Skipping duplicate file: " + uniqueKey);
                        _skippedFileCount++;
                    }
                }

                currentBatch++;

                // Force GUI update
                EditorApplication.delayCall += () => { Repaint(); };

                // Continue processing if there are more files
                if (endIndex < totalFiles)
                {
                    EditorApplication.delayCall += ProcessNextBatch;
                }
                else
                {
                    // Ensure the final UI update is visible before proceeding
                    EditorApplication.delayCall += () =>
                    {
                        // Update the final status
                        _processingStatus = $"Completed checking {processedFiles}/{totalFiles} files";
                        _processingProgress = 1.0f;
                        Repaint();

                        // Wait a moment to show the final status before proceeding
                        EditorApplication.delayCall += () =>
                        {
                            // All files processed, create download list
                            filesToDownload.AddRange(uniqueFiles.Values);

                            // If all files already exist, finish the process
                            if (filesToDownload.Count == 0)
                            {
                                _isProcessingFiles = false;
                                EditorApplication.delayCall += () =>
                                {
                                    EditorUtility.DisplayDialog("Download Completed",
                                        $"All files already exist and are valid. Skipped {_skippedFileCount} files.", "OK");
                                    Repaint();
                                };
                                return;
                            }

                            // Start downloading files
                            _isProcessingFiles = false;
                            _downloadingFiles = filesToDownload;
                            _downloadingFileIndex = 0;
                            _downloadSuccessFileCount = 0;
                            _downloadFailureFileCount = 0;

                            DownloadFile(_downloadingFiles[_downloadingFileIndex]);
                            IsDownloading = true;

                            _currentProgressID = Progress.Start("OpenCV for Unity Example Assets Downloader", null, Progress.Options.Sticky);
                            Progress.RegisterCancelCallback(_currentProgressID, () =>
                            {
                                //Debug.Log("The task has been canceled.");

                                CancelDownloadFiles();

                                return true;
                            });
                            Progress.ShowDetails(false);
                        };
                    };
                }
            }

            // Start processing the first batch
            ProcessNextBatch();
        }

        private void CancelDownloadFiles()
        {
            _unityWebRequest.Abort();
            IsDownloading = false;
        }

        private void DownloadFile(Dictionary<string, string> file)
        {
            _unityWebRequest?.Dispose();

            _unityWebRequest = new UnityWebRequest(file["url"], UnityWebRequest.kHttpVerbGET);
            string path = _streamingAssetsFolderPath + "/" + file["module"] + "/" + file["filename"];
            _unityWebRequest.downloadHandler = new DownloadHandlerFile(path);
            _unityWebRequest.EditorSendWebRequest(
                //onSuccess
                () =>
                {
                    if (CheckSHA1Hash(file))
                    {
                        Debug.Log("Download Success: " + path);

                        _downloadSuccessFileCount++;
                    }
                    else
                    {
                        Debug.LogError("Download Failure: " + path + " Incorrect hash value.");

                        FileUtil.DeleteFileOrDirectory(path);

                        _downloadFailureFileCount++;
                    }

                    ContinueOrFinishDownloadFile();
                },
                //onError
                () =>
                {
                    //delete error file.
                    if (FileUtil.DeleteFileOrDirectory(path))
                    {
                        //Debug.LogError("delete " + path);
                    }

                    //Candeled by ProgressBar.
                    if (!IsDownloading)
                    {
                        Debug.LogWarning("Download Cancel: " + path);
                        AssetDatabase.Refresh();
                        return;
                    }

                    Debug.LogError("Download Failure: " + path + " " + _unityWebRequest.error + "\nIf the network environment is bad, please retry after a while.");

                    _downloadFailureFileCount++;

                    ContinueOrFinishDownloadFile();
                }
                );
        }

        private void ContinueOrFinishDownloadFile()
        {
            _downloadingFileIndex++;
            if (_downloadingFileIndex > _downloadingFiles.Count - 1)
            {
                IsDownloading = false;

                AssetDatabase.Refresh();

                // Set progress bar to 100% and finish it
                Progress.Report(_currentProgressID, 1.0f, "Download completed");
                Progress.Finish(_currentProgressID, Progress.Status.Succeeded);

                // Use EditorApplication.delayCall to ensure the progress bar is updated before showing the dialog
                EditorApplication.delayCall += () =>
                {
                    // Add a small delay to ensure the progress bar is fully updated
                    EditorApplication.delayCall += () =>
                    {
                        if (_downloadSuccessFileCount == _downloadingFiles.Count)
                        {
                            EditorUtility.DisplayDialog("Download Completed",
                                $"All files have been successfully downloaded. Success: {_downloadSuccessFileCount}, Skipped: {_skippedFileCount}.", "OK");
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Download Completed",
                                $"Success: {_downloadSuccessFileCount}, Failure: {_downloadFailureFileCount}, Skipped: {_skippedFileCount}\nFiles that failed to download are output to the console.", "OK");
                        }
                    };
                };
            }
            else
            {
                DownloadFile(_downloadingFiles[_downloadingFileIndex]);
            }
        }

        private bool CheckSHA1Hash(Dictionary<string, string> file)
        {
            //skip check
            if (!file.ContainsKey("sha")) return true;

            string filePath = _streamingAssetsFolderPath + "/" + file["module"] + "/" + file["filename"];

            // Return false if file doesn't exist
            if (!File.Exists(filePath)) return false;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                    {
                        byte[] bs = sha1.ComputeHash(fs);
                        string result = BitConverter.ToString(bs).ToLower().Replace("-", "");

                        if (_isDebugMode && !result.Equals(file["sha"]))
                        {
                            Debug.LogError("sha1 hash check failed: " + filePath + "\n expected: " + file["sha"] + "\n actual: " + result);
                        }

                        return result.Equals(file["sha"]);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error checking SHA1 hash for {filePath}: {e.Message}");
                return false;
            }
        }
    }

    // Public Methods
    public static class WebRequestInEditor
    {
        public static void EditorSendWebRequest(this UnityWebRequest self, Action onSuccess = null, Action onError = null)
        {
            self.SendWebRequest();

            EditorApplication.CallbackFunction updateFunc = null;
            updateFunc = () =>
            {
                if (self.isDone)
                {
                    EditorApplication.update -= updateFunc;

                    switch (self.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                        case UnityWebRequest.Result.ConnectionError:
                        case UnityWebRequest.Result.ProtocolError:
                        case UnityWebRequest.Result.DataProcessingError:
                            onError?.Invoke();
                            break;

                        case UnityWebRequest.Result.Success:
                            onSuccess?.Invoke();
                            break;

                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            };

            EditorApplication.update += updateFunc;
        }
    }
}
#endif
