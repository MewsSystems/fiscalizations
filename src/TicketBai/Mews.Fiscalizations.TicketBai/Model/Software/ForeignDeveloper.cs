﻿using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class ForeignDeveloper
    {
        public ForeignDeveloper(IdType idType, String1To20 id, Country country = null)
        {
            IdType = Check.IsNotNull(idType, nameof(idType));
            Id = Check.IsNotNull(id, nameof(id));
            Country = country.ToOption();
        }

        public IdType IdType { get; }

        public String1To20 Id { get; }

        public IOption<Country> Country { get; }
    }
}