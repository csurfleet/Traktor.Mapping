using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private ObservableCollection<MappingModel> mappings;

        public ObservableCollection<MappingModel> Mappings
        {
            get { return mappings; }
            set
            {
                mappings = value;
                OnPropertyChanged(nameof(Mappings));
            }
        }

        private ICollectionView mappingView;

        public ICollectionView MappingView
        {
            get { return mappingView; }
            set
            {
                mappingView = value;
                OnPropertyChanged(nameof(MappingView));
            }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        private string filter;

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
