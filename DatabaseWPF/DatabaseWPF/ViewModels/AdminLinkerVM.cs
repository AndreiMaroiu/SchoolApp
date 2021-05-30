using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.ViewModels.Options;
using DatabaseWPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace DatabaseWPF.ViewModels
{
    class AdminLinkerVM : BaseVM
    {
        #region Constants

        private static readonly Dictionary<string, LinkerOptions> linkerOptions =
            OptionsReader.ReadLinkerOptions("LinkerOptions.txt");

        private static readonly List<string> couplings = linkerOptions.Keys.ToList();

        private const int MAXFIELDS = 4;

        #endregion

        private string _currentCoupling;
        private IEnumerable _allEntities;
        private string _checkBoxString;

        public AdminLinkerVM(MainWindow window) : base(window)
        {
            ButtonClick = new ButtonCommand(Add);

            Visibilities = new ObservableCollection<bool>();
            for (int i = 0; i < MAXFIELDS; ++i)
            {
                Visibilities.Add(false);
                CurrentObjects.Add(null);
                Labels.Add(string.Empty);
                Sources.Add(null);
            }
        }

        #region Commands

        public ButtonCommand ButtonClick { get; init; }

        #endregion

        #region AddMethods

        private void Add(object o)
        {
            var options = linkerOptions[CurrentCoupling];

            for (int i = 0; i < options.NullChecks; ++i)
            {
                if (CurrentObjects[i] is null)
                {
                    MessageBox.Show($"Alegeti o valoare pentru {options.LabelNames[i]}");

                    return;
                }
            }

            List<(string name, object data)> parameters = new();

            var paramsList = options.ProcedureParams;
            int count = paramsList.Count;

            if (options.HaveCheckBox)
            {
                count = paramsList.Count - 1;
                parameters.Add(($"@{paramsList[count]}", CurrentObjects[count].ID));
            }

            for (int i = 0; i < count; ++i)
            {
                object id;
                if (CurrentObjects[i] is null)
                {
                    id = DBNull.Value;
                }
                else
                {
                    id = CurrentObjects[i].ID;
                }
                parameters.Add(($"@{paramsList[i]}", id));
            }

            try
            {
                DALHelper.ExecuteNonQuery($"Add{options.ProcedureName}", parameters.ToArray());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            SetWindowObjects(CurrentCoupling);
        }

        #endregion

        #region Set Methods

        private void SetWindowObjects(string value)
        {
            if (linkerOptions.ContainsKey(value))
            {
                var options = linkerOptions[value];
                SetVisibilities(options.ComboCount);

                int count;

                if (options.HaveCheckBox)
                {
                    Visibilities[3] = true;
                    CheckBoxString = options.LabelNames[options.LabelNames.Count - 1];

                    count = options.LabelNames.Count - 1;
                }
                else
                {
                    count = options.LabelNames.Count;
                }

                for (int i = 0; i < count; ++i)
                {
                    Labels[i] = options.LabelNames[i];
                    string tableName = GetTableName(options.ProcedureParams[i]);
                    string procedureName = GetProcedureNameFull(tableName);
                    Type type = GetType(tableName);
                    Sources[i] = DALHelper.GetObjects(procedureName, type);
                }

                AllEntities = DALHelper.GetObjects($"{GetProcedureName(options.ProcedureName)}Full",
                                                    Types.GetType($"{options.ProcedureName}Full"));
            }
        }

        private Type GetType(string value)
        {
            Type type = Types.GetType(value + "Full");

            if (type is not null)
            {
                return type;
            }

            return Types.GetType(value);
        }

        private string GetTableName(string value)
        {
            string aux = value.Replace("ID", "");

            char firstChar = value[0];

            aux = aux.Remove(0, 1);
            aux = new string(firstChar, 1).ToUpper() + aux;

            return aux;
        }

        private string GetProcedureName(string name)
        {
            if (name[name.Length - 1] == 's')
            {
                return $"Get{name}es";
            }
            return $"Get{name}s";
        }

        private string GetProcedureNameFull(string name)
        {
            string result;

            if (name[name.Length - 1] == 's')
            {
                result = $"Get{name}es";
            }
            else
            {
                result = $"Get{name}s";
            }

            if ((Types.GetType(name + "Full")) is not null)
            {
                return result + "Full";
            }
            return result;
        }

        #endregion

        #region Bindings

        public ObservableCollection<DatabaseObject> CurrentObjects { get; set; } = new();

        public ObservableCollection<string> Labels { get; set; } = new();
        public ObservableCollection<IEnumerable> Sources { get; set; } = new();

        public string CurrentCoupling
        {
            get => _currentCoupling;

            set
            {
                _currentCoupling = value;

                SetWindowObjects(value);

                OnPropertyChanged();
            }
        }

        public List<string> Couplings => couplings;

        public IEnumerable AllEntities
        {
            get => _allEntities;

            set
            {
                _allEntities = value;

                OnPropertyChanged();
            }
        }

        public bool BoolValue { get; set; }

        public string CheckBoxString
        {
            get => _checkBoxString;

            set
            {
                _checkBoxString = value;

                OnPropertyChanged();
            }
        }
        #endregion
    }
}
