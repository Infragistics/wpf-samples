using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace IgExcel.Business
{
    public class NumberFormatInfo : BusinessBase
    {
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; NotifyPropertyChanged(); }
        }

        private string formatsHeader;
        public string FormatsHeader
        {
            get { return formatsHeader; }
            set { formatsHeader = value; NotifyPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        private List<FormatInfo> formats;
        public List<FormatInfo> Formats
        {
            get { return formats; }
            set { formats = value; NotifyPropertyChanged(); }
        }

        private int decimalPlaces;
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set { decimalPlaces = value; NotifyPropertyChanged(); }
        }

        private bool isCustom;
        public bool IsCustom
        {
            get { return isCustom; }
            set { isCustom = value; NotifyPropertyChanged(); }
        }

        private bool areFormatsVisible;
        public bool AreFormatsVisible
        {
            get { return areFormatsVisible; }
            set { areFormatsVisible = value; NotifyPropertyChanged(); }
        }

        public NumberFormatInfo()
        {
            Formats = new List<FormatInfo>();
            DecimalPlaces = 2;
            IsCustom = false;
            AreFormatsVisible = true;
        }

        public FormatInfo AddFormatInfo(string mask)
        {
            var format = new FormatInfo(mask, mask);
            this.Formats.Add(format);
            return format;
        }

        public void AddFormatInfo(string mask, string previewText)
        {
            this.Formats.Add(new FormatInfo(mask, previewText));
        }

        public FormatInfo FindFormat(string mask)
        {
            return this.Formats.FirstOrDefault(x => x.Mask == mask);
        }
    }

    public class FormatInfo : BusinessBase
    {
        private string mask;
        public string Mask
        {
            get { return mask; }
            set { mask = value; NotifyPropertyChanged(); }
        }
        private string color;
        public string Color
        {
            get { return color; }
            set { color = value; NotifyPropertyChanged(); }
        }
        string previewText;
        public string PreviewText
        {
            get { return previewText; }
            set { previewText = value; NotifyPropertyChanged(); }
        }

        public FormatInfo(string mask, string previewText)
        {
            this.mask = mask;
            this.previewText = previewText;
            this.color = "#1E1E1E";
        }

        public FormatInfo(string mask, string previewText, string color)
        {
            this.mask = mask;
            this.previewText = previewText;
            this.color = color;
        }
    }

    public class NumberFormatInfoAdvanced : NumberFormatInfo
    {
        private const string Delimiter = ".";

        public NumberFormatInfoAdvanced()
            : base()
        {
            this.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "DecimalPlaces")
                {
                    UpdateMaskAndPreviewText();
                }
            };
        }

        private void UpdateMaskAndPreviewText()
        {
            Regex regexDecimalPart = new Regex("\\" + Delimiter + "(\\d+)?");
            Regex regexDigit = new Regex("((\\d+[,]\\d+))|(\\d+)|([#]+[,]?([#]+)?)");

            StringBuilder newDecimalPartPreview = new StringBuilder();
            StringBuilder newDecimalPartMask = new StringBuilder();

            if (DecimalPlaces > 0)
            {
                newDecimalPartPreview.Append(Delimiter);
                newDecimalPartMask.Append(Delimiter);

                for (int i = 0; i < DecimalPlaces; i++)
                {
                    newDecimalPartPreview.Append("1");
                    newDecimalPartMask.Append("0");
                }
            }

            foreach (var format in Formats)
            {
                if (!format.PreviewText.Contains(Delimiter) && DecimalPlaces > 0)
                {
                    var matchPreviewDigit = regexDigit.Match(format.PreviewText);

                    if (matchPreviewDigit.Length > 0)
                    {
                        format.PreviewText = format.PreviewText.Replace(matchPreviewDigit.Value, matchPreviewDigit.Value + newDecimalPartPreview.ToString());
                    }

                    var matchesMaskDigit = regexDigit.Matches(format.Mask);

                    foreach (Match match in matchesMaskDigit)
                    {
                        format.Mask = format.Mask.Replace(match.Value, match.Value + newDecimalPartMask.ToString());

                        if (matchesMaskDigit.Count > 1)
                        {
                            if (matchesMaskDigit[0].Value == matchesMaskDigit[1].Value)
                                break;
                        }
                    }

                    continue;
                }

                var matchPreview = regexDecimalPart.Match(format.PreviewText);

                if (matchPreview.Length > 0)
                {
                    format.PreviewText = format.PreviewText.Replace(matchPreview.Value, newDecimalPartPreview.ToString());
                }

                var matchMask = regexDecimalPart.Match(format.Mask);

                if (matchPreview.Length > 0)
                {
                    format.Mask = format.Mask.Replace(matchMask.Value, newDecimalPartMask.ToString());
                }
            }
        }
    }
}
