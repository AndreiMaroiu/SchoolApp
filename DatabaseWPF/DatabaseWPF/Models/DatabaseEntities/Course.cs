using DatabaseWPF.Models.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.Models.DatabaseEntities
{
    class Course : DatabaseObject
    {
        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ClassSubjectID { get; set; }

        public override string ToString()
            => $"{Name.Trim()} {Description?.Trim()}";

        public void Download()
        {
            SaveFileDialog dialog = new();
            dialog.Filter = FileFilters.GetFilter(Extension);

            if (dialog.ShowDialog() is true)
            {
                try
                {
                    File.WriteAllBytes($"{dialog.FileName}{Extension.Trim()}", Data);
                }
                catch (IOException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
