using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Editor.Controllers;
using Editor.ViewModels;
using Microsoft.Win32;
using TraktorMapping.TSI;
using TraktorMapping.TSI.Format;

namespace Editor
{
    public partial class MainWindow : Window
    {
        public MainWindowModel model;

        public MainWindow()
        {
            InitializeComponent();

            model = new MainWindowModel();

            model.PropertyChanged += ModelOnPropertyChanged;

            DataContext = model;
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(model.Filter) && model.Mappings != null)
                model.MappingView.Refresh();

        }

        private async void OpenMenuItemClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "TSI|*.tsi" };

            if (dialog.ShowDialog() == true)
            {
                var currentCursor = Cursor;
                Cursor = Cursors.Wait;
                var tsi = new TsiFile(dialog.FileName);

                var createModel = Task<List<MappingModel>>.Factory.StartNew(() => MainWindowController.CreateModel(tsi.Devices.First()));

                await createModel;
                model.Filter = "";
                model.Mappings = new ObservableCollection<MappingModel>(createModel.Result);
                model.MappingView = CollectionViewSource.GetDefaultView(model.Mappings);
                model.MappingView.Filter = Filter;

                model.FileName = dialog.FileName;

                Cursor = currentCursor;
            }
        }

        public bool Filter(object obj)
        {
            var data = obj as MappingModel;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(model.Filter))
                {
                    return data.TraktorCommand.ToLower().Contains(model.Filter.ToLower()) ||
                        data.Comment.ToLower().Contains(model.Filter.ToLower());
                }
                return true;
            }
            return false;
        }

        private async void SaveTsi(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(model.FileName))
            {
                var currentCursor = Cursor;
                Cursor = Cursors.Wait;

                var saveFile = Task.Factory.StartNew(() =>
                {
                    var tsi = new TsiFile(model.FileName);
                    if (MainWindowController.SaveModel(model.Mappings, tsi.Devices.First()) == SaveModelResult.SuccessWithNonSavedUnknownMappings)
                    {
                        MessageBox.Show("You have made changes to mappings associated with unknown traktor functions. " +
                            "Please note that these changes will not be saved. All other changes will be saved as normal.");
                    }
                    tsi.Save();
                });

                await saveFile;

                Cursor = currentCursor;
            }
        }
    }
}
