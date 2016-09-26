using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;



namespace ButlerAI
{
    
    /// <summary>
    /// Captures frames from either a webcam or a video stream avi
    /// </summary>
    public class Vision
    {
        private readonly Capture captureDevice = null;

        private CascadeClassifier faceDetector;
        public Vision()
        {
            captureDevice = new Capture();
            //faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
            faceDetector = new CascadeClassifier("haarcascade_profileface.xml");
        }       


        public Image WebCamView { get; private set; }
        public Image ProcessedImage {get; private set;}
        public List<Image> FacesDetected { get; private set; }

        public event EventHandler FrameScanned;
        private Mat currentFrame = new Mat();
        private Mat grayFrame = new Mat();              

        public void ScanFrameForFaces(object sender, EventArgs e)
        {
            captureDevice.Retrieve(currentFrame, 0);
            CvInvoke.Resize(currentFrame, currentFrame, new Size(320, 240));
            CvInvoke.CvtColor(currentFrame, grayFrame, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(grayFrame, grayFrame);

            Rectangle[] facesDetected = (faceDetector.DetectMultiScale(grayFrame, 1.1, 10, new Size(50, 50)));

            FacesDetected = new List<Image>();

            //Extract the scanned frames only if we have the handler wired up from the calling library
            var frameScanned = FrameScanned;
            if (frameScanned != null)
            {
                //Draw a red box around each detected face
                //and extract each face
                foreach (Rectangle face in facesDetected)
                {
                    CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);
                    var f = new Mat(grayFrame, face);
                    CvInvoke.Resize(f, f, new Size(100, 100));
                    FacesDetected.Add(f.Bitmap);
                }

                WebCamView = currentFrame.Bitmap;
                ProcessedImage = grayFrame.Bitmap;
                frameScanned(this, e);
            }
        }


        


    }

    
}
