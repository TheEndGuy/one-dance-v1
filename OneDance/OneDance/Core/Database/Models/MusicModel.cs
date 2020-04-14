using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Database.Models
{
    public class MusicModel
    {
        private string m_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public MusicModel(string musicDirectory, string musicTitle)
        {
            Directory = musicDirectory;
            Title = musicTitle;
            MediaUri = new Uri(m_path + Directory);
        }

        public string Directory
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public Uri MediaUri
        {
            get;
            set;
        }
    }
}
