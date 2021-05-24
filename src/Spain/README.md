# Mews.Fiscalizations.Spain (SII)

SII stands for Suministro Inmediato de Información del IVA, which translates to Immediate Supply of Information on VAT.
It's an online API provided by Spanish authorities in a form of a SOAP Web Service.

## Key features
- No SPanish abbreviations.
- Early data validation.
- Intuitive immutable DTOs.

## Known issues
- We didn't implement deleting or modifying already existing records.

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

# NuGet

We have published the library as [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain/).

# Authors
Development: [@PavelKalandra](https://github.com/KaliCZ), [@MiroslavVeith](https://github.com/mveith)

# Who uses the library in production?
- [Mews](https://mews.com) - Property Management Solution for the 21st century.

We would like to hear your story and know who users of the lib are. Please, thank us for providing the library by sharing with us, who you are and letting us add you into this list.

The time to implement this was kindly provided by [Mews](https://mews.com).

# Donate
There is no need to donate the project, but thanks for considering it! Instead, if you like the project, star it here on GitHub :-)! Thanks!

If you still insist on donating, we accept gummy bears at Mews Systems, IP Pavlova 5, Vinohrady 
120 00 Prague. This project was, of course, powered by a huge pile of gummy bears ;-)
