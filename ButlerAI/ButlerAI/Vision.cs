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

using Emgu


namespace ButlerAI
{
    public class Vision
    {
        private Capture captureDevice = null;
        


        public Vision()
        {
            captureDevice = new Capture(@"C:\per\billy.mp4");
            

        }
        
        /// <summary>
        /// Get a list of areas containing detected faces 
        /// </summary>
        public Bitmap GetFaces()
        {
            var frame = captureDevice.QueryFrame();
            return frame.ToImage<Gray,byte>().ToBitmap();
        }
    }

    
}
