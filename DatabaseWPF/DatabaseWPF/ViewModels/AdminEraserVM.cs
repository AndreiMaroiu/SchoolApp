using DatabaseWPF.Models.DataAccesLayer;
using DatabaseWPF.Models.DatabaseEntities;
using DatabaseWPF.Models.Helpers;
using DatabaseWPF.ViewModels.Options;
using DatabaseWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseWPF.ViewModels
{
    class AdminEraserVM : BaseVM
    {
        #region Constants

        private static readonly List<string> entitiesRO = OptionsReader.ReadSimpleOptions("DeleteEntitiesRo.txt");

        private static readonly List<string> entitiesEN = OptionsReader.ReadSimpleOptions("DeleteEntitiesEn.txt");
        private int _currentEntityIndex;
        private string _label;
        private object _databaseObjects;

        #endregion

        public AdminEraserVM(MainWindow window) : base(window)
        {
            EraseCommand = new ButtonCommand(Erase);
        }

        #region Commands

        private void Erase(object o)
        {
            if (CurrentObject is not null)
            {
                try
                {
                    DALHelper.ExecuteNonQuery($"Delete{entitiesEN[CurrentEntityIndex]}", ("@id", CurrentObject.ID));

                    MessageBox.Show($"A fost sters {CurrentObject}");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Alegeti o entitate");
            }
        }

        public ButtonCommand EraseCommand { get; init; }

        #endregion

        #region Methods

        private (string, string) GetProcedureName(string name)
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

            if(Types.GetType(name+"Full") is not null)
            {
                return (result + "Full", name + "Full");
            }
            return (result, name);
        }

        private void ChangeSource(string value)
        {
            var (procedure, type) = GetProcedureName(value);

            DatabaseObjects = DALHelper.GetObjects(procedure, Types.GetType(type));
        }

        #endregion

        #region Bindings

        public List<string> Entities => entitiesRO;

        public int CurrentEntityIndex
        {
            get => _currentEntityIndex;

            set
            {
                _currentEntityIndex = value;

                Label = entitiesRO[value];

                ChangeSource(entitiesEN[value]);

                OnPropertyChanged();
            }
        }

        public string Label
        {
            get => _label;

            set
            {
                _label = value;

                OnPropertyChanged();
            }
        }

        public object DatabaseObjects
        {
            get => _databaseObjects;

            set
            {
                _databaseObjects = value;

                OnPropertyChanged();
            }
        }

        public DatabaseObject CurrentObject { get; set; }

        #endregion
    }
}
