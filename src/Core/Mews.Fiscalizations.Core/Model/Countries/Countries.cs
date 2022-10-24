using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
{
    public static class Countries
    {
        static Countries()
        {
            All = NonEmptyEnumerable.Create(
                AalandIslands = NonEuropean("AX"),
                Afghanistan = NonEuropean("AF"),
                Albania = NonEuropean("AL"),
                Algeria = NonEuropean("DZ"),
                AmericanSamoa = NonEuropean("AS"),
                Andorra = NonEuropean("AD"),
                Angola = NonEuropean("AO"),
                Anguilla = NonEuropean("AI"),
                Antarctica = NonEuropean("AQ"),
                AntiguaAndBarbuda = NonEuropean("AG"),
                Argentina = NonEuropean("AR"),
                Armenia = NonEuropean("AM"),
                Aruba = NonEuropean("AW"),
                Australia = NonEuropean("AU"),
                Austria = European("AT", taxpayerNumberPattern: "^(AT)?U[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^U[0-9]{8}$"),
                Azerbaijan = NonEuropean("AZ"),
                Bahamas = NonEuropean("BS"),
                Bahrain = NonEuropean("BH"),
                Bangladesh = NonEuropean("BD"),
                Barbados = NonEuropean("BB"),
                Belarus = NonEuropean("BY"),
                Belgium = European("BE", taxpayerNumberPattern: "^(BE)?0[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^0[0-9]{9}$"),
                Belize = NonEuropean("BZ"),
                Benin = NonEuropean("BJ"),
                Bermuda = NonEuropean("BM"),
                Bhutan = NonEuropean("BT"),
                Bolivia = NonEuropean("BO"),
                BonaireSintEustatiusAndSaba = NonEuropean("BQ"),
                BosniaAndHerzegovina = NonEuropean("BA"),
                Botswana = NonEuropean("BW"),
                BouvetIsland = NonEuropean("BV"),
                Brazil = NonEuropean("BR"),
                BritishIndianOceanTerritory = NonEuropean("IO"),
                BruneiDarussalam = NonEuropean("BN"),
                Bulgaria = European("BG", taxpayerNumberPattern: "^(BG)?[0-9]{9,10}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9,10}$"),
                BurkinaFaso = NonEuropean("BF"),
                Burundi = NonEuropean("BI"),
                CaboVerde = NonEuropean("CV"),
                Cambodia = NonEuropean("KH"),
                Cameroon = NonEuropean("CM"),
                Canada = NonEuropean("CA"),
                CaymanIslands = NonEuropean("KY"),
                CentralAfricanRepublic = NonEuropean("CF"),
                Chad = NonEuropean("TD"),
                Chile = NonEuropean("CL"),
                China = NonEuropean("CN"),
                ChristmasIsland = NonEuropean("CX"),
                CocosIslands = NonEuropean("CC"),
                Colombia = NonEuropean("CO"),
                Comoros = NonEuropean("KM"),
                Congo = NonEuropean("CG"),
                CongoDemocraticRepublic = NonEuropean("CD"),
                CookIslands = NonEuropean("CK"),
                CostaRica = NonEuropean("CR"),
                CoteIvoire = NonEuropean("CI"),
                Croatia = NonEuropean("HR"),
                Cuba = NonEuropean("CU"),
                Curacao = NonEuropean("CW"),
                Cyprus = European("CY", taxpayerNumberPattern: "^(CY)?[0-9]{8}L$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}L$"),
                CzechRepublic = European("CZ", taxpayerNumberPattern: "^(CZ)?[0-9]{8,10}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8,10}$"),
                Denmark = European("DK", taxpayerNumberPattern: "^(DK)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                Djibouti = NonEuropean("DJ"),
                Dominica = NonEuropean("DM"),
                DominicanRepublic = NonEuropean("DO"),
                Ecuador = NonEuropean("EC"),
                Egypt = NonEuropean("EG"),
                ElSalvador = NonEuropean("SV"),
                EquatorialGuinea = NonEuropean("GQ"),
                Eritrea = NonEuropean("ER"),
                Estonia = European("EE", taxpayerNumberPattern: "^(EE)?[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9}$"),
                Ethiopia = NonEuropean("ET"),
                FalklandIslands = NonEuropean("FK"),
                FaroeIslands = NonEuropean("FO"),
                Fiji = NonEuropean("FJ"),
                Finland = European("FI", taxpayerNumberPattern: "^(FI)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                France = European("FR", taxpayerNumberPattern: "^(FR)?[0-9A-Z]{2}[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9A-Z]{2}[0-9]{9}$"),
                FrenchGuiana = NonEuropean("GF"),
                FrenchPolynesia = NonEuropean("PF"),
                FrenchSouthernTerritories = NonEuropean("TF"),
                Gabon = NonEuropean("GA"),
                Gambia = NonEuropean("GM"),
                Georgia = NonEuropean("GE"),
                Germany = European("DE", taxpayerNumberPattern: "^(DE)?[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9}$"),
                Ghana = NonEuropean("GH"),
                Gibraltar = NonEuropean("GI"),
                Greece = European("GR", taxpayerNumberPattern: "^(EL|GR)?[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9}$"),
                Greenland = NonEuropean("GL"),
                Grenada = NonEuropean("GD"),
                Guadeloupe = NonEuropean("GP"),
                Guam = NonEuropean("GU"),
                Guatemala = NonEuropean("GT"),
                Guernsey = NonEuropean("GG"),
                Guinea = NonEuropean("GN"),
                GuineaBissau = NonEuropean("GW"),
                Guyana = NonEuropean("GY"),
                Haiti = NonEuropean("HT"),
                HeardIslandAndMcDonaldIslands = NonEuropean("HM"),
                HolySee = NonEuropean("VA"),
                Honduras = NonEuropean("HN"),
                HongKong = NonEuropean("HK"),
                Hungary = European("HU", taxpayerNumberPattern: "^(HU)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                Iceland = NonEuropean("IS"),
                India = NonEuropean("IN"),
                Indonesia = NonEuropean("ID"),
                Iran = NonEuropean("IR"),
                Iraq = NonEuropean("IQ"),
                Ireland = European("IE", taxpayerNumberPattern: "^(IE)?[0-9A-Z]{8}([0-9A-Z]{1})?$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9A-Z]{8}([0-9A-Z]{1})?$"),
                IsleOfMan = NonEuropean("IM"),
                Israel = NonEuropean("IL"),
                Italy = European("IT", taxpayerNumberPattern: "^(IT)?[0-9]{11}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{11}$"),
                Jamaica = NonEuropean("JM"),
                Japan = NonEuropean("JP"),
                Jersey = NonEuropean("JE"),
                Jordan = NonEuropean("JO"),
                Kazakhstan = NonEuropean("KZ"),
                Kenya = NonEuropean("KE"),
                Kiribati = NonEuropean("KI"),
                KoreaDemocraticPeoplesRepublic = NonEuropean("KP"),
                KoreaRepublic = NonEuropean("KR"),
                Kosovo = NonEuropean("XK"),
                Kuwait = NonEuropean("KW"),
                Kyrgyzstan = NonEuropean("KG"),
                LaoPeoplesDemocraticRepublic = NonEuropean("LA"),
                Latvia = European("LV", taxpayerNumberPattern: "^(LV)?[0-9]{11}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{11}$"),
                Lebanon = NonEuropean("LB"),
                Lesotho = NonEuropean("LS"),
                Liberia = NonEuropean("LR"),
                Libya = NonEuropean("LY"),
                Liechtenstein = NonEuropean("LI"),
                Lithuania = European("LT", taxpayerNumberPattern: "^(LT)?([0-9]{9}|[0-9]{12})$", taxpayerNumberPatternWithoutCountryCodePrefix: "^([0-9]{9}|[0-9]{12})$"),
                Luxembourg = European("LU", taxpayerNumberPattern: "^(LU)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                Macao = NonEuropean("MO"),
                Macedonia = NonEuropean("MK"),
                Madagascar = NonEuropean("MG"),
                Malawi = NonEuropean("MW"),
                Malaysia = NonEuropean("MY"),
                Maldives = NonEuropean("MV"),
                Mali = NonEuropean("ML"),
                Malta = European("MT", taxpayerNumberPattern: "^(MT)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                MarshallIslands = NonEuropean("MH"),
                Martinique = NonEuropean("MQ"),
                Mauritania = NonEuropean("MR"),
                Mauritius = NonEuropean("MU"),
                Mayotte = NonEuropean("YT"),
                Mexico = NonEuropean("MX"),
                Micronesia = NonEuropean("FM"),
                Moldova = NonEuropean("MD"),
                Monaco = NonEuropean("MC"),
                Mongolia = NonEuropean("MN"),
                Montenegro = NonEuropean("ME"),
                Montserrat = NonEuropean("MS"),
                Morocco = NonEuropean("MA"),
                Mozambique = NonEuropean("MZ"),
                Myanmar = NonEuropean("MM"),
                Namibia = NonEuropean("NA"),
                Nauru = NonEuropean("NR"),
                Nepal = NonEuropean("NP"),
                Netherlands = European("NL", taxpayerNumberPattern: "^(NL)?[0-9]{9}B[0-9]{2}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9}B[0-9]{2}$"),
                NewCaledonia = NonEuropean("NC"),
                NewZealand = NonEuropean("NZ"),
                Nicaragua = NonEuropean("NI"),
                Niger = NonEuropean("NE"),
                Nigeria = NonEuropean("NG"),
                Niue = NonEuropean("NU"),
                NorfolkIsland = NonEuropean("NF"),
                NorthernMarianaIslands = NonEuropean("MP"),
                Norway = NonEuropean("NO"),
                Oman = NonEuropean("OM"),
                Pakistan = NonEuropean("PK"),
                Palau = NonEuropean("PW"),
                Palestine = NonEuropean("PS"),
                Panama = NonEuropean("PA"),
                PapuaNewGuinea = NonEuropean("PG"),
                Paraguay = NonEuropean("PY"),
                Peru = NonEuropean("PE"),
                Philippines = NonEuropean("PH"),
                Pitcairn = NonEuropean("PN"),
                Poland = European("PL", taxpayerNumberPattern: "^(PL)?[0-9]{10}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{10}$"),
                Portugal = European("PT", taxpayerNumberPattern: "^(PT)?[0-9]{9}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{9}$"),
                PuertoRico = NonEuropean("PR"),
                Qatar = NonEuropean("QA"),
                Romania = European("RO", taxpayerNumberPattern: "^(RO)?[0-9]{2,10}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{2,10}$"),
                Runion = NonEuropean("RE"),
                RussianFederation = NonEuropean("RU"),
                Rwanda = NonEuropean("RW"),
                SaintBarthélemy = NonEuropean("BL"),
                SaintHelena = NonEuropean("SH"),
                SaintKittsAndNevis = NonEuropean("KN"),
                SaintLucia = NonEuropean("LC"),
                SaintMartin = NonEuropean("MF"),
                SaintPierreAndMiquelon = NonEuropean("PM"),
                SaintVincentAndTheGrenadines = NonEuropean("VC"),
                Samoa = NonEuropean("WS"),
                SanMarino = NonEuropean("SM"),
                SaoTomeAndPrincipe = NonEuropean("ST"),
                SaudiArabia = NonEuropean("SA"),
                Senegal = NonEuropean("SN"),
                Serbia = NonEuropean("RS"),
                Seychelles = NonEuropean("SC"),
                SierraLeone = NonEuropean("SL"),
                Singapore = NonEuropean("SG"),
                SintMaarten = NonEuropean("SX"),
                Slovakia = European("SK", taxpayerNumberPattern: "^(SK)?[0-9]{10}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{10}$"),
                Slovenia = European("SI", taxpayerNumberPattern: "^(SI)?[0-9]{8}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{8}$"),
                SolomonIslands = NonEuropean("SB"),
                Somalia = NonEuropean("SO"),
                SouthAfrica = NonEuropean("ZA"),
                SouthGeorgia = NonEuropean("GS"),
                SouthSudan = NonEuropean("SS"),
                Spain = European("ES", taxpayerNumberPattern: "^(ES)?(([a-z|A-Z]{1}\\d{7}[a-z|A-Z]{1})|(\\d{8}[a-z|A-Z]{1})|([a-z|A-Z]{1}\\d{8}))$", taxpayerNumberPatternWithoutCountryCodePrefix: "^(([a-z|A-Z]{1}\\d{7}[a-z|A-Z]{1})|(\\d{8}[a-z|A-Z]{1})|([a-z|A-Z]{1}\\d{8}))$"),
                SriLanka = NonEuropean("LK"),
                Sudan = NonEuropean("SD"),
                Suriname = NonEuropean("SR"),
                SvalbardAndJanMayen = NonEuropean("SJ"),
                Swaziland = NonEuropean("SZ"),
                Sweden = European("SE", taxpayerNumberPattern: "^(SE)?[0-9]{12}$", taxpayerNumberPatternWithoutCountryCodePrefix: "^[0-9]{12}$"),
                Switzerland = NonEuropean("CH"),
                SyrianArabRepublic = NonEuropean("SY"),
                Taiwan = NonEuropean("TW"),
                Tajikistan = NonEuropean("TJ"),
                Tanzania = NonEuropean("TZ"),
                Thailand = NonEuropean("TH"),
                TimorLeste = NonEuropean("TL"),
                Togo = NonEuropean("TG"),
                Tokelau = NonEuropean("TK"),
                Tonga = NonEuropean("TO"),
                TrinidadAndTobago = NonEuropean("TT"),
                Tunisia = NonEuropean("TN"),
                Turkey = NonEuropean("TR"),
                Turkmenistan = NonEuropean("TM"),
                TurksAndCaicosIslands = NonEuropean("TC"),
                Tuvalu = NonEuropean("TV"),
                Uganda = NonEuropean("UG"),
                Ukraine = NonEuropean("UA"),
                UnitedArabEmirates = NonEuropean("AE"),
                UnitedKingdom = European("GB", taxpayerNumberPattern: "^(GB)?([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})$", taxpayerNumberPatternWithoutCountryCodePrefix: "^([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})$"),
                UnitedStatesMinorOutlyingIslands = NonEuropean("UM"),
                UnitedStatesOfAmerica = NonEuropean("US"),
                Uruguay = NonEuropean("UY"),
                Uzbekistan = NonEuropean("UZ"),
                Vanuatu = NonEuropean("VU"),
                Venezuela = NonEuropean("VE"),
                Vietnam = NonEuropean("VN"),
                VirginIslandsBritish = NonEuropean("VG"),
                VirginIslandsUS = NonEuropean("VI"),
                WallisAndFutuna = NonEuropean("WF"),
                WesternSahara = NonEuropean("EH"),
                Yemen = NonEuropean("YE"),
                Zambia = NonEuropean("ZM"),
                Zimbabwe = NonEuropean("ZW")
            ).ToList();

            AllByCodes = All.ToDictionary(c => c.Alpha2Code);
            EuropeanUnionByCodes = All.Select(c => c.First).Flatten().ToDictionary(c => c.Alpha2Code);
        }

        public static Country AalandIslands { get; }
        public static Country Afghanistan { get; }
        public static Country Albania { get; }
        public static Country Algeria { get; }
        public static Country AmericanSamoa { get; }
        public static Country Andorra { get; }
        public static Country Angola { get; }
        public static Country Anguilla { get; }
        public static Country Antarctica { get; }
        public static Country AntiguaAndBarbuda { get; }
        public static Country Argentina { get; }
        public static Country Armenia { get; }
        public static Country Aruba { get; }
        public static Country Australia { get; }
        public static Country Austria { get; }
        public static Country Azerbaijan { get; }
        public static Country Bahamas { get; }
        public static Country Bahrain { get; }
        public static Country Bangladesh { get; }
        public static Country Barbados { get; }
        public static Country Belarus { get; }
        public static Country Belgium { get; }
        public static Country Belize { get; }
        public static Country Benin { get; }
        public static Country Bermuda { get; }
        public static Country Bhutan { get; }
        public static Country Bolivia { get; }
        public static Country BonaireSintEustatiusAndSaba { get; }
        public static Country BosniaAndHerzegovina { get; }
        public static Country Botswana { get; }
        public static Country BouvetIsland { get; }
        public static Country Brazil { get; }
        public static Country BritishIndianOceanTerritory { get; }
        public static Country BruneiDarussalam { get; }
        public static Country Bulgaria { get; }
        public static Country BurkinaFaso { get; }
        public static Country Burundi { get; }
        public static Country CaboVerde { get; }
        public static Country Cambodia { get; }
        public static Country Cameroon { get; }
        public static Country Canada { get; }
        public static Country CaymanIslands { get; }
        public static Country CentralAfricanRepublic { get; }
        public static Country Chad { get; }
        public static Country Chile { get; }
        public static Country China { get; }
        public static Country ChristmasIsland { get; }
        public static Country CocosIslands { get; }
        public static Country Colombia { get; }
        public static Country Comoros { get; }
        public static Country Congo { get; }
        public static Country CongoDemocraticRepublic { get; }
        public static Country CookIslands { get; }
        public static Country CostaRica { get; }
        public static Country CoteIvoire { get; }
        public static Country Croatia { get; }
        public static Country Cuba { get; }
        public static Country Curacao { get; }
        public static Country Cyprus { get; }
        public static Country CzechRepublic { get; }
        public static Country Denmark { get; }
        public static Country Djibouti { get; }
        public static Country Dominica { get; }
        public static Country DominicanRepublic { get; }
        public static Country Ecuador { get; }
        public static Country Egypt { get; }
        public static Country ElSalvador { get; }
        public static Country EquatorialGuinea { get; }
        public static Country Eritrea { get; }
        public static Country Estonia { get; }
        public static Country Ethiopia { get; }
        public static Country FalklandIslands { get; }
        public static Country FaroeIslands { get; }
        public static Country Fiji { get; }
        public static Country Finland { get; }
        public static Country France { get; }
        public static Country FrenchGuiana { get; }
        public static Country FrenchPolynesia { get; }
        public static Country FrenchSouthernTerritories { get; }
        public static Country Gabon { get; }
        public static Country Gambia { get; }
        public static Country Georgia { get; }
        public static Country Germany { get; }
        public static Country Ghana { get; }
        public static Country Gibraltar { get; }
        public static Country Greece { get; }
        public static Country Greenland { get; }
        public static Country Grenada { get; }
        public static Country Guadeloupe { get; }
        public static Country Guam { get; }
        public static Country Guatemala { get; }
        public static Country Guernsey { get; }
        public static Country Guinea { get; }
        public static Country GuineaBissau { get; }
        public static Country Guyana { get; }
        public static Country Haiti { get; }
        public static Country HeardIslandAndMcDonaldIslands { get; }
        public static Country HolySee { get; }
        public static Country Honduras { get; }
        public static Country HongKong { get; }
        public static Country Hungary { get; }
        public static Country Iceland { get; }
        public static Country India { get; }
        public static Country Indonesia { get; }
        public static Country Iran { get; }
        public static Country Iraq { get; }
        public static Country Ireland { get; }
        public static Country IsleOfMan { get; }
        public static Country Israel { get; }
        public static Country Italy { get; }
        public static Country Jamaica { get; }
        public static Country Japan { get; }
        public static Country Jersey { get; }
        public static Country Jordan { get; }
        public static Country Kazakhstan { get; }
        public static Country Kenya { get; }
        public static Country Kiribati { get; }
        public static Country KoreaDemocraticPeoplesRepublic { get; }
        public static Country KoreaRepublic { get; }
        public static Country Kosovo { get; }
        public static Country Kuwait { get; }
        public static Country Kyrgyzstan { get; }
        public static Country LaoPeoplesDemocraticRepublic { get; }
        public static Country Latvia { get; }
        public static Country Lebanon { get; }
        public static Country Lesotho { get; }
        public static Country Liberia { get; }
        public static Country Libya { get; }
        public static Country Liechtenstein { get; }
        public static Country Lithuania { get; }
        public static Country Luxembourg { get; }
        public static Country Macao { get; }
        public static Country Macedonia { get; }
        public static Country Madagascar { get; }
        public static Country Malawi { get; }
        public static Country Malaysia { get; }
        public static Country Maldives { get; }
        public static Country Mali { get; }
        public static Country Malta { get; }
        public static Country MarshallIslands { get; }
        public static Country Martinique { get; }
        public static Country Mauritania { get; }
        public static Country Mauritius { get; }
        public static Country Mayotte { get; }
        public static Country Mexico { get; }
        public static Country Micronesia { get; }
        public static Country Moldova { get; }
        public static Country Monaco { get; }
        public static Country Mongolia { get; }
        public static Country Montenegro { get; }
        public static Country Montserrat { get; }
        public static Country Morocco { get; }
        public static Country Mozambique { get; }
        public static Country Myanmar { get; }
        public static Country Namibia { get; }
        public static Country Nauru { get; }
        public static Country Nepal { get; }
        public static Country Netherlands { get; }
        public static Country NewCaledonia { get; }
        public static Country NewZealand { get; }
        public static Country Nicaragua { get; }
        public static Country Niger { get; }
        public static Country Nigeria { get; }
        public static Country Niue { get; }
        public static Country NorfolkIsland { get; }
        public static Country NorthernMarianaIslands { get; }
        public static Country Norway { get; }
        public static Country Oman { get; }
        public static Country Pakistan { get; }
        public static Country Palau { get; }
        public static Country Palestine { get; }
        public static Country Panama { get; }
        public static Country PapuaNewGuinea { get; }
        public static Country Paraguay { get; }
        public static Country Peru { get; }
        public static Country Philippines { get; }
        public static Country Pitcairn { get; }
        public static Country Poland { get; }
        public static Country Portugal { get; }
        public static Country PuertoRico { get; }
        public static Country Qatar { get; }
        public static Country Romania { get; }
        public static Country Runion { get; }
        public static Country RussianFederation { get; }
        public static Country Rwanda { get; }
        public static Country SaintBarthélemy { get; }
        public static Country SaintHelena { get; }
        public static Country SaintKittsAndNevis { get; }
        public static Country SaintLucia { get; }
        public static Country SaintMartin { get; }
        public static Country SaintPierreAndMiquelon { get; }
        public static Country SaintVincentAndTheGrenadines { get; }
        public static Country Samoa { get; }
        public static Country SanMarino { get; }
        public static Country SaoTomeAndPrincipe { get; }
        public static Country SaudiArabia { get; }
        public static Country Senegal { get; }
        public static Country Serbia { get; }
        public static Country Seychelles { get; }
        public static Country SierraLeone { get; }
        public static Country Singapore { get; }
        public static Country SintMaarten { get; }
        public static Country Slovakia { get; }
        public static Country Slovenia { get; }
        public static Country SolomonIslands { get; }
        public static Country Somalia { get; }
        public static Country SouthAfrica { get; }
        public static Country SouthGeorgia { get; }
        public static Country SouthSudan { get; }
        public static Country Spain { get; }
        public static Country SriLanka { get; }
        public static Country Sudan { get; }
        public static Country Suriname { get; }
        public static Country SvalbardAndJanMayen { get; }
        public static Country Swaziland { get; }
        public static Country Sweden { get; }
        public static Country Switzerland { get; }
        public static Country SyrianArabRepublic { get; }
        public static Country Taiwan { get; }
        public static Country Tajikistan { get; }
        public static Country Tanzania { get; }
        public static Country Thailand { get; }
        public static Country TimorLeste { get; }
        public static Country Togo { get; }
        public static Country Tokelau { get; }
        public static Country Tonga { get; }
        public static Country TrinidadAndTobago { get; }
        public static Country Tunisia { get; }
        public static Country Turkey { get; }
        public static Country Turkmenistan { get; }
        public static Country TurksAndCaicosIslands { get; }
        public static Country Tuvalu { get; }
        public static Country Uganda { get; }
        public static Country Ukraine { get; }
        public static Country UnitedArabEmirates { get; }
        public static Country UnitedKingdom { get; }
        public static Country UnitedStatesMinorOutlyingIslands { get; }
        public static Country UnitedStatesOfAmerica { get; }
        public static Country Uruguay { get; }
        public static Country Uzbekistan { get; }
        public static Country Vanuatu { get; }
        public static Country Venezuela { get; }
        public static Country Vietnam { get; }
        public static Country VirginIslandsBritish { get; }
        public static Country VirginIslandsUS { get; }
        public static Country WallisAndFutuna { get; }
        public static Country WesternSahara { get; }
        public static Country Yemen { get; }
        public static Country Zambia { get; }
        public static Country Zimbabwe { get; }

        public static IReadOnlyList<Country> All { get; }

        public static IReadOnlyDictionary<string, Country> AllByCodes { get; }

        public static IReadOnlyDictionary<string, EuropeanUnionCountry> EuropeanUnionByCodes { get; }

        public static IOption<Country> GetByCode(string code)
        {
            return AllByCodes.Get(code);
        }

        public static IOption<EuropeanUnionCountry> GetEuropeanUnionByCode(string code)
        {
            return EuropeanUnionByCodes.Get(code);
        }

        private static Country European(string alpha2Code, string taxpayerNumberPattern, string taxpayerNumberPatternWithoutCountryCodePrefix)
        {
            return new Country(new EuropeanUnionCountry(alpha2Code, new Regex(taxpayerNumberPattern), new Regex(taxpayerNumberPatternWithoutCountryCodePrefix)));
        }

        private static Country NonEuropean(string alpha2Code)
        {
            return new Country(new NonEuropeanUnionCountry(alpha2Code));
        }
    }
}
