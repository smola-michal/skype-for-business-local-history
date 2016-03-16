using System.IO;

namespace SkyForBusLH
{
    public class ConversationIdStorage
    {
        private readonly string _folder;

        public ConversationIdStorage() : this(Storage.ConversationIdStorageFolder)
        {
        }

        public ConversationIdStorage(string folder)
        {
            _folder = folder;
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
        }

        private const string FileName = @"\nextConvId.txt";

        public int Id
        {
            get
            {
                int result = 1;
                if (!File.Exists(_folder + FileName))
                {
                    return result;
                }

                using (var idFile = new StreamReader(_folder + FileName))
                {
                    string line = idFile.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        result = int.Parse(line);
                    }
                }

                return result;
            }
            set
            {
                using (var outfile = new StreamWriter(_folder + FileName, false))
                {
                    outfile.WriteLine(value);
                    outfile.Close();
                }
            }
        }
    }
}
