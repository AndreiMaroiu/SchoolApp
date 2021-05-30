using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.ViewModels.Options;
using DatabaseWPF.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class AdminUpdaterVM : BaseVM
    {
        private const int MAXFIELDS = 5;

        private Dictionary<string, UpdateOptions> multiOptions;
        private string _currentOption;
        private object _entities;
        private object _currentEntity;

        public AdminUpdaterVM(MainWindow window) : base(window)
        {
            multiOptions = OptionsReader.ReadMultiOptions("UpdateOptions.txt");
            Options = multiOptions.Keys.ToList();

            UpdateCommand = new ButtonCommand(Update);

            InitVisibilities(MAXFIELDS);

            for (int i = 0; i < MAXFIELDS; ++i)
            {
                Labels.Add(string.Empty);
                ComboVisibilities.Add(false);
                BoxVisibilities.Add(false);
                Texts.Add(string.Empty);
                Combos.Add(null);
                CurrentCombos.Add(null);
            }
        }

        private void Update(object o)
        {
            if(CurrentOption is null)
            {
                MessageBox.Show("Alegeti o optiune.");
                return;
            }

            UpdateOptions option = multiOptions[CurrentOption];

            List<(string name, object data)> parameters = new();

            DatabaseObject obj = CurrentEntity as DatabaseObject;

            parameters.Add(("@id", obj.ID));

            int index = 0;
            foreach (var param in option.ProcedureParams)
            {
                object value;

                if (CurrentCombos[index] is not null)
                {
                    value = (CurrentCombos[index] as DatabaseObject).ID;
                }
                else if (Texts[index] is not null or "")
                {
                    value = Texts[index];
                }
                else
                {
                    value = DBNull.Value;
                }

                parameters.Add(($"@{param}", value));

                ++index;
            }

            try
            {
                DALHelper.ExecuteNonQuery($"Update{option.ProcedureName}", parameters.ToArray());

                MessageBox.Show($"S-a actualizat {option.Name}");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ButtonCommand UpdateCommand { get; init; }

        private string GetProcedureName(string name)
        {
            string procedure = null;

            if (name[name.Length - 1] == 's')
            {
                procedure = $"Get{name}es";
            }
            else
            {
                procedure = $"Get{name}s";
            }

            return procedure;
        }

        private void SetLocalVisibilities(UpdateOptions options)
        {
            int count = options.LabelNames.Count;

            for (int i = 0; i < count; ++i)
            {
                if (options.ProcedureParams[i].EndsWith("ID"))
                {
                    ComboVisibilities[i] = true;
                    BoxVisibilities[i] = false;
                }
                else
                {
                    BoxVisibilities[i] = true;
                    ComboVisibilities[i] = false;
                }

                Labels[i] = options.LabelNames[i];
            }

            for (int i = count; i < MAXFIELDS; ++i)
            {
                ComboVisibilities[i] = false;
                BoxVisibilities[i] = false;
            }
        }

        private void ShowObjects()
        {
            UpdateOptions options = multiOptions[CurrentOption];

            SetVisibilities(options.LabelNames.Count);
            SetLocalVisibilities(options);

            Type fullType = Types.GetType(options.ProcedureName + "Full");

            if (fullType is not null)
            {
                isFull = true;
                Entities = DALHelper.GetObjects(GetProcedureName(options.ProcedureName) + "Full", fullType); ;
            }
            else
            {
                isFull = false;
                Type type = Types.GetType(options.ProcedureName);
                Entities = DALHelper.GetObjects(GetProcedureName(options.ProcedureName), type);
            }
        }

        private string ParamToName(string param)
        {
            if(param.EndsWith("ID"))
            {
                return param.Replace("ID", "_id").ToLower();
            }

            return param.ToLower();
        }

        private void SetFields()
        {
            if (CurrentEntity is null)
            {
                return;
            }

            UpdateOptions options = multiOptions[CurrentOption];
            Type type = Types.GetType(options.ProcedureName);
            PropertyInfo[] properties = type.GetProperties();

            int index = 0;
            foreach (var property in properties)
            {
                if(index >= MAXFIELDS || index >= options.ProcedureParams.Count)
                {
                    break;
                }

                if (property.Name != "ID")
                {
                    string param = options.ProcedureParams[index];
                    if (!property.Name.ToLower().StartsWith(ParamToName(param)))
                    {
                        continue;
                    }

                    if (property.Name.EndsWith("_ID"))
                    {
                        SetCombo(index, property);
                    }
                    else
                    {
                        Texts[index] = property.GetValue(CurrentEntity);
                    }
                    ++index;
                }
            }
        }

        private void SetCombo(int index, PropertyInfo property)
        {
            string name = property.Name.Replace("_ID", "");
            List<DatabaseObject> list = DALHelper.GetObjects(GetProcedureName(name), Types.GetType(name));
            Combos[index] = list;

            foreach (var item in list)
            {
                object value = property.GetValue(CurrentEntity);

                if(value is null)
                {
                    continue;
                }

                if (item.ID == (int)value)
                {
                    CurrentCombos[index] = item;
                    break;
                }
            }
        }

        private void ClearObjects()
        {
            for (int i = 0; i < MAXFIELDS; ++i)
            {
                Texts[i] = string.Empty;
                Combos[i] = null;
            }
        }

        #region Bidings

        public List<string> Options { get; set; }

        public string CurrentOption
        {
            get => _currentOption;

            set
            {
                _currentOption = value;

                ShowObjects();
                ClearObjects();

                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Labels { get; set; } = new();

        public ObservableCollection<bool> ComboVisibilities { get; set; } = new();

        public ObservableCollection<bool> BoxVisibilities { get; set; } = new();

        public ObservableCollection<object> Texts { get; set; } = new();

        public ObservableCollection<object> Combos { get; set; } = new();
        public ObservableCollection<object> CurrentCombos { get; set; } = new();

        private bool isFull;

        public object Entities
        {
            get => _entities;

            set
            {
                _entities = value;

                OnPropertyChanged();
            }
        }

        public object CurrentEntity
        {
            get => _currentEntity;

            set
            {
                if(value is null)
                {
                    return;
                }

                if (isFull)
                {
                    DatabaseObject obj = value as DatabaseObject;
                    UpdateOptions option = multiOptions[CurrentOption];
                    string name = option.ProcedureName;
                    _currentEntity = DALHelper.GetObject($"Get{name}", Types.GetType(name), ("@id", obj.ID));
                }
                else
                {
                    _currentEntity = value;
                }

                SetFields();
            }
        }

        #endregion
    }
}
