using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
namespace GenAlgorithm
{
    class Logger
    {
        private static Logger instance;
        private StreamWriter _writeFile;
        private DateTime _localDate;
        private CultureInfo _culture;
        private DirectoryInfo _dir;
        private string _fileName;
        public bool debug;

        public static Logger Get()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        private Logger()
        {
            _culture = new CultureInfo("en-GB");
            debug = false;
            _localDate = DateTime.Now;
            string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _dir = new DirectoryInfo(myDocPath + @"\gen_algorithm_doc");
            _dir.Create();
            _fileName = _dir.FullName + @"\gen_algorithm_log_"+  + _localDate.Hour + "." + _localDate.Minute + "." + _localDate.Second + "." + _localDate.Millisecond + ".log";
            using (_writeFile = new StreamWriter(_fileName, false))
                _writeFile.WriteLine("__________________________ Log Started. " + _localDate.ToString(_culture) + " __________________________");
        }

        public void Debug(string text)
        {
            if (debug)
            {
                _localDate = DateTime.Now;
                using (_writeFile = new StreamWriter(_fileName, true))
                    _writeFile.WriteLine("DEBUG:        " + _localDate.ToString(_culture) + ": " + text);
            }
        }

        public void Info(string text)
        {
            _localDate = DateTime.Now;
            using (_writeFile = new StreamWriter(_fileName, true))
                _writeFile.WriteLine("INFO:      " + _localDate.ToString(_culture) + ": " + text);
        }

        public void Warning(string text)
        {
            _localDate = DateTime.Now;
            using (_writeFile = new StreamWriter(_fileName, true))
                _writeFile.WriteLine("WARNING:    " + _localDate.ToString(_culture) + ": " + text);
        }

        public void Error(string text)
        {
            _localDate = DateTime.Now;
            using (_writeFile = new StreamWriter(_fileName, true))
                _writeFile.WriteLine("ERROR:        " + _localDate.ToString(_culture) + ": " + text);
        }

    }
}
