using Model.DataSet;
using Model.KEA.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.KEA
{
    public static class KEAGlobal
    {
        static IContext Context;
        static bool isInitiated;
        public static void InitiateKEAGlobal(IContext context)
        {
            Context = context;
            if (context != null)
            {
                isInitiated = true;
            }
            else
            {
                isInitiated = false;
            }
        }

        static void CheckInitiateStatus()
        {
            if (!isInitiated)
            {
                throw new InvalidOperationException("KEAGlobal is not initiated yet. Use InitiateKEAGlobal to initiate it");
            }
        }

        static LanguageManager languageManager;
        public static LanguageManager LanguageManager
        {
            get
            {
                if (languageManager == null)
                {
                    CheckInitiateStatus();
                    languageManager = new LanguageManager(Context);
                }
                return languageManager;
            }
        }

        static PDFHelper pDFHelper;
        public static PDFHelper PDFHelper
        {
            get
            {
                if (pDFHelper == null)
                {
                    CheckInitiateStatus();
                    pDFHelper = new PDFHelper();
                }
                return pDFHelper;
            }
        }

        static DocumentManager documentManager;
        public static DocumentManager DocumentManager
        {
            get
            {
                if (documentManager == null)
                {
                    CheckInitiateStatus();
                    documentManager = new DocumentManager(Context);
                }
                return documentManager;
            }
        }

        static SentenseManager sentenseManager;
        public static SentenseManager SentenseManager
        {
            get
            {
                if (sentenseManager == null)
                {
                    CheckInitiateStatus();
                    sentenseManager = new SentenseManager(Context);
                }
                return sentenseManager;
            }
        }
    }
}
