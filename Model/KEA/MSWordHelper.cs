﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.KEA
{
    public static class MSWordHelper
    {
        public static string GetText(string filepath)
        {
            // Open a WordprocessingDocument based on a filepath.
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Open(filepath, false))
            {
                foreach (var item in wordDocument.Parts)
                {

                }
                // Assign a reference to the existing document body.  
                Body body = wordDocument.MainDocumentPart.Document.Body;
                //text of Docx file 
                return body.InnerText.ToString();
            }
        }
    }
}
