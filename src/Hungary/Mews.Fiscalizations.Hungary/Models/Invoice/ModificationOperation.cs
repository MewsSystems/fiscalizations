namespace Mews.Fiscalizations.Hungary.Models;

public enum ModificationOperation
{
    /// <summary>NAV MODIFY - the document changes the original invoice.</summary>
    Modify,

    /// <summary>NAV STORNO - the document cancels the original invoice in full.</summary>
    Storno
}
