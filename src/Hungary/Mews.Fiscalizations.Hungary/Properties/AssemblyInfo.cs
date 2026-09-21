using System.Runtime.CompilerServices;

// The request mapping and the NAV error code table are internal on purpose - they are how this library talks
// to NAV, not part of its contract. The tests assert on them directly rather than only through the public
// client, because that is where a mistake becomes an invalid data report.
[assembly: InternalsVisibleTo("Mews.Fiscalizations.Hungary.Tests")]
