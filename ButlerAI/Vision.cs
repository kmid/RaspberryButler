using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Drawing;



namespace ButlerAI
{
    
    /// <summary>
    /// Captures frames from either a webcam or a video stream avi
    /// </summary>
    public class Vision
    {
        private readonly Capture captureDevice = null;


        public Vision()
        {
            captureDevice = new Capture();
        }
        
        /// <summary>
        /// Get a list of areas containing detected faces 
        /// </summary>
        public Bitmap GetFaces()
        {
            var frame = captureDevice.QueryFrame();
            return frame.ToImage<Gray,byte>().ToBitmap();
        }

        public Image WebCameView;
        private Mat currentFrame = new Mat();
        private Mat resizedFrame = new Mat();

        public void GrabFrame(object sender, EventArgs e)
        {

            captureDevice.Retrieve(currentFrame, 0);
            CvInvoke.Resize(resizedFrame, currentFrame, new Size(320, 240), 0, 0, Emgu.CV.CvEnum.Inter.Cubic);

        }

        
    }

    
}
