using Mews.Fiscalizations.Austria.Dto;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Austria;

public interface ISigner
{
    Task<SignerOutput> SignAsync(QrData qrData);
}