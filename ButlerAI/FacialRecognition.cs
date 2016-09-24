using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Face;

namespace ButlerAI
{
    public class FacialeRecognition
    {

        EigenFaceRecognizer faceRecogniser = new EigenFaceRecognizer();

        public string Recogise(Mat inputFace)
        {
            var result = faceRecogniser.Predict(inputFace);

            return string.Empty;            
        }

        public string Learn(Mat inputFace, string name)
        {

        }
        public void SaveTrainingData()
        {

        }

        public void LoadTrainingData()
        {

        }

    }
}
