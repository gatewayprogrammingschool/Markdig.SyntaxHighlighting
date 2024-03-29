﻿using System.Text.RegularExpressions;

using MDS.ColorCode;

namespace MDS.Markdig.SyntaxHighlighting;

public class LanguageTypeAdapter
{
    private readonly Dictionary<string, ILanguage> languageMap = new()
    {
        {"csharp", Languages.CSharp},
        {"cplusplus", Languages.Cpp},
    };

    public ILanguage Parse(string id, string firstLine = null)
    {
        if (id == null)
        {
            return null;
        }

        if (languageMap.ContainsKey(id))
        {
            return languageMap[id];
        }

        if (!string.IsNullOrWhiteSpace(firstLine))
        {
            foreach (var lang in Languages.All)
            {
                if (lang.FirstLinePattern == null)
                {
                    continue;
                }

                var firstLineMatcher = new Regex(lang.FirstLinePattern, RegexOptions.IgnoreCase);

                if (firstLineMatcher.IsMatch(firstLine))
                {
                    return lang;
                }
            }
        }

        var byIdCanidate = Languages.FindById(id);

        return byIdCanidate;
    }
}