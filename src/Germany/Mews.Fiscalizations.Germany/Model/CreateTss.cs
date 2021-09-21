namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class CreateTss : Tss
    {
        public CreateTss(string adminPuk, Tss tss)
            : base(tss.Id, tss.Description, tss.State, tss.CreatedUtc, tss.InitializedUtc, tss.DisabledUtc, tss.Certificate, tss.SerialNumber, tss.PublicKey, tss.SignatureCounter, tss.SignatureAlgorithm, tss.TransactionCounter)
        {
            AdminPuk = adminPuk;
        }

        public string AdminPuk { get; }
    }
}
