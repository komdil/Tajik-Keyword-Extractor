using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Linq;
using System.Text;
using TajikKEA;

namespace TajikKEAHelper
{
    public class PDFHelper
    {
        public string ReadPdfFile(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                string strText = string.Empty;

                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                    var bytes = reader.GetPageContent(page);
                    var text = Encoding.UTF8.GetString(bytes);
                    var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                    s = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                    s = ReplaceMent(s);
                    strText = strText + s;
                }
                strText = KEAGlobal.TextnormilizerManager.RemoveUnnassesarySpaces(strText);
                return strText;
            }
        }

        ReplaceMent[] replaceMents = new ReplaceMent[]
        {
             new ReplaceMent {ReplaceFrom="ќ",ReplaceTo="қ" },
             new ReplaceMent {ReplaceFrom="Ќ",ReplaceTo="Қ" },
             new ReplaceMent {ReplaceFrom="ў",ReplaceTo="ӯ" },
             new ReplaceMent {ReplaceFrom="Ў",ReplaceTo="Ӯ" },
             new ReplaceMent {ReplaceFrom="ї",ReplaceTo="ӣ" },
             new ReplaceMent {ReplaceFrom="ї",ReplaceTo="ӣ" },
             new ReplaceMent {ReplaceFrom="ѓ",ReplaceTo="ғ" },
             new ReplaceMent {ReplaceFrom="Ѓ",ReplaceTo="Ғ" },
             new ReplaceMent {ReplaceFrom="љ",ReplaceTo="ҷ" },
             new ReplaceMent {ReplaceFrom="Љ",ReplaceTo="Ҷ" },
             new ReplaceMent {ReplaceFrom="њ",ReplaceTo="ҳ" },
             new ReplaceMent {ReplaceFrom="Њ",ReplaceTo="Ҳ" },
        };
        string ReplaceMent(string text)
        {
            foreach (var item in replaceMents.Where(s => s.ReplaceFrom != null))
            {
                text = text.Replace(item.ReplaceFrom, item.ReplaceTo);
            }
            return text;
        }
    }
}
