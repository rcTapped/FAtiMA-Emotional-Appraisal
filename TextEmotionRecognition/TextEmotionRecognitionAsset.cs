﻿using AssetPackage;
using System;
using System.Net;
using TextEmotionRecognition.DTOs;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using RealTimeEmotionRecognition;
using System.Linq;

namespace TextEmotionRecognition
{
    public class TextEmotionRecognitionAsset : BaseAsset, IAffectRecognitionAsset
    {

        public string Language { get; set; }
        public string LSACorpus { get; set; }
        public string LDACorpus { get; set; }
        public bool PostTagging { get; set; }
        public bool Dialogism { get; set; }

        public float DecayWindow { get; set; }
        private DateTime SampleTime { get; set; }

        private IEnumerable<AffectiveInformation> CurrentSample { get; set; }

        public string Name
        {
            get
            {
                return "TextEmotionRecognition";
            }
        }

        public TextEmotionRecognitionAsset()
        {
            this.Language = "en";
            this.LSACorpus = "resources/config/LSA/tasa_en";
            this.LDACorpus = "resources/config/LDA/tasa_en";
            this.PostTagging = false;
            this.Dialogism = false;

            this.DecayWindow = 5.0f;//5 seconds by default

            this.CurrentSample = new List<AffectiveInformation>();
        }


        public IEnumerable<AffectiveInformation> ProcessText(string text)
        {
            string result;

            var request = this.CreateRequest(text);

            try
            {
                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Close();


                var responseDTO = JsonConvert.DeserializeObject<SentimentAnalysisResponseDTO>(result);

                this.CurrentSample = responseDTO.Data[0].Valences.Select(v => new AffectiveInformation { Name = v.Content, Score = v.Score });
                this.SampleTime = DateTime.Now;

                return this.CurrentSample; 
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private HttpWebRequest CreateRequest(string text)
        {
            var escapedText = Uri.EscapeDataString(text);
            string parameters = "?text=" + escapedText + "&lang=" + this.Language + "&lsa=" + this.LSACorpus + "&lda=" + this.LDACorpus + "&postagging=" + this.PostTagging + "&dialogism=" + this.Dialogism;
            
            HttpWebRequest request = WebRequest.Create(new Uri("http://readerbench.com:8080/getSentiment" + parameters)) as HttpWebRequest;
            request.Method = "GET";

            return request;
        }



        public void Test()
        {
            var result = this.ProcessText("RAGE aims to develop, transform and enrich advanced technologies from the leisure games industry into self-contained gaming assets (i.e. solutions showing economic value potential) that support game studios at developing applied games easier, faster and more cost-effectively. These assets will be available along with a large volume of high-quality knowledge resources through a self-sustainable Ecosystem, which is a social space that connects research, gaming industries, intermediaries, education providers, policy makers and end-users. RAGE – Realising an Applied Gaming Eco-system,  is a 48-months Technology and Know-How driven Research and Innovation project co-funded by EU Framework Programme for Research and Innovation, Horizon 2020.");

            return;
        }

        public IEnumerable<AffectiveInformation> GetSample()
        {
            if(this.CurrentSample.Count() > 0 && (DateTime.Now - this.SampleTime).Seconds > this.DecayWindow)
            {
                this.CurrentSample = new List<AffectiveInformation>();
            }

            return this.CurrentSample;
        }
    }
}
