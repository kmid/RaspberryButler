using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Newtonsoft.Json;
using System.Drawing;
using Emgu.CV.CvEnum;


namespace ButlerAI
{
    public class FacialRecognition
    {
        private EigenFaceRecognizer faceRecogniser;

        public int EigenFaces { get; set; }
        public double EigenThreshold { get; set; }

        List<TrainingData> trainingData = new List<TrainingData>();

        public FacialRecognition()
        {
            //Default settings
            EigenFaces = 300;
            EigenThreshold = double.PositiveInfinity;
            faceRecogniser = new EigenFaceRecognizer(EigenFaces,EigenThreshold);
        }

        public MatchedData Recogise(Bitmap inputFace)
        {

            var result = faceRecogniser.Predict(new Image<Gray, byte>(inputFace).Mat);
            var matchedObject = trainingData
                .Where(l => l.TrainingFaceId == result.Label)
                .Select(l => new MatchedData{ TrainingFaceName = l.TrainingFaceName, TrainingFaceImage = l.TrainingFaceImage.Bitmap, Distance = result.Distance})
                .FirstOrDefault();
            return matchedObject;
        }

        private void Learn()
        {
            var faces = trainingData.Select(s => s.TrainingFaceImage).ToArray();
            var labels = trainingData.Select(s => s.TrainingFaceId).ToArray();
            faceRecogniser.Train(faces,labels);
        }


        public void LoadEigenRecogniser(string contentLocation, string networkFilename)
        {
            if (File.Exists(contentLocation + networkFilename))
                faceRecogniser.Load(contentLocation + networkFilename);
         
        }

        public void SaveEigenRecogniser(string contentLocation, string networkFilename)
        {

            if (Directory.Exists(contentLocation))
                faceRecogniser.Save(contentLocation + networkFilename);
        }

        public void AddFaceToTrainingData(Bitmap face, string name, bool canRetrain)
        {
            var faceData = new TrainingData()
            {
                TrainingFaceImage = new Image<Gray, byte>(face),
                TrainingFaceName = name,
                TrainingFaceId = trainingData.Count
            };

            trainingData.Add(faceData);
            if (canRetrain)
                Learn();
        }

        /// <summary>
        /// Load all the training images into the system and do a learn cycle
        /// </summary> 
        public void LoadTrainingData(string contentLocation, string trainingFilename)
        {
            if (File.Exists(contentLocation + trainingFilename))
            {
                var jsonDataFile = File.ReadAllText(contentLocation + trainingFilename);
                trainingData = JsonConvert.DeserializeObject<List<TrainingData>>(jsonDataFile);
                Learn();
            }
        }

        public void SaveTrainingData(string contentLocation, string trainingFilename)
        {
            if (Directory.Exists(contentLocation))
            {
                var jsonDataFile = JsonConvert.SerializeObject(trainingData.ToArray());
                File.WriteAllText(contentLocation + trainingFilename, jsonDataFile);
            }
        }
    }
}
