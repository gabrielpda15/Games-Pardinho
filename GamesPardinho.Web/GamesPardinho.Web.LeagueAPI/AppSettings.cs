using GamesPardinho.Web.LeagueAPI.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GamesPardinho.Web.LeagueAPI
{
    public class AppSettings
    {
        public const string FILE_NAME = "appsettings.json";

        public NVObject ApiKey { get; set; }

        public static AppSettings LoadSettings()
        {
            if (File.Exists(FILE_NAME))
            {
                var rawFile = File.ReadAllText(FILE_NAME);
                return JsonConvert.DeserializeObject<AppSettings>(rawFile);
            }
            return null;
        }
    }
}
