using System;
using System.Collections.Generic;
using Infragistics.Documents.Parsing;
using Infragistics.Documents.Tagging;

namespace IGSyntaxParsingEngine.Samples.Languages
{
    #region CustomXMLLanguage
    /// <summary>
    /// CustomXMLLanguage class
    /// </summary>
    public sealed partial class CustomXMLLanguage : global::Infragistics.Documents.Parsing.LanguageBase
    {
        #region Static Variables
        private static global::Infragistics.Documents.Parsing.Grammar _grammarInstance;
        private static CustomXMLLanguage _instance;
        private static object _syncLock = new object();
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="CustomXMLLanguage"/> instance.
        /// </summary>
        public CustomXMLLanguage()
            : base(CustomXMLLanguage.GrammarInstance)
        {
        }
        #endregion

        #region Properties

        #region Instance
        /// <summary>
        /// Returns a static instance of the language (read-only)
        /// </summary>
        public static CustomXMLLanguage Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomXMLLanguage();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region GrammarInstance
        private static global::Infragistics.Documents.Parsing.Grammar GrammarInstance
        {
            get
            {
                if (_grammarInstance == null)
                {
                    lock (_syncLock)
                    {
                        if (_grammarInstance == null)
                        {
                            var grammar = new global::Infragistics.Documents.Parsing.Grammar();
                            CustomXMLLanguage.InitializeGrammarProperties(grammar);
                            CustomXMLLanguage.CreateLexerStates(grammar);
                            CustomXMLLanguage.CreateNonTerminalSymbols(grammar);
                            CustomXMLLanguage.CreateProductions(grammar);
                            CustomXMLLanguage.CreateSyntaxRules(grammar);
                            grammar.StartSymbol = grammar.NonTerminalSymbols["RootContent"];
                            _grammarInstance = grammar;
                        }
                    }
                }

                return _grammarInstance;
            }
        }
        #endregion

        #endregion

        #region Methods

        #region CreateLexerStates
        /// <summary>
        /// Creates the LexerStates for the associated Grammar.
        /// </summary>
        private static void CreateLexerStates(global::Infragistics.Documents.Parsing.Grammar grammar)
        {
            var lexerStates = grammar.LexerStates;
            var defaultState = lexerStates.DefaultLexerState;

            defaultState.Symbols.Add(grammar.NewLineSymbol, false);
            defaultState.Symbols.Add(grammar.WhitespaceSymbol, false);
            var t = defaultState.Symbols.Add("TAG_OPEN", "<", 2818, false, 25);
            long[][] l;
            l = new long[3][];
            l[0] = new long[] { 0x2200200000000200, 0xFFFF00 };
            l[1] = new long[] { 0 };
            l[2] = new long[] { 0x500000000030000, 0x1010001000002, 0x200000004000000, 67108864 };
            t.SetLookaheadPatternData("[^!]", l);
            t = defaultState.Symbols.Add("CDATA_DEC_OPEN", "<![CDATA[", 1024, false, 28);
            l = new long[3][];
            l[0] = new long[] { 0xB00090000000200, 0xFFFF00 };
            l[1] = new long[] { 0 };
            l[2] = new long[] { 0x500000000030000, 0x1010001000002, 0x200000004000000, 67108864 };
            t.SetLookaheadPatternData(".", l);
            defaultState.Symbols.Add("COMMENT_START", "<!--", 256, false, 31);
            defaultState.Symbols.Add("TAG_VALUE_CONTENT", "[^\\<&\\n\\r]+", 2, false, 32);
            var s0 = new long[3][];
            s0[0] = new long[] { 0xE00090000000800, 0xE00080000002500, 0xB00250021001F00, 0x3D003B0027000C00, 0xFFFF00 };
            s0[1] = new long[] { 0 };
            s0[2] = new long[] { 0x700000000110000, 0xA000300090005, 0x200005000D0004, 0x60006003C0003, 0x103000105000102, 0x700010600010400, 0x101000000000001, 0x500000505000100, 0x20010000070601, 0x201000000000400, 0x502000605000100, 0x400030007060403, 0x2000090002, 0x400000101, 0x4000A0100000201, 0x2101000002010000, 0x190100000700, 0x5B0008002D000204, 0x2D01000000000A00, 0x4000000000900, 0x430100001F010000, 0x4401000000000B00, 0x4101000000000C00, 0x5401000000000D00, 0x4101000000000E00, 0x5B01000000000F00, 0x4000000001000, 469827584 };
            defaultState.SetLexerStateData(s0);
            var ls0 = lexerStates.Add("Tag");
            ls0.Symbols.Add("LITERAL_OPEN", "\"", 1792, false, 16);
            t = ls0.Symbols.Add("XML_DEC", "xml", 1025, false, 17);
            l = new long[3][];
            l[0] = new long[]{0x3A002F000000D700, 0x7B005E005B004000, -5332064043844720384, -4467360820493240064, -1944746885864307454, 0x7E037902EF02ED02, 0x1404890482038503, 0x5A05580557053005, -1511574477249093627, 0x3B062005F305EF05, 0x6A065F064B063F06, -1799500283076252410, 0x6FE06FD06ED06, 0x6E074C0730070F07, -5618328463224635641, -718338400208830713, 0x3A090307FB07F907, 0x51094F093E093C09, 0x7009650962095709, -8283944863233639927, -5545740896344371703, -4753061181750921975, -2158953711172727543, 0xB0A0409F209E509, 0x3A0A120A110A0E0A, 0x700A650A5F0A580A, -5041071652439690998, -3383664524970509302, -1149854919934943478, 0x110B0E0B0D0B040A, 0x3E0B3C0B3A0B120B, 0x720B650B620B5B0B, -7634853662559600117, -6553966661894170613, -5040745092285225205, 0x3A0C040BF00BE50B, 0x700C650C620C5F0C, -4752216743935704052, -1149291961391457012, 0x620D5F0D3A0D040C, -7562243006035827443, -4103413619720611571, 0x470E3F0E340E000D, -8859002617862402290, -8210471076559419890, -5472250616794410226, -4103132140448859122, -2445776690781171954, 0x2A0F1F0F010EFF0E, -8354310283458560241, 0x4A103F102B0FFF0F, -4174661962060247280, 0x5A10FF10FD10CF10, -427376795431379439, 0x5E124F124E11FF11, -5327038099370123502, 0x1612C712C612B712, -8064962786581342445, 0x6D140013F5139F13, -7271483714367492586, 0x1216FF16EB169F16, 0x52173F1732171F17, -5469763483759845609, -2515300971406961129, 0x1A180F17EA17DF17, -6262115538787885288, 0x6E1945191D18FF18, -6189776465528525031, -2730923989385232359, 0x341B041A1719FF19, 0x5A1B4F1B4C1B441B, -7197316126225924325, 0x161EFF1EFA1E9F1E, 0x461F1F1F1E1F171F, 0x7E1F4F1F4E1F471F, -3665999229561635041, -2585113328407818465, -207181938411184353, 0x5520532041203E1F, -9214226159781253088, 0x321012095208F20, 0x1621092108210621, 0x3A2123211E211821, 0x4A21442140213B21, -8853652312860570335, 0x782C732C6D2BFF21, 0x262CFF2CE52C7F2C, 0x702D6E2D662D2F2D, -2365059211140956371, 0x363030300730042D, -7552465999171143120, 0x2D31043100309C30, -5174179461667409871, -5317626144777703631, -8241587731540607155, 0x23A7FFA71BA716A4, -6580885331495075928, 0x6BFA2FFA2EF8FFD7, 0x7FAFFFADAFA6FFA, -5549810251022527749,
            -7999149414038121733, -216753333766090243, 0x50FE4CFE35FE32FD, 0x1AFF0FFEFDFE6FFE, 0x5BFF3EFF3BFF20FF, -3963235842880739841, -2810296745802610177, 0xFFFFFFDDFFD9FF};
            l[1] = new long[] { 0x6D070000000100, 0x38700F700D70060, 0x3CF03A2038D038B, 0x71106D4067003F6, 0xA2909DE09B109A9, 0xA5D0A370A340A31, 0xAB10AA90A920A8E, 0xB340B310B290AB4, 0xB910B840B700B5E, 0xC110C0D0B9D0B9B, 0xC910C8D0C340C29, 0xD0D0CDF0CB40CA9, 0xDBC0DB20D290D11, 0xE980E890E830E31, 0xEAC0EA60EA40EA0, 0x10220F480EC50EB1, 0x1257124910FB1028, 0x12BF12B112891259, 0x170D131112D712C1, 0x1F5C1F5A1F58176D, 0x1FC51FBD1FB51F5E, 0x2127212521141FF5, 0x2C5F2C2F212E2129, 0x2DBF2DB72DAF2DA7, 0x30A02DD72DCF2DC7, -6337787304051855109, -342841778754684130, -110906337813923009, 0x302010000D7FF40, 0xB0A090807060504, 0x131211100F0E0D0C, 0x1B1A191817161514, 0x232221201F1E1D1C, 0x2B2A292827262524, 0x333231302F2E2D2C, 0x3B3A393837363534, 0x434241403F3E3D3C, 0x4B4A494847464544, 0x535251504F4E4D4C, 0x5B5A595857565554, 0x636261605F5E5D5C, 0x6B6A696867666564, 0x737271706F6E6D6C, 0x7B7A797877767574, -8970465118873813636, -8391743736169200252, -7813022353464586868, -7234300970759973484, -6655579588055360100, -6076858205350746716, -5498136822646133332, -4919415439941519948, -4340694057236906564, -3761972674532293180, -3183251291827679796, 0xD6D5D4 };
            l[2] = new long[] { 0x101000000030000, 256, 0x40000000101, 0x40000000200, 0 };
            t.SetLookaheadPatternData("(\\W|\\z)", l);
            t = ls0.Symbols.Add("IdentifierToken", "[a-zA-Z_\\p{L}][a-zA-Z0-9_\\-:\\p{L}]*", 1538, false, 18);
            t.SetLookaheadPatternFrom(grammar.TerminalSymbolFromName("XML_DEC"));
            ls0.Symbols.Add("EqualsToken", "=", 2560, false, 19);
            ls0.Symbols.Add("SLASH", "/", 2816, false, 20);
            ls0.Symbols.Add("ColonToken", ":", 2816, false, 21);
            ls0.Symbols.Add("DotToken", ".", 2816, false, 22);
            ls0.Symbols.Add("QuestionToken", "?", 2816, false, 23);
            ls0.Symbols.Add(grammar.WhitespaceSymbol, false);
            ls0.Symbols.Add("TAG_CLOSE", ">", 2816, true, 24);
            ls0.Symbols.Add(grammar.NewLineSymbol, false);
            var s1 = new long[3][];
            s1[0] = new long[]{0x5900570041010000, 0x7900770061005A00, 0x4E004C0041007A00, 0x6E006C0061005A00, 0x4D004B0041007A00, 0x6D006B0061005A00, 0x61005A0041007A00, -4611622245947639296, -576190268819057152, -2305050249418063616, -8645929405735836670, -6700334783957595645, -647404524687602173, 0x310513048A048103, -3457208681764661755, 0x2105F205F005EA05, 0x6E064A0640063A06, -1943634164544278778, -430393901325031930, 0x4D072F071206FC06, -3888958301579809529, 0x407F507F407EA07, 0x7B09610958093909, -8139820878980874487, -6194235053396357111, -2591336659215077367, -1150140797303530231, 0xF0A0A0A0509F109, 0x2A0A280A130A100A, 0x350A330A320A300A, 0x590A390A380A360A, -8860141728686253046, -7851303525831504630, -5617484025342285814, -2302824803808398582, 0xF0B0C0B050AE10A, 0x2A0B280B130B100B, 0x350B330B320B300B, 0x5F0B5D0B5C0B390B, -8211317713498578677, -7418672084235350005, -6698085148538463733, -5905439519325576181, 0xE0C0C0C050BB90B, 0x2A0C280C120C100C, 0x600C390C350C330C, -8211034035203579636, -6193390615597903860, -2302261845264911604, 0xE0D0C0D050CE10C, 0x2A0D280D120D100D, -8859318181893949171, -5544580894245939699, 0x10DC60DC00DBB0D, 0x400E330E320E300E, -8714885230108981746, -7417825447363311602, -6192833154360893682, -5616358108204979442, -2589917168060681458, 0x490F470F400EDD0E, 0xF8B0F880F6A0F, 0x2910271023102110, -6912931899462637040, 0x10FA10D010C510, -6336105005240461039, 0x4A1248120011F911, 0x5A12561250124D12, -8497579934392099566, -5615232191604552430, -4462295293163817710, -2877001835781896942, 0x1813151312131012, -6912023689167676909, 0x6F166C140113F413, -6911167156707756522, 0xE170C170016EA16, 0x4017311720171117, 0x6E176C1760175117, 0x2017B31780177017, 0x18A81880187718, 0x70196D1950191C19, -4532405622983265255, 0x51A161A0019C719, 0x1B4B1B451B331B, -6908914225186488547, 0x181F151F001EF91E, 0x481F451F201F1D1F, 0x5F1F571F501F4D1F, -5323338187142103777, -4170399089241900001, -3017461091725751265, -999820970604635361, -8061447593766423521, 0x192113210A209420, 0x2F212D212A211D21, 0x45213F213C213921, 0x21842183214921, 0x602C5E2C302C2E2C, -9210856105068172244,
            0x302D252D002CE42C, -6904697531520948947, -5751749637963930067, -4598810540902599123, -3445871443841268179, 0x52DDE2DD82DD62D, 0x3B30353031300630, -7120025876876477392, -274444490499907792, 0x31312C310530FF30, -1138927806979142095, 0x4DB5340031FF31, 0x17A48CA0009FBB4E, 0x3A801A800A71AA7, 0xCA80AA807A805A8, 0xA873A840A822A8, 0x30FA2DF900D7A3AC, 0xFAD9FA70FA6AFA, 0x1FFB17FB13FB06FB, 0x38FB36FB2AFB28FB, 0x43FB41FB40FB3CFB, -3171745819766602501, -7854963856916005381, 0x70FDFBFDF0FDC7FD, 0x21FEFCFE76FE74FE, 0x66FF5AFF41FF3AFF, -3819114057684762881, -2666174960623431681, 0xFFDCFF};
            s1[1] = new long[]{0x39070000000400, 0xBA00B500AA005F, 0x559038C038602EE, 0x7B1071006FF06D5, 0x9B20950093D07FA, 0xABD0A5E09CE09BD, 0xB830B710B3D0AD0, 0xDBD0CDE0CBD0B9C, 0xEA50E8D0E8A0E84, 0xF000EC60EBD0EA7, 0x17D712C0125810FC, 0x1F5D1F5B1F5917DC, 0x2102207F20711FBE, 0x2126212421152107, -352075240541642456, 0x302010000F5FB3E, 0x161514131211100F, 0x1E1D1C1B1A191817, 0x262524232221201F, 0x2E2D2C2B2A292827, 0x363534333231302F, 0x3E3D3C3B3A393837, 0x464544434241403F, 0x4E4D4C4B4A494847, 0x565554535251504F, 0x5E5D5C5B5A595857, 0x666564636261605F, 0x6E6D6C6B6A696867, 0x767574737271706F, 0x7E7D7C7B7A797877, -8753444600359583617, -8174723217654970233, -7596001834950356849, -7017280452245743465, -6438559069541130081, -5859837686836516697, -5281116304131903313, -4702394921427289929, -4123673538722676545, -3544952156018063161, -2966230773313449777, -2387509390608836393, -1808788007904223009, -1230066625199609625, -651345242494996241, -72623859790382857, 0x2D003A07000000FF, -5043832568198177024, 0x59038C038602EE00, -5690561981924846331, -5617871059559122425, -4824940654569603831, -9003978736914477046, -4824236954528015349, -6553145314192227315, 0xEC60EBD0EA70E, -2949083620954801137, 0x5D1F5B1F5917DC17, 0x2207F20711FBE1F, 0x2621242115210721, 0x1D2D6F214E212821, 0xD0C0E00F4FB3EFB, 0x161514131211100F, 0x1E1D1C1B1A191817, 0x262524232221201F, 0x2E2D2C2B2A292827, 0x363534333231302F, 0x3E3D3C3B3A393837, 0x464544434241403F, 0x4E4D4C4B4A494847, 0x565554535251504F, 0x5E5D5C5B5A595857, 0x666564636261605F, 0x6E6D6C6B6A696867, 0x767574737271706F, 0x7E7D7C7B7A797877, -8753444600359583617, -8174723217654970233, -7596001834950356849, -7017280452245743465, -6438559069541130081, -5859837686836516697, -5281116304131903313, -4702394921427289929, -4123673538722676545, -3544952156018063161, -2966230773313449777, -2387509390608836393, -1808788007904223009, -1230066625199609625, -651345242494996241, -72623859790382857, 0x2D003A07000000FF, -5043832568198177024, 0x59038C038602EE00, -5690561981924846331, -5617871059559122425, -4824940654569603831, -9003978736914477046, -4824236954528015349,
            -6553145314192227315, 0xEC60EBD0EA70E, -2949083620954801137, 0x5D1F5B1F5917DC17, 0x2207F20711FBE1F, 0x2621242115210721, 0x1D2D6F214E212821, 0x5040E00F6FB3EFB, 0x14131211100F0706, 0x1C1B1A1918171615, 0x24232221201F1E1D, 0x2C2B2A2928272625, 0x34333231302F2E2D, 0x3C3B3A3938373635, 0x44434241403F3E3D, 0x4C4B4A4948474645, 0x54535251504F4E4D, 0x5C5B5A5958575655, 0x64636261605F5E5D, 0x6C6B6A6968676665, 0x74737271706F6E6D, 0x7C7B7A7978777675, -8898124946035736963, -8319403563331123579, -7740682180626510195, -7161960797921896811, -6583239415217283427, -6004518032512670043, -5425796649808056659, -4847075267103443275, -4268353884398829891, -3689632501694216507, -3110911118989603123, -2532189736284989739, -1953468353580376355, -1374746970875762971, -796025588171149587, -217304205466536203, 0x3A07000000FFFEFD, -5404132634274026240, -8357689136328427008, 0x1006FF06D5055903, 0x50093D07FA07B107, 0x5E09CE09BD09B209, 0x710B3D0AD00ABD0A, -2446372640008731893, -8282530869738095348, -4175191933895924466, -4606522638875033586, 0x5B1F5917DC17D712, 0x7F20711FBE1F5D1F, 0x2421152107210220, 0x6F214E2128212621, 0xE00F6FB3EFB1D2D, 0x1211100F0B0A0908, 0x1A19181716151413, 0x2221201F1E1D1C1B, 0x2A29282726252423, 0x3231302F2E2D2C2B, 0x3A39383736353433, 0x4241403F3E3D3C3B, 0x4A49484746454443, 0x5251504F4E4D4C4B, 0x5A59585756555453, 0x6261605F5E5D5C5B, 0x6A69686766656463, 0x7271706F6E6D6C6B, 0x7A79787776757473, -9042805291711890309, -8464083909007276925, -7885362526302663541, -7306641143598050157, -6727919760893436773, -6149198378188823389, -5570476995484210005, -4991755612779596621, -4413034230074983237, -3834312847370369853, -3255591464665756469, -2676870081961143085, -2098148699256529701, -1519427316551916317, -940705933847302933, -361984551142689549, 0xFFFEFDFCFB};
            s1[2] = new long[] { 0xC01000000100000, 0x9000204000100, 0xA010003000020, 0x500000D01000400, 0x100060000220100, 0x2F01000700002E, 0x900003A01000800, 0x1000A00003D0100, 0x3F01000B00003E, 3072, 0x780058000204000D, 0x2040000000000, 0x1002000010009, 0x40000010100, 0x100000004000000, 0x3000A01000002, 0x40000020100, 0x40000100100, 0x40000160100, 0x40000140100, 0x40000150100, 0x40000130100, 0x40000180100, 0xC00010100170100, 0x100000000010000, 0xC0002010012, 0x204000E00000002, 0x10000006D004D00, 0xC0002010012, 0x204000F00000003, 0x10000006C004C00, 0xC0001010012, 0x2020000000001, 1179665 };
            ls0.SetLexerStateData(s1);
            var ls1 = lexerStates.Add("StringLiteral");
            ls1.Symbols.Add("ENTITY_START", "&", 2560, false, 13);
            ls1.Symbols.Add("LITERAL_CONTENT", "[^\\\"<>&\\n\\r]+", 1794, false, 14);
            ls1.Symbols.Add(grammar.WhitespaceSymbol, false);
            ls1.Symbols.Add("LITERAL_CLOSE", "\"", 1792, true, 15);
            ls1.Symbols.Add(grammar.NewLineSymbol, true);
            var s2 = new long[3][];
            s2[0] = new long[] { 0xE0021000E000700, 0xB00090000002000, 0x2700250023000C00, 0xFFFF003F003B00 };
            s2[1] = new long[] { 0 };
            s2[2] = new long[] { 0x700000000080000, 0xD0004000A0006, 0x22000200210005, 0x3D000700260006, 0x300010200060002, 0x2040001010002, 8690598405, 0x500010002010000, 0x700020001020002, 0x4003D00210002, 0x6050403, 0x2000101000E01, 0x20006003D000107, 0x605040003, 0x400000E0100, 0x100000004000000, 0x4000A01000002, 0x40000020100, 0x400000F0100, 852224 };
            ls1.SetLexerStateData(s2);
            var ls2 = lexerStates.Add("Entity_StringLiteral");
            ls2.Symbols.Add("ENTITY_AMP", "amp", 0, false, 6);
            ls2.Symbols.Add("ENTITY_QUOT", "quot", 0, false, 7);
            ls2.Symbols.Add("ENTITY_LT", "lt", 0, false, 8);
            ls2.Symbols.Add("ENTITY_GT", "gt", 0, false, 9);
            ls2.Symbols.Add("ENTITY_APOS", "apos", 0, false, 10);
            ls2.Symbols.Add("ENTITY_TEXT", "[^;\\r\\n]+", 2, false, 11);
            ls2.Symbols.Add(grammar.WhitespaceSymbol, false);
            ls2.Symbols.Add("ENTITY_END", ";", 0, true, 12);
            ls2.Symbols.Add(grammar.NewLineSymbol, true);
            var s3 = new long[3][];
            s3[0] = new long[] { 0x620060003C001700, 0x6D006B0068006600, 0x3CFFFF0072007000, 0x3C006F006E006C00, 0x740072003C006F00, 0x760074003CFFFF00, 0x210020000EFFFF00, 0x3CFFFF0071003A00, 0x3CFFFF0070006E00, 0x3CFFFF0075007300, 0x3A000EFFFF00, 0xC000B000900 };
            s3[1] = new long[] { 0 };
            s3[2] = new long[] { 0x700000000150000, 0xD0004000A0007, 0x610006003B0005, 0x6C000D00670007, 0x900110071000F, 0x10C000216000115, 0x100020000020D00, 0x2030002020002, 516, 0x216000115000505, 0x1300020D00010C00, 0x101000B01000002, 0x1615000405000200, 0xB0100001314, 0x4000000000004, 0xA01000002010000, 0x20100000400, 0x1000C0100000004, 0x1500060500020003, 0x8000E06051416, 0x7001000A00006D01, 0x201000B01000000, 0x1615000505000200, 0x70010009000E0714, 0x101000B01000000, 0x1615000405000200, 0x100060100001314, 0x1500050500020002, 0x1000B00100F1416, 0x1000B010000006F, 0x1500050500020002, 0x1000C0009081416, 0x1000B0100000073, 0x1500040500020001, 0xA010000131416, 0x5050002000201, 0xE001211141615, 0xB010000007401, 0x4050002000101, 0x901000013141615, 0x505000200020100, 0x1000121114161500, 0xB01000000740100, 0x405000200010100, 0x100001314161500, 0x500020002010008, 0xB0A1416150005, 0x100000075010012, 0x50002000201000B, 0x100F1416150005, 0x10000006F010013, 0x50002000201000B, 0x12111416150005, 0x100000074010014, 0x50002000101000B, 0x131416150004, 1793 };
            ls2.SetLexerStateData(s3);
            var ls3 = lexerStates.Add("CData");
            ls3.Symbols.Add(grammar.NewLineSymbol, false);
            ls3.Symbols.Add("CDATA_TEXT", "([^\\]])|(\\][^\\]])|(\\]\\][^>])+", 2, false, 26);
            ls3.Symbols.Add(grammar.WhitespaceSymbol, false);
            ls3.Symbols.Add("CDATA_DEC_CLOSE", "]]>", 0, true, 27);
            var s4 = new long[3][];
            s4[0] = new long[] { 0xB00090000000800, 0x210020000E000C00, 0x5E005C0000005C00, 0x3F003D0000FFFF00, 0xFFFF00 };
            s4[1] = new long[] { 0 };
            s4[2] = new long[] { 0x7000000000E0000, 0xD0004000A0003, 0x50008005D0006, 0x102000501000100, 0x50500050300, 0x900020400000000, 0x20020000200, 0x900020400001A01, 0x20020000200, 0x400000101, 0x201000000040000, 0x1A01000000040000, 0x7000A010000, 0x400000201, 0x5D00010700000201, 0x5040002000900, 1285, 0x2000D003E000107, 0xA07000A0600, 0xB005D0100000000, 0x5D0100001A010000, 0x205000000000C00, 0xA07000A0600, 0x1B01000000040000, 0 };
            ls3.SetLexerStateData(s4);
            var ls4 = lexerStates.Add("Comment");
            ls4.Symbols.Add("COMMENT_TEXT", "([^-]|-[^-]|--[^>])+", 258, false, 29);
            ls4.Symbols.Add(grammar.NewLineSymbol, false);
            ls4.Symbols.Add(grammar.WhitespaceSymbol, false);
            ls4.Symbols.Add("COMMENT_END", "-->", 256, true, 30);
            var s5 = new long[3][];
            s5[0] = new long[] { 0xE000D000B000B00, 0xB000A0000002000, 0x20000A002C00, 0xFFFF003F003D00, 0x2C0021000900, 0xFFFF002E002C00 };
            s5[1] = new long[] { 0 };
            s5[2] = new long[] { 0x7000000000C0000, 0x2D0007000A0002, 0x10700050009, 0x3080002010008, 778, 0x40004002D000107, 0x800020400020700, 50987011, 0x2D00010700001D01, 0x2070004000400, 0x30A000308000204, 0x1D0100000000, 0x20004002D000107, 0x30A00030900, 0x10700001D010000, 0x900020005002D00, 50987011, 0x3050002050000, 0x4000000000306, 0x2D00010700000000, 0x3090002000400, 0x1D0100000000030A, 0x4002D0001070000, 0x303000702000300, 0x100000000030A00, 0x2D00010700001D, 0xA0003090002000A, 0x700000000000003, 0x2000B003E0001, 0x306000305, 0x100000004000000, 30 };
            ls4.SetLexerStateData(s5);
            grammar.TerminalSymbolFromName("ENTITY_START").LexerStateToEnter = ls2;
            grammar.TerminalSymbolFromName("LITERAL_OPEN").LexerStateToEnter = ls1;
            grammar.TerminalSymbolFromName("TAG_OPEN").LexerStateToEnter = ls0;
            grammar.TerminalSymbolFromName("CDATA_DEC_OPEN").LexerStateToEnter = ls3;
            grammar.TerminalSymbolFromName("COMMENT_START").LexerStateToEnter = ls4;
        }
        #endregion

        #region CreateNonTerminalSymbols
        /// <summary>
        /// Creates the NonTerminalSymbols for the associated Grammar.
        /// </summary>
        private static void CreateNonTerminalSymbols(global::Infragistics.Documents.Parsing.Grammar grammar)
        {
            var col = grammar.NonTerminalSymbols;

            col.Add("RootContent", 33);
            col.Add("Content", 34);
            col.Add("CData", 36);
            col.Add("XmlDeclaration", 37);
            col.Add("_entity_head", 38);
            col.Add("EntityDoubleQuote", 39);
            col.Add("EntitySingleQuote", 40);
            col.Add("EntityLessThan", 41);
            col.Add("EntityGreaterThan", 42);
            col.Add("EntityAmpersand", 43);
            col.Add("EntityText", 44);
            col.Add("_entity", 45);
            col.Add("Tag", 46);
            col.Add("TagValue", 47);
            col.Add("_tag_value", 49);
            col.Add("_tag_head", 50);
            col.Add("_tag_content", 51);
            col.Add("_tag_no_content", 52);
            col.Add("_tag_with_content", 53);
            col.Add("ElementName", 55);
            col.Add("NamespacePrefix", 56);
            col.Add("_element_name", 57);
            col.Add("_qualified_identifier", 58);
            col.Add("AttributeList", 60);
            col.Add("AttributeDeclaration", 62);
            col.Add("AttributeName", 63);
            col.Add("AttributeValue", 64);
            col.Add("_string_literal", 65);
        }
        #endregion

        #region CreateProductions
        /// <summary>
        /// Creates the productions for the associated Grammar.
        /// </summary>
        private static void CreateProductions(global::Infragistics.Documents.Parsing.Grammar grammar)
        {
            grammar.AddProduction(0, 33, 34, 3);
            grammar.AddProduction(1, 34, 37, 35);
            grammar.AddProduction(2, 35);
            grammar.AddProduction(3, 35, 35, 46);
            grammar.AddProduction(4, 36, 28, 27);
            grammar.AddProduction(5, 36, 28, 26, 27);
            grammar.AddProduction(6, 37, 25, 23, 17, 23, 24);
            grammar.AddProduction(7, 37, 25, 23, 17, 60, 23, 24);
            grammar.AddProduction(8, 38, 13);
            grammar.AddProduction(9, 39, 38, 7, 12);
            grammar.AddProduction(10, 40, 38, 10, 12);
            grammar.AddProduction(11, 41, 38, 8, 12);
            grammar.AddProduction(12, 42, 38, 9, 12);
            grammar.AddProduction(13, 43, 38, 6, 12);
            grammar.AddProduction(14, 44, 38, 11, 12);
            grammar.AddProduction(15, 45, 43);
            grammar.AddProduction(16, 45, 39);
            grammar.AddProduction(17, 45, 40);
            grammar.AddProduction(18, 45, 41);
            grammar.AddProduction(19, 45, 42);
            grammar.AddProduction(20, 45, 44);
            grammar.AddProduction(21, 46, 52);
            grammar.AddProduction(22, 46, 53);
            grammar.AddProduction(23, 47, 48);
            grammar.AddProduction(24, 48);
            grammar.AddProduction(25, 48, 48, 49);
            grammar.AddProduction(26, 49, 32);
            grammar.AddProduction(27, 49, 45);
            grammar.AddProduction(28, 49, 36);
            grammar.AddProduction(29, 50, 25, 55);
            grammar.AddProduction(30, 50, 25, 55, 60);
            grammar.AddProduction(31, 51, 46);
            grammar.AddProduction(32, 51, 47);
            grammar.AddProduction(33, 52, 50, 20, 24);
            grammar.AddProduction(34, 53, 50, 24, 54, 25, 20, 55, 24);
            grammar.AddProduction(35, 54);
            grammar.AddProduction(36, 54, 54, 51);
            grammar.AddProduction(37, 55, 57);
            grammar.AddProduction(38, 56, 18, 21);
            grammar.AddProduction(39, 57, 58);
            grammar.AddProduction(40, 57, 56, 58);
            grammar.AddProduction(41, 58, 18, 59);
            grammar.AddProduction(42, 59);
            grammar.AddProduction(43, 59, 59, 22, 18);
            grammar.AddProduction(44, 60, 61);
            grammar.AddProduction(45, 61);
            grammar.AddProduction(46, 61, 61, 62);
            grammar.AddProduction(47, 62, 63, 19, 64);
            grammar.AddProduction(48, 63, 57);
            grammar.AddProduction(49, 64, 65);
            grammar.AddProduction(50, 65, 16, 15);
            grammar.AddProduction(51, 65, 16, 14, 15);
            grammar.AddProduction(52, 65, 16, 45, 15);
        }
        #endregion

        #region CreateSyntaxRules
        /// <summary>
        /// Creates the syntax rules for the associated Grammar.
        /// </summary>
        private static void CreateSyntaxRules(global::Infragistics.Documents.Parsing.Grammar grammar)
        {
            var col = grammar.NonTerminalSymbols;
            col[0].SetRuleData(0x3030022030500, -1);
            col[1].SetRuleData(0x2E03070025030500, -256);
            col[2].SetRuleData(0x1A0306001C030500, -4293197056);
            col[3].SetRuleData(0x17030019030500, 0x3003C0306001103, -1099108777961);
            col[4].SetRuleData(-4294114560);
            col[5].SetRuleData(0x7030026030500, -16774141);
            col[6].SetRuleData(0xA030026030500, -16774141);
            col[7].SetRuleData(0x8030026030500, -16774141);
            col[8].SetRuleData(0x9030026030500, -16774141);
            col[9].SetRuleData(0x6030026030500, -16774141);
            col[10].SetRuleData(0xB030026030500, -16774141);
            col[11].SetRuleData(0x2703002B030400, 0x2A03002903002803, -4292082944);
            col[12].SetRuleData(0x35030034030400, -1);
            col[13].SetRuleData(-1098689345792);
            col[14].SetRuleData(0x2D030020030400, -16767997);
            col[15].SetRuleData(0x37030019030500, -4291034362);
            col[16].SetRuleData(0x2F03002E030400, -1);
            col[17].SetRuleData(0x14030032030500, -16771069);
            col[18].SetRuleData(0x18030032030500, 0x300190300330307, 0x18030037030014, -1);
            col[19].SetRuleData(-4291230976);
            col[20].SetRuleData(0x15030012030500, -1);
            col[21].SetRuleData(0x3A03003803060500, -256);
            col[22].SetRuleData(0x305070012030500, -1099209441258);
            col[23].SetRuleData(-1098471241984);
            col[24].SetRuleData(0x1303003F030500, -16760829);
            col[25].SetRuleData(-4291230976);
            col[26].SetRuleData(-4290706688);
            col[27].SetRuleData(0x304060010030500, 0xF03FF002D03000E, -256);
        }
        #endregion

        #region GetParseTable
        /// <summary>
        /// Gets the parse table for a GLR parser.
        /// </summary>
        protected override global::Infragistics.Documents.Parsing.ParseTable GetParseTable()
        {
            var parseTable = this.Grammar.CachedParseTable;
            if (parseTable == null)
            {
                lock (_syncLock)
                {
                    parseTable = this.Grammar.CachedParseTable;
                    if (parseTable == null)
                    {
                        parseTable = this.GetParseTableHelper();
                        this.Grammar.CachedParseTable = parseTable;
                    }
                }
            }
            return parseTable;
        }
        #endregion

        #region GetParseTableHelper
        private global::Infragistics.Documents.Parsing.ParseTable GetParseTableHelper()
        {
            const long c0 = 0x20001C0019000F;
            const long c1 = 0x1020020001C0019;
            var s = new long[83][];
            s[0] = new long[] { 0x1900030300000000, 0x203002200010300, 4328531200, -16777216 };
            s[1] = new long[] { 0x102000300040300, -280375465082880 };
            s[2] = new long[] { 0x3000200020700, 0x200230005030019, -71776114766249983 };
            s[3] = new long[] { 0x202001700060300, 0x7010006000000, -255 };
            s[4] = new long[] { 0x102000300000600, -279275953455104 };
            s[5] = new long[] { 0xB03000300010600, 0x2E000703001900, 0x8030032000A03, 0x200350009030034, 0x302000100000002, -65280 };
            s[6] = new long[] { 0x2020011000C0300, 0x7020006000000, -254 };
            s[7] = new long[] { 0x3000200030700, 0x300000001020019, -65024 };
            s[8] = new long[] { 0x3000500150700, 0x20001C0019000D, 0x100150000000102 };
            s[9] = new long[] { 0x3000500160700, 0x20001C0019000D, 0x100160000000102 };
            s[10] = new long[] { 0xE030014000D0300, 8623495168, -280374894591967 };
            s[11] = new long[] { 0xF03001200130300, 0x38001203003700, 0x11030039001003, 0x1D0000000202003A, -1095214694144 };
            s[12] = new long[] { 0xA0012002D0600, 0x3C001503001700, 0x202003D001603, 0x300070300060000 };
            s[13] = new long[] { 0x102001800170300, -279275399806976 };
            s[14] = new long[] { 0xD000400230700, 0x18030020001C0019, 4328535552, -16646110 };
            s[15] = new long[] { 0x10B0012002D0600, 0x300180014000200, 0x3D001603003C0019, 0x1D000000020200, -4261405182 };
            s[16] = new long[] { 0x12000300250700, 0x10200180014, -4278180608 };
            s[17] = new long[] { 0x12000400270700, 0x102001800140013, -280374810771456 };
            s[18] = new long[] { 0x1A030012001B0300, 4328536576, -16711640 };
            s[19] = new long[] { 0x120005002A0700, 0x18001600140013, 0x1C030015001D03, 0x260000000202003B, -1095213973248 };
            s[20] = new long[] { 0x1020018001E0300, -277076829536256 };
            s[21] = new long[] { 0x1020017001F0300, -277076812759040 };
            s[22] = new long[] { 0x1203001200130300, 0x39002203003800, 0x2003003A001103, 0x7003F002103003E, 0x1700140003002C, 0x2C00000002020018, -1095213645568 };
            s[23] = new long[] { 0x3000500210700, 0x20001C0019000D, 0x300210000000102 };
            s[24] = new long[] { 0xD000300180700, 0x1900020A0020001C, 0x2603002E00250300, 0x30002703002F00, 0x24030032000A03, 0x300340008030033, 0x20200350009, -71213023062121472 };
            s[25] = new long[] { 0x140002001E0700, 0x1E00000001020018, -64768 };
            s[26] = new long[] { 0x12000400280700, 0x102001800140013, -279275282366464 };
            s[27] = new long[] { 0x120005002A0700, 0x18001600140013, 0x102003B001C03, -1095213973504 };
            s[28] = new long[] { 0x12000400290700, 0x2803001800140013, 8623494656, -280374743531479 };
            s[29] = new long[] { 0x102001200260600, -279275315920896 };
            s[30] = new long[] { 0x3000200060700, 0x600000001020019, -64256 };
            s[31] = new long[] { 0x102001800290300, -275977301131264 };
            s[32] = new long[] { 0x120004002E0700, 0x102001800170014, -279275181703168 };
            s[33] = new long[] { 0x1020013002A0300, -280374676553728 };
            s[34] = new long[] { 0x102001300300600, -280374659776512 };
            s[35] = new long[] { 0x2B03001200130300, 0x37000F03001400, 0x10030038001203, 0x2003A0011030039, 0x1E01001D00000003, -1082329530112 };
            s[36] = new long[] { 0xD000400240700, c1, -279275349475328 };
            s[37] = new long[] { 0xD0004001F0700, c1, -280374944989184 };
            s[38] = new long[] { 0xD000400200700, c1, -280374928211968 };
            s[39] = new long[] { 0x40A000D00030A00, 0x2000050A001C00, 0x2F030019001706, 0x300260037030024, 0x2800320300270031, 0x3403002900330300, 0x2B003003002A00, 0x2E03002C003503, 0x20031002C03002D, 0x1901001700000002, -65280 };
            s[40] = new long[] { 0x102001200390300, -279275232034816 };
            s[41] = new long[] { 0x3000200070700, 0x700000001020019, -64000 };
            s[42] = new long[] { 0x3A030010003C0300, 0x41003B03004000, 0x2002F0000000102 };
            s[43] = new long[] { 0x3D03001200130300, 0x38001203003700, 0x11030039001003, 0x220000000102003A, -64256 };
            s[44] = new long[] { 0xD000400190700, c1, -279275534024704 };
            s[45] = new long[] { 0xD0004001A0700, c1, -280375028875264 };
            s[46] = new long[] { 0xD0004001B0700, c1, -280375012098048 };
            s[47] = new long[] { 0xD0004001C0700, c1, -280374995320832 };
            s[48] = new long[] { 0xD0005000F0700, c0, 0x1000F0000000102 };
            s[49] = new long[] { 0xD000500100700, c0, 0x100100000000102 };
            s[50] = new long[] { 0xD000500110700, c0, 0x100110000000102 };
            s[51] = new long[] { 0xD000500120700, c0, 0x100120000000102 };
            s[52] = new long[] { 0xD000500130700, c0, 0x100130000000102 };
            s[53] = new long[] { 0xD000500140700, c0, 0x100140000000102 };
            s[54] = new long[] { 0x3E03001A003F0300, 8623495936, -280375381131260 };
            s[55] = new long[] { 0x4103000600400300, 0x8004303000700, 0x42030009004403, 0x2000B004503000A, 0x901000D00000006, 0x1000B01000A0100, -280375230136308 };
            s[56] = new long[] { 0xB000600080800, 0x100080000000102 };
            s[57] = new long[] { 0x120005002B0700, 0x18001600140013, 0x3002B0000000102 };
            s[58] = new long[] { 0x120004002F0700, 0x102001800170014, -278175653298176 };
            s[59] = new long[] { 0x12000400310700, 0x102001800170014, -280374642999296 };
            s[60] = new long[] { 0x4703000D00380300, 0xF004603000E00, 0x31030026003703, 0x300280032030027, 0x2A00340300290033, 0x3503002B00300300, 0x2D004803002C00, 0x100320000000302, -280374592602061 };
            s[61] = new long[] { 0x102001800490300, -274877336518656 };
            s[62] = new long[] { 0xD000400040700, c1, -279275886346240 };
            s[63] = new long[] { 0x102001B004A0300, -279275869569024 };
            s[64] = new long[] { 0x102000C004B0300, -279275735351296 };
            s[65] = new long[] { 0x102000C004C0300, -279275802460160 };
            s[66] = new long[] { 0x102000C004D0300, -279275785682944 };
            s[67] = new long[] { 0x102000C004E0300, -279275768905728 };
            s[68] = new long[] { 0x102000C004F0300, -279275752128512 };
            s[69] = new long[] { 0x102000C00500300, -279275718574080 };
            s[70] = new long[] { 0x12000400320700, 0x102001800170014, -279275114594304 };
            s[71] = new long[] { 0x102000F00510300, -279275097817088 };
            s[72] = new long[] { 0x102000F00520300, -279275081039872 };
            s[73] = new long[] { 0x3000500220700, 0x20001C0019000D, 0x700220000000102 };
            s[74] = new long[] { 0xD000400050700, c1, -278176357941248 };
            s[75] = new long[] { 0xD0005000D0700, c0, 0x3000D0000000102 };
            s[76] = new long[] { 0xD000500090700, c0, 0x300090000000102 };
            s[77] = new long[] { 0xD0005000A0700, c0, 0x3000A0000000102 };
            s[78] = new long[] { 0xD0005000B0700, c0, 0x3000B0000000102 };
            s[79] = new long[] { 0xD0005000C0700, c0, 0x3000C0000000102 };
            s[80] = new long[] { 0xD0005000E0700, c0, 0x3000E0000000102 };
            s[81] = new long[] { 0x12000400330700, 0x102001800170014, -278175586189312 };
            s[82] = new long[] { 0x12000400340700, 0x102001800170014, -278175569412096 };
            var a = new long[6][];
            a[0] = new long[] { -72008090243562752 };
            a[1] = new long[] { 0x2D001D00020700 };
            a[2] = new long[] { -72031179986763008 };
            a[3] = new long[] { -72032279497014528 };
            a[4] = new long[] { -72032279497145600 };
            a[5] = new long[] { -72032279497735424 };
            return global::Infragistics.Documents.Parsing.ParseTable.Decode(s, a);
        }
        #endregion

        #region InitializeGrammarProperties
        /// <summary>
        /// Initializes the properties of the associated Grammar.
        /// </summary>
        private static void InitializeGrammarProperties(global::Infragistics.Documents.Parsing.Grammar grammar)
        {
            grammar.Name = "Infragistics XML Language Definition";
            grammar.WhitespacePattern = "[ \\t]+";
        }
        #endregion

        #endregion


        #region SymbolNames
        /// <summary>
        /// Symbol name constants for the <see cref="CustomXMLLanguage"/>.
        /// </summary>
        public static class SymbolNames
        {

            /// <summary>
            /// Name for the AttributeDeclaration symbol.
            /// </summary>
            public const string AttributeDeclaration = "AttributeDeclaration";

            /// <summary>
            /// Name for the AttributeList symbol.
            /// </summary>
            public const string AttributeList = "AttributeList";

            /// <summary>
            /// Name for the AttributeName symbol.
            /// </summary>
            public const string AttributeName = "AttributeName";

            /// <summary>
            /// Name for the AttributeValue symbol.
            /// </summary>
            public const string AttributeValue = "AttributeValue";

            /// <summary>
            /// Name for the CData symbol.
            /// </summary>
            public const string CData = "CData";

            /// <summary>
            /// Name for the CDATA_DEC_CLOSE symbol.
            /// </summary>
            public const string CDATA_DEC_CLOSE = "CDATA_DEC_CLOSE";

            /// <summary>
            /// Name for the CDATA_DEC_OPEN symbol.
            /// </summary>
            public const string CDATA_DEC_OPEN = "CDATA_DEC_OPEN";

            /// <summary>
            /// Name for the CDATA_TEXT symbol.
            /// </summary>
            public const string CDATA_TEXT = "CDATA_TEXT";

            /// <summary>
            /// Name for the ColonToken symbol.
            /// </summary>
            public const string ColonToken = "ColonToken";

            /// <summary>
            /// Name for the COMMENT_END symbol.
            /// </summary>
            public const string COMMENT_END = "COMMENT_END";

            /// <summary>
            /// Name for the COMMENT_START symbol.
            /// </summary>
            public const string COMMENT_START = "COMMENT_START";

            /// <summary>
            /// Name for the COMMENT_TEXT symbol.
            /// </summary>
            public const string COMMENT_TEXT = "COMMENT_TEXT";

            /// <summary>
            /// Name for the Content symbol.
            /// </summary>
            public const string Content = "Content";

            /// <summary>
            /// Name for the DotToken symbol.
            /// </summary>
            public const string DotToken = "DotToken";

            /// <summary>
            /// Name for the ElementName symbol.
            /// </summary>
            public const string ElementName = "ElementName";

            /// <summary>
            /// Name for the EndOfStreamToken symbol.
            /// </summary>
            public const string EndOfStreamToken = "EndOfStreamToken";

            /// <summary>
            /// Name for the ENTITY_AMP symbol.
            /// </summary>
            public const string ENTITY_AMP = "ENTITY_AMP";

            /// <summary>
            /// Name for the ENTITY_APOS symbol.
            /// </summary>
            public const string ENTITY_APOS = "ENTITY_APOS";

            /// <summary>
            /// Name for the ENTITY_END symbol.
            /// </summary>
            public const string ENTITY_END = "ENTITY_END";

            /// <summary>
            /// Name for the ENTITY_GT symbol.
            /// </summary>
            public const string ENTITY_GT = "ENTITY_GT";

            /// <summary>
            /// Name for the ENTITY_LT symbol.
            /// </summary>
            public const string ENTITY_LT = "ENTITY_LT";

            /// <summary>
            /// Name for the ENTITY_QUOT symbol.
            /// </summary>
            public const string ENTITY_QUOT = "ENTITY_QUOT";

            /// <summary>
            /// Name for the ENTITY_START symbol.
            /// </summary>
            public const string ENTITY_START = "ENTITY_START";

            /// <summary>
            /// Name for the ENTITY_TEXT symbol.
            /// </summary>
            public const string ENTITY_TEXT = "ENTITY_TEXT";

            /// <summary>
            /// Name for the EntityAmpersand symbol.
            /// </summary>
            public const string EntityAmpersand = "EntityAmpersand";

            /// <summary>
            /// Name for the EntityDoubleQuote symbol.
            /// </summary>
            public const string EntityDoubleQuote = "EntityDoubleQuote";

            /// <summary>
            /// Name for the EntityGreaterThan symbol.
            /// </summary>
            public const string EntityGreaterThan = "EntityGreaterThan";

            /// <summary>
            /// Name for the EntityLessThan symbol.
            /// </summary>
            public const string EntityLessThan = "EntityLessThan";

            /// <summary>
            /// Name for the EntitySingleQuote symbol.
            /// </summary>
            public const string EntitySingleQuote = "EntitySingleQuote";

            /// <summary>
            /// Name for the EntityText symbol.
            /// </summary>
            public const string EntityText = "EntityText";

            /// <summary>
            /// Name for the EqualsToken symbol.
            /// </summary>
            public const string EqualsToken = "EqualsToken";

            /// <summary>
            /// Name for the IdentifierToken symbol.
            /// </summary>
            public const string IdentifierToken = "IdentifierToken";

            /// <summary>
            /// Name for the LITERAL_CLOSE symbol.
            /// </summary>
            public const string LITERAL_CLOSE = "LITERAL_CLOSE";

            /// <summary>
            /// Name for the LITERAL_CONTENT symbol.
            /// </summary>
            public const string LITERAL_CONTENT = "LITERAL_CONTENT";

            /// <summary>
            /// Name for the LITERAL_OPEN symbol.
            /// </summary>
            public const string LITERAL_OPEN = "LITERAL_OPEN";

            /// <summary>
            /// Name for the NamespacePrefix symbol.
            /// </summary>
            public const string NamespacePrefix = "NamespacePrefix";

            /// <summary>
            /// Name for the NewLineToken symbol.
            /// </summary>
            public const string NewLineToken = "NewLineToken";

            /// <summary>
            /// Name for the QuestionToken symbol.
            /// </summary>
            public const string QuestionToken = "QuestionToken";

            /// <summary>
            /// Name for the RootContent symbol.
            /// </summary>
            public const string RootContent = "RootContent";

            /// <summary>
            /// Name for the SLASH symbol.
            /// </summary>
            public const string SLASH = "SLASH";

            /// <summary>
            /// Name for the Tag symbol.
            /// </summary>
            public const string Tag = "Tag";

            /// <summary>
            /// Name for the TAG_CLOSE symbol.
            /// </summary>
            public const string TAG_CLOSE = "TAG_CLOSE";

            /// <summary>
            /// Name for the TAG_OPEN symbol.
            /// </summary>
            public const string TAG_OPEN = "TAG_OPEN";

            /// <summary>
            /// Name for the TAG_VALUE_CONTENT symbol.
            /// </summary>
            public const string TAG_VALUE_CONTENT = "TAG_VALUE_CONTENT";

            /// <summary>
            /// Name for the TagValue symbol.
            /// </summary>
            public const string TagValue = "TagValue";

            /// <summary>
            /// Name for the UnrecognizedToken symbol.
            /// </summary>
            public const string UnrecognizedToken = "UnrecognizedToken";

            /// <summary>
            /// Name for the WhitespaceToken symbol.
            /// </summary>
            public const string WhitespaceToken = "WhitespaceToken";

            /// <summary>
            /// Name for the XML_DEC symbol.
            /// </summary>
            public const string XML_DEC = "XML_DEC";

            /// <summary>
            /// Name for the XmlDeclaration symbol.
            /// </summary>
            public const string XmlDeclaration = "XmlDeclaration";
        }
        #endregion

    }
    #endregion

}
