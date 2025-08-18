using Amazon.Runtime;
using Amazon.Translate;
using Amazon.Translate.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerizeMVC.Models;
using System.Net;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace SummerizeMVC.Controllers
{
    public class JsonController : Controller
    {
        const string aws_access_key_id = "AKIAV4VW7JRJEMN6QOFK";
        const string aws_secret_access_key = "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";
        public JsonController() { }

        [HttpPost]
        public async Task<ActionResult> RequestTranscribeModel([FromBody]TranscribeModel model)
        {
            var homeModel = new HomeModel();
            if (string.IsNullOrEmpty(model.prompt)) return Json(homeModel);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://b9xup5upy7.execute-api.eu-central-1.amazonaws.com/DEV/text");
            var test = Request;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            List<string> badWords = new List<string>();
            //badWords.Add("naar mijn mening kunt u dus gewoon veilig op reis om uw dochter te bezoeken.");
            //badWords.Add("error: transcription error occurred: your request timed out because no new audio was received for 15 seconds.");
            badWords.Add("Error: Transcription error occurred: Your request timed out because no new audio was received for 15 seconds.");
            foreach (var item in badWords)
            {
                model.prompt = model.prompt.Replace(item, "");
            }
            BasicAWSCredentials credentials = new BasicAWSCredentials(aws_access_key_id, aws_secret_access_key);
            //var request1 = new TranslateTextRequest
            //{
            //    SourceLanguageCode = "nl",
            //    TargetLanguageCode = "en", 
            //    Text = model.prompt
            //};
            //using (var client = new AmazonTranslateClient(credentials))
            //{
            //    var response = await client.TranslateTextAsync(request1);
            //    model.prompt = response.TranslatedText;
            //}
            model.prompt = await TranslateNLtoEN(model.prompt, credentials);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"modelId\":\"amazon.titan-text-express-v1\"," +
               "\"prompt\":\"_xxx_\"}";

                json = json.Replace("_xxx_", model.prompt);

                streamWriter.Write(json);
            }
            
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                homeModel.Result = streamReader.ReadToEnd();
            }

            homeModel.Result = await TranslateENtoNL(homeModel.Result, credentials);

            //var request2 = new TranslateTextRequest
            //{
            //    SourceLanguageCode = "en",
            //    TargetLanguageCode = "nl",
            //    Text = homeModel.Result
            //};

            //using (var client = new AmazonTranslateClient(credentials))
            //{                
            //    var response = await client.TranslateTextAsync(request2);
            //    homeModel.Result = response.TranslatedText;
            //}
            return Json(homeModel);
        }

        private async Task<string> TranslateNLtoEN(string text, BasicAWSCredentials credentials)
        {
            string result = "";
            var request1 = new TranslateTextRequest
            {
                SourceLanguageCode = "nl",
                TargetLanguageCode = "en",
                Text = text
            };
            using (var client = new AmazonTranslateClient(credentials))
            {
                var response = await client.TranslateTextAsync(request1);
                result = response.TranslatedText;
            }
            return result;
        }

        private async Task<string> TranslateENtoNL(string text, BasicAWSCredentials credentials)
        {
            string result = "";
            var request1 = new TranslateTextRequest
            {
                SourceLanguageCode = "en",
                TargetLanguageCode = "nl",
                Text = text
            };
            using (var client = new AmazonTranslateClient(credentials))
            {
                var response = await client.TranslateTextAsync(request1);
                result = response.TranslatedText;
            }
            return result;
        }


        //public ActionResult TextToSpeech(string text)
        //{
        //    string filename = "";
        //    try
        //    {
        //        AWSCredentials credentials = new StoredProfileAWSCredentials("my_credential");
        //        AmazonPollyClient client = new AmazonPollyClient(credentials, Amazon.RegionEndpoint.EUWest1);

        //        // Create describe voices request.
        //        DescribeVoicesRequest describeVoicesRequest = new DescribeVoicesRequest();
        //        // Synchronously ask Amazon Polly to describe available TTS voices.
        //        DescribeVoicesResponse describeVoicesResult = client.DescribeVoices(describeVoicesRequest);
        //        List<Voice> voices = describeVoicesResult.Voices;


        //        // Create speech synthesis request.
        //        SynthesizeSpeechRequest synthesizeSpeechPresignRequest = new SynthesizeSpeechRequest();
        //        // Text
        //        synthesizeSpeechPresignRequest.Text = text;
        //        // Select voice for synthesis.
        //        synthesizeSpeechPresignRequest.VoiceId = voices[18].Id;
        //        // Set format to MP3.
        //        synthesizeSpeechPresignRequest.OutputFormat = OutputFormat.Mp3;
        //        // Get the presigned URL for synthesized speech audio stream.

        //        string current_dir = AppDomain.CurrentDomain.BaseDirectory;
        //        filename = CalculateMD5Hash(text) + ".mp3";
        //        var path_audio = current_dir + @"\Audios\" + filename;

        //        var presignedSynthesizeSpeechUrl = client.SynthesizeSpeechAsync(synthesizeSpeechPresignRequest).GetAwaiter().GetResult();

        //        FileStream wFile = new FileStream(path_audio, FileMode.Create);
        //        presignedSynthesizeSpeechUrl.AudioStream.CopyTo(wFile);
        //        wFile.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        filename = ex.ToString();
        //    }

        //    return Json(filename, JsonRequestBehavior.AllowGet);
        //}
    }
}
