using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LargePermutationsGenerator
{

    class OutputBuffer : IOutputBuffer
    {

        private readonly int _maxBufferSize;
        private readonly int _maxOutputBufferFileSizeInMb = 1024;
        private long _maxOutputBufferFileSizeInBytes;
        private readonly string _outputDirectoryTemplate = @"H:\dev-space\LargePermutationsGenerator\{0}";
        private string _outputDirectory;
        private readonly string outputFileName = "PasswordDictionary_{0}.txt";
        private string _outputFile;
        private readonly List<string> _buffer = new List<string>();
        private readonly List<char[]> _charBuffer = new List<char[]>();
        private FileInfo _outputBufferFile;
        //private int _gigabyteInBytes = 1000000000;
        private int _megaByteIntBytes = 1000000;

        public OutputBuffer(int maxBufferSize)
        {
            _maxBufferSize = maxBufferSize;
            SetCurrentOutputFile();
            CalculateOutputFileSizeLimit();
            SetCurrentOutputFolder();
        }


        public void AddToBuffer(string element)
        {
            _buffer.Add(element);
            FlushBufferIfRequired();
        }

        public void AddToBuffer(char[] element)
        {
            _charBuffer.Add(element);
            FlushBufferIfRequired();
        }

        private void CalculateOutputFileSizeLimit()
        {
            int maxOutputSizeInByte = _megaByteIntBytes * _maxOutputBufferFileSizeInMb;
            _maxOutputBufferFileSizeInBytes = maxOutputSizeInByte;
        }

        private void FlushBufferIfRequired()
        {
            if (!OutputBufferFileHasEnoughSpace())
            {
                SetCurrentOutputFile();
            }
            if (_buffer.Count >= _maxBufferSize || _charBuffer.Count >= _maxBufferSize)
            {
                FlushBuffer();
            }
        }

        private void FlushBuffer()
        {
            if (_buffer.Any())
            {
                File.AppendAllLines(_outputFile, _buffer);
            } else if (_charBuffer.Any())
            {
                string[] tmpBuffer = new string[_charBuffer.Count];
                for (int index = 0; index < _charBuffer.Count; index++)
                {
                    char[] chars = _charBuffer[index];
                    string tmpString = null;
                    foreach (char aChar in chars)
                    {
                        tmpString += Convert.ToString(aChar);
                    }
                    tmpBuffer[index] = tmpString;
                }
                File.AppendAllLines(_outputFile, tmpBuffer);
            }
            _buffer.Clear();
        }

        private bool OutputBufferFileHasEnoughSpace()
        {   FileInfo tmpOutputFile = new FileInfo(_outputBufferFile.FullName);
            if (tmpOutputFile.Exists)
            {
                long currentLengthInBytes = tmpOutputFile.Length;
                return currentLengthInBytes < _maxOutputBufferFileSizeInBytes;
            }
            return true;
        }

        private void SetCurrentOutputFile()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
            string newOutputFileName = String.Format(outputFileName, timestamp);
            _outputFile = Path.Combine(_outputDirectory, newOutputFileName);
            _outputBufferFile = new FileInfo(_outputFile);
        }

        private void SetCurrentOutputFolder()
        {
            Version assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string version = assemblyVersion.ToString(4);
            _outputDirectory = String.Format(_outputDirectoryTemplate, version);
        }
    }
}
