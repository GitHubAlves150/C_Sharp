using AppMVVM.Models;
using AppMVVM.Services;
using AppMVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppMVVM.ViewModels
{
    public class VeiculosViewModel : INotifyPropertyChanged // pra saber quando as propriedades serao alteradas
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //A propriedade que sera alterada é Veiculo da classe veiculos que esta na pasta Models back-end
        public ObservableCollection<Veiculos> Veiculo{ get; set; }
        public ICommand BuscarVeiculos { get; set; }

        //construtor da classe VeiculosViewModel
        public VeiculosViewModel()
        {
            BuscarVeiculos = new RelayCommad(ObterVeiculos);
        }

        public void ObterVeiculos(object obj)
        {
            var veiculosList = new Veiculos().ObterVeivulos();
            Veiculo = new ObservableCollection<Veiculos>(veiculosList as List<Veiculos>);
            NotifyPropertyChanged("Veiculo");
        }

        //manipulador desse evento é
        public void NotifyPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            
        }
    }
}
