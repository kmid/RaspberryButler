using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ButlerAI
{
    public class TrainingData
    {
        public Image<Gray, byte> TrainingFaceImage { get; set; }
        public int TrainingFaceId { get; set; }
        public string TrainingFaceName { get; set; }

        public TrainingData()
        {            
        }

    }
}
