using System.Collections.Generic;
using System.Linq;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Nif
{
    public enum NifSearchResult
    {
        Found,
        NotFoundBecauseNifModifiedByServer,
        NotFound,
        NotProcessed,
        Other
    }

    public class NifInfoEntry
    {
        public NifInfoEntry(TaxpayerIdentificationNumber taxpayerNumber, string name)
        {
            TaxpayerNumber = Check.IsNotNull(taxpayerNumber, nameof(taxpayerNumber));
            Name = Check.IsNotNull(name, nameof(name));
        }

        public TaxpayerIdentificationNumber TaxpayerNumber { get; }

        public string Name { get; }
    }

    public class NifInfoResults
    {
        public NifInfoResults(string taxId, string name, NifSearchResult result, string resultMessage = null)
        {
            TaxId = taxId;
            Name = name;
            Result = result;
            ResultMessage = resultMessage.ToOption();
        }

        public string TaxId { get; }

        public string Name { get; }

        public NifSearchResult Result { get; }

        public IOption<string> ResultMessage { get; }
    }

    public class Request
    {
        public Request(INonEmptyEnumerable<NifInfoEntry> entries)
        {
            Entries = entries;
        }

        public INonEmptyEnumerable<NifInfoEntry> Entries { get; }
    }

    public class Response
    {
        public Response(IEnumerable<NifInfoResults> results)
        {
            Results = results.ToList();
        }

        public IEnumerable<NifInfoResults> Results { get; }
    }
}