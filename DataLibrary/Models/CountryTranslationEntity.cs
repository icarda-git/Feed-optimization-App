﻿using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CountryTranslationEntity : EntityBase
{
    public CountryTranslationEntity()
    {
    }

    public CountryTranslationEntity(int countryId, string languageCode, string translatedDescription)
    {
        CountryId = countryId;
        LanguageCode = languageCode;
        TranslatedDescription = translatedDescription;
    }

    public int CountryId { get; set; } // Reference to Ref_Country.Id

    public string LanguageCode { get; set; } // NOT NULL

    public string TranslatedDescription { get; set; } // NOT NULL

    public CountryEntity Country { get; set; }
}