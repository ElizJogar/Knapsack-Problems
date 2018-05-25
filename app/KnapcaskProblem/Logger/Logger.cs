using System;
using System.IO;
using System.Globalization;

namespace CustomLogger
{
    public class Logger
    {
        private static Logger m_instance;
        private StreamWriter m_writeFile;
        private DateTime m_localDate;
        private CultureInfo m_culture;
        private DirectoryInfo m_dir;
        private string m_fileName;

        public bool debug;

        public static Logger Get()
        {
            if (m_instance == null) m_instance = new Logger();
            return m_instance;
        }

        private Logger()
        {
            m_culture = new CultureInfo("en-GB");
            debug = false;
            m_localDate = DateTime.Now;
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_dir = new DirectoryInfo(myDocPath + @"\knapsack_problems_doc");
            m_dir.Create();
            m_fileName = m_dir.FullName + @"\log_" + 
                m_localDate.Hour + "." + m_localDate.Minute + "." + m_localDate.Second + "." + m_localDate.Millisecond +
                ".log";
            using (m_writeFile = new StreamWriter(m_fileName, false))
                m_writeFile.WriteLine("__________________________ Log Started. " +
                    m_localDate.ToString(m_culture) +
                    " __________________________");
        }

        public void Debug(string text)
        {
            if (debug)
            {
                m_localDate = DateTime.Now;
                using (m_writeFile = new StreamWriter(m_fileName, true))
                    m_writeFile.WriteLine("DEBUG:        " + m_localDate.ToString(m_culture) + ": " + text);
            }
        }

        public void Info(string text)
        {
            m_localDate = DateTime.Now;
            using (m_writeFile = new StreamWriter(m_fileName, true))
                m_writeFile.WriteLine("INFO:      " + m_localDate.ToString(m_culture) + ": " + text);
        }

        public void Warning(string text)
        {
            m_localDate = DateTime.Now;
            using (m_writeFile = new StreamWriter(m_fileName, true))
                m_writeFile.WriteLine("WARNING:    " + m_localDate.ToString(m_culture) + ": " + text);
        }

        public void Error(string text)
        {
            m_localDate = DateTime.Now;
            using (m_writeFile = new StreamWriter(m_fileName, true))
                m_writeFile.WriteLine("ERROR:        " + m_localDate.ToString(m_culture) + ": " + text);
        }

    }
}
