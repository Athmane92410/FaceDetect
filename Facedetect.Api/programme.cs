using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford;
using Microsoft.ProjectOxford.Face;
using System.IO;
using Microsoft.ProjectOxford.Face.Contract;
namespace Facedetect.Api
{
    public class programme
    {
        string personGroupId;
        private readonly IFaceServiceClient faceServiceClient;
        private const string KEY = "d98a85ae-d924-4aef-8570-83a2b528faa6";
        const string URIBASE = "https://eastus2.api.cognitive.microsoft.com/face/v1.0/detect";
        static FaceServiceClient CLIENT;
        public programme()
       {
        personGroupId  = "me2";
            //faceServiceClient = new FaceServiceClient("94dcb6a56f284ceb9c8b89db34614cbc");
            //faceServiceClient = new FaceServiceClient(KEY);
              
              faceServiceClient = new FaceServiceClient("cdaa051499427c9db2f7820a02fffd");
        }
       
       private async Task<FaceRectangle[]> UploadAndDetectFaces(string imageFilePath)
       {
           try
           {
               using (Stream imageFileStream = File.OpenRead(imageFilePath))
               {
                   var faces = await faceServiceClient.DetectAsync(imageFileStream);
                   var faceRects = faces.Select(face => face.FaceRectangle);
                   return faceRects.ToArray();
               }
           }
           catch (Exception)
           {
               return new FaceRectangle[0];
           }
       }

       public  async void Detect(Stream testImageFile)
       {
         //  using (Stream s = File.OpenRead(testImageFile))
          //{
           var faces = await faceServiceClient.DetectAsync(testImageFile);
               var faceIds = faces.Select(face => face.FaceId).ToArray();

               var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
               foreach (var identifyResult in results)
               {
                   // Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                  // MessageBox.Show(identifyResult.FaceId.ToString());
                   if (identifyResult.Candidates.Length == 0)
                   {
                       Console.WriteLine("No one identified");
                   }
                   else
                   {
                       // Get top 1 among all candidates returned
                       var candidateId = identifyResult.Candidates[0].PersonId;
                       var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                       // Console.WriteLine(;
                     //  MessageBox.Show(person.Name);
                   }
              // }

           }
       }

       public async void AddPresons(string friend1ImageDi)
        {
        
         try
            {
                await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Athmane");

                // Define Anna
                CreatePersonResult friend1 = await faceServiceClient.CreatePersonAsync(
                    // Id of the person group that the person belonged to
                    personGroupId,
                    // Name of the person
                    "Makour"
                );
                const string friend1ImageDir = @"C:\Users\Athmane\Pictures\moi";//@"C:\Users\Athmane\Pictures\test";//"C:\Users\Athmane\Downloads\Cognitive-Face-Windows-master\Cognitive-Face-Windows-master\Data\PersonGroup\Family1-Mom";

                foreach (string imagePath in Directory.GetFiles(friend1ImageDir, "*.JPEG"))
                {
                    using (Stream s = File.OpenRead(imagePath))
                    {
                        // Detect faces in the image and add to Anna
                        await faceServiceClient.AddPersonFaceAsync(
                            personGroupId, friend1.PersonId, s);
                    }
                }
                await faceServiceClient.TrainPersonGroupAsync(personGroupId);
                TrainingStatus trainingStatus = null;
                while (true)
                {
                    trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);

                    if (trainingStatus.Status != Status.Running)
                    {
                        break;
                    }

                    await Task.Delay(1000);
                }
            }
            catch (Exception ec)
            {
                return;
            }
        }
    }
}
