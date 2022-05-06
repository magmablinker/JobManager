using JobManager.Core.Data.DataTransferObjects;
using JobManager.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JobManager.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly LanguageDto _languageDto;
        private readonly string _language;

        public LanguageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _languageDto = ParseLanguage(env.ContentRootPath);

            _language = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Language")
                ? httpContextAccessor.HttpContext.Request.Headers["X-Language"].ToString()
                : "en";
        }

        public string Get(string key, params string[] args)
        {
            if (!_languageDto.LanguageItems.ContainsKey(key)) throw new ArgumentException($"{key} is not a valid language item");

            if (!_languageDto.LanguageItems[key].Languages.ContainsKey(_language)) throw new ArgumentException($"{_language} is not available for {key}");

            return string.Format(_languageDto.LanguageItems[key].Languages[_language], args);
        }

        private LanguageDto ParseLanguage(string contentRootPath)
        {
            var languageFileContents = File.ReadAllText($"{contentRootPath}{Path.DirectorySeparatorChar}Static{Path.DirectorySeparatorChar}Language{Path.DirectorySeparatorChar}language.json");

            var languageDto = new LanguageDto();

            var items = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(languageFileContents);

            foreach(var item in items)
            {
                LanguageItem languageItem = new LanguageItem();

                foreach(var language in item.Value)
                {
                    languageItem.Languages.Add(language.Key, language.Value);
                }

                languageDto.LanguageItems.Add(item.Key, languageItem);
            }

            return languageDto;
        }
    }
}
