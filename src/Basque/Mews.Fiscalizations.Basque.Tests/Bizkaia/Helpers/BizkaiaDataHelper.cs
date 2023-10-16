using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;

internal class BizkaiaDataHelper
{
    internal static BizkaiaData CreateSampleBizkaiaData() 
    {
        return new BizkaiaData
        {
            con = "LROE",
            apa = "1.1",
            inte = new BizkaiaData.Inte
            {
                nif = "NIF",
                nrs = "Name",
                ap1 = "Family name 1",
                ap2 = "Family name 2"
            },
            drs = new BizkaiaData.Drs
            {
                mode = "140/240",
                ejer = "ejercicio"
            }
        };
    }
}
