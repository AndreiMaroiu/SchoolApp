using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;

namespace DatabaseWPF.ViewModels.Options
{
    public static class OptionsReader
    {
        private static string GetFullPath(string path) => $"DatabaseWPF.OptionsFiles.{path}";

        public static Dictionary<string, List<string>> ReadOptions(string path)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            using Stream stream = thisAssembly.GetManifestResourceStream(GetFullPath(path));
            using StreamReader reader = new StreamReader(stream);

            Dictionary<string, List<string>> result = new();

            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                var items = line.Split(' ');

                List<string> list = new();

                for (int i = 1; i < items.Length; ++i)
                {
                    list.Add(items[i]);   
                }

                result[items[0]] = list;
            }

            return result;
        }

        public static List<string> ReadSimpleOptions(string path)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            using Stream stream = thisAssembly.GetManifestResourceStream(GetFullPath(path));
            using StreamReader reader = new StreamReader(stream);

            List<string> result = new();

            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                result.Add(line);
            }

            return result;
        }

        public static Dictionary<string, UpdateOptions> ReadMultiOptions(string path)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            using Stream stream = thisAssembly.GetManifestResourceStream(GetFullPath(path));
            using StreamReader reader = new StreamReader(stream);

            Dictionary<string, UpdateOptions> result = new();

            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                var items = line.Split(' ');

                if(items.Length > 1)
                {
                    UpdateOptions options = new();

                    options.Name = items[0];
                    options.ProcedureName = items[1];

                    List<string> parameters = new();
                    List<string> labels = new();

                    for (int i = 2; i < items.Length - 1; i += 2)
                    {
                        labels.Add(items[i]);
                        parameters.Add(items[i + 1]);
                    }

                    options.ProcedureParams = parameters;
                    options.LabelNames = labels;

                    result[items[0]] = options;
                }
            }

            return result;
        }

        public static Dictionary<string, LinkerOptions> ReadLinkerOptions(string path)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            using Stream stream = thisAssembly.GetManifestResourceStream(GetFullPath(path));
            using StreamReader reader = new StreamReader(stream);

            Dictionary<string, LinkerOptions> result = new();

            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                var items = line.Split(' ');

                if (items.Length > 4)
                {
                    LinkerOptions options = new();

                    options.Name = items[0];
                    options.ProcedureName = items[1];
                    options.NullChecks = int.Parse(items[2]);
                    options.ComboCount = int.Parse(items[3]);
                    options.HaveCheckBox = bool.Parse(items[4]);

                    List<string> parameters = new();
                    List<string> labels = new();

                    for (int i = 5; i < items.Length - 1; i += 2)
                    {
                        labels.Add(items[i]);
                        parameters.Add(items[i + 1]);
                    }

                    options.ProcedureParams = parameters;
                    options.LabelNames = labels;

                    result[items[0]] = options;
                }
            }

            return result;
        }
    }
}
