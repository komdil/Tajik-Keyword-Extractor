using System;
using TajikKEA.DataSet;
using TajikKEA.Document;
using TajikKEA.Sentence;
using TajikKEA.TextNormilizer;
using TajikKEA.TFIDF;

namespace TajikKEA
{
    public static class KEAGlobal
    {
        public static IWordContext Context { get; set; }

        static bool isInitiated;
        public static void InitiateKEAGlobal(IWordContext context)
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

        static KEAManager keaManager;
        public static KEAManager KEAManager
        {
            get
            {
                if (keaManager == null)
                {
                    CheckInitiateStatus();
                    keaManager = new KEAManager(Context);
                }
                return keaManager;
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
                    documentManager = new DocumentManager();
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

        static TFIDFManager tFIDFManager;
        public static TFIDFManager TFIDFManager
        {
            get
            {
                if (tFIDFManager == null)
                {
                    CheckInitiateStatus();
                    tFIDFManager = new TFIDFManager();
                }
                return tFIDFManager;
            }
        }


        static TextnormilizerManager textnormilizerManager;
        public static TextnormilizerManager TextnormilizerManager
        {
            get
            {
                if (textnormilizerManager == null)
                {
                    CheckInitiateStatus();
                    textnormilizerManager = new TextnormilizerManager();
                }
                return textnormilizerManager;
            }
        }

        static TajikKEALogger logger;
        public static TajikKEALogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new TajikKEALogger();
                }
                return logger;
            }
        }
    }
}
