using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using TajikKEA.DataSet;
using TajikKEA.TFIDF;

namespace TajikKEAHelper
{
    public static class MSExcelHelper
    {
        public static void ExtractResult(string excelPath, List<TFIDFView> results)
        {
            using (SpreadsheetDocument xl = SpreadsheetDocument.Create(excelPath, SpreadsheetDocumentType.Workbook))
            {
                List<OpenXmlAttribute> oxa;
                OpenXmlWriter oxw;
                xl.AddWorkbookPart();
                WorksheetPart wsp = xl.WorkbookPart.AddNewPart<WorksheetPart>();
                oxw = OpenXmlWriter.Create(wsp);
                oxw.WriteStartElement(new Worksheet());
                oxw.WriteStartElement(new SheetData());
                for (int i = 1; i <= results.Count + 1; ++i)
                {
                    oxa = new List<OpenXmlAttribute>();
                    // this is the row index
                    oxa.Add(new OpenXmlAttribute("r", null, i.ToString()));

                    oxw.WriteStartElement(new Row(), oxa);

                    oxa = new List<OpenXmlAttribute>();
                    // this is the data type ("t"), with CellValues.String ("str")
                    oxa.Add(new OpenXmlAttribute("t", null, "str"));

                    if (i == 1)
                    {
                        AddCells(oxa, oxw, "Калима");
                        AddCells(oxa, oxw, "Қимати TF");
                        AddCells(oxa, oxw, "Қимати IDF");
                        AddCells(oxa, oxw, "Қимати TFxIDF");
                    }
                    else
                    {
                        var result = results[i - 2];
                        AddCells(oxa, oxw, result.Word);
                        AddCells(oxa, oxw, result.TF.ToString());
                        AddCells(oxa, oxw, result.IDF.ToString());
                        AddCells(oxa, oxw, result.TF_IDF.ToString());
                    }
                    // this is for Row
                    oxw.WriteEndElement();
                }

                // this is for SheetData
                oxw.WriteEndElement();
                // this is for Worksheet
                oxw.WriteEndElement();
                oxw.Close();

                oxw = OpenXmlWriter.Create(xl.WorkbookPart);
                oxw.WriteStartElement(new Workbook());
                oxw.WriteStartElement(new Sheets());

                // you can use object initialisers like this only when the properties
                // are actual properties. SDK classes sometimes have property-like properties
                // but are actually classes. For example, the Cell class has the CellValue
                // "property" but is actually a child class internally.
                // If the properties correspond to actual XML attributes, then you're fine.
                oxw.WriteElement(new Sheet()
                {
                    Name = "Sheet1",
                    SheetId = 1,
                    Id = xl.WorkbookPart.GetIdOfPart(wsp)
                });

                // this is for Sheets
                oxw.WriteEndElement();
                // this is for Workbook
                oxw.WriteEndElement();
                oxw.Close();

                xl.Close();
            }
        }

        public static void ExtractResult(string excelPath, List<IWordDataSet> results)
        {
            using (SpreadsheetDocument xl = SpreadsheetDocument.Create(excelPath, SpreadsheetDocumentType.Workbook))
            {
                List<OpenXmlAttribute> oxa;
                OpenXmlWriter oxw;
                xl.AddWorkbookPart();
                WorksheetPart wsp = xl.WorkbookPart.AddNewPart<WorksheetPart>();
                oxw = OpenXmlWriter.Create(wsp);
                oxw.WriteStartElement(new Worksheet());
                oxw.WriteStartElement(new SheetData());
                for (int i = 1; i <= results.Count + 1; ++i)
                {
                    oxa = new List<OpenXmlAttribute>();
                    // this is the row index
                    oxa.Add(new OpenXmlAttribute("r", null, i.ToString()));

                    oxw.WriteStartElement(new Row(), oxa);

                    oxa = new List<OpenXmlAttribute>();
                    // this is the data type ("t"), with CellValues.String ("str")
                    oxa.Add(new OpenXmlAttribute("t", null, "str"));

                    if (i == 1)
                    {
                        AddCells(oxa, oxw, "Калима");
                        AddCells(oxa, oxw, "Қимати IDF");
                    }
                    else
                    {
                        var result = results[i - 2];
                        AddCells(oxa, oxw, result.Content);
                        AddCells(oxa, oxw, result.CommonIDF.ToString());
                    }
                    // this is for Row
                    oxw.WriteEndElement();
                }

                // this is for SheetData
                oxw.WriteEndElement();
                // this is for Worksheet
                oxw.WriteEndElement();
                oxw.Close();

                oxw = OpenXmlWriter.Create(xl.WorkbookPart);
                oxw.WriteStartElement(new Workbook());
                oxw.WriteStartElement(new Sheets());

                // you can use object initialisers like this only when the properties
                // are actual properties. SDK classes sometimes have property-like properties
                // but are actually classes. For example, the Cell class has the CellValue
                // "property" but is actually a child class internally.
                // If the properties correspond to actual XML attributes, then you're fine.
                oxw.WriteElement(new Sheet()
                {
                    Name = "Sheet1",
                    SheetId = 1,
                    Id = xl.WorkbookPart.GetIdOfPart(wsp)
                });

                // this is for Sheets
                oxw.WriteEndElement();
                // this is for Workbook
                oxw.WriteEndElement();
                oxw.Close();

                xl.Close();
            }
        }

        static void AddCells(List<OpenXmlAttribute> oxa, OpenXmlWriter oxw, string cellValue)
        {
            // it's suggested you also have the cell reference, but
            // you'll have to calculate the correct cell reference yourself.
            // Here's an example:
            //oxa.Add(new OpenXmlAttribute("r", null, "A1"));
            oxw.WriteStartElement(new Cell(), oxa);
            oxw.WriteElement(new CellValue(cellValue));
            // this is for Cell
            oxw.WriteEndElement();
        }
    }
}
