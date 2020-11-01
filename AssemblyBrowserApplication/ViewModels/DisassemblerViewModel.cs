using System.ComponentModel;
using System.IO;
using System.Reflection;
using AssemblyBrowser.Models;
using AssemblyBrowser.ViewModels.Command;
using Disassembler.Entity;
using Microsoft.Win32;

namespace AssemblyBrowser.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private AssemblyInfo _assemblyInfo;

        public ApplicationViewModel()
        {
            SelectAssemblyCommand = new RelayCommand(LoadAssemblyInfo);
        }

        public AssemblyInfo AssemblyInfo
        {
            get => _assemblyInfo;
            set
            {
                _assemblyInfo = value;
                OnPropertyChanged("AssemblyInfo");
            }
        }

        public RelayCommand SelectAssemblyCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadAssemblyInfo(object o)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select assembly to view",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = ".Net assembly files (*.exe, *.dll) | *.exe; *.dll"
            };
            if (openFileDialog.ShowDialog() != true) return;
            var assembly = Assembly.LoadFrom(openFileDialog.FileName);
            var disassemblerModel = new DisassemblerModel();
            AssemblyInfo = disassemblerModel.GetAssemblyInfo(assembly);
        }
    }
}