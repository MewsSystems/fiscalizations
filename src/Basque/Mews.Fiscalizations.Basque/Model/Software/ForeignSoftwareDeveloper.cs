﻿namespace Mews.Fiscalizations.Basque.Model;

public sealed class ForeignSoftwareDeveloper
{
    public ForeignSoftwareDeveloper(IdType idType, String1To20 id, Country country = null)
    {
        IdType = Check.IsNotNull(idType, nameof(idType));
        Id = Check.IsNotNull(id, nameof(id));
        Country = country.ToOption();
    }

    public IdType IdType { get; }

    public String1To20 Id { get; }

    public Option<Country> Country { get; }
}