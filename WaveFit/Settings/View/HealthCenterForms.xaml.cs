using Correios.NET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Settings.View
{
    /// <summary>
    /// Interaction logic for HealthCenterForms.xaml
    /// </summary>
    public partial class HealthCenterForms : Window
    {
        public ObservableCollection<HealthCenterModel> HealthCenter { get; set; } = new ObservableCollection<HealthCenterModel>();
        public ObservableCollection<AudiometerModel> Audiometer { get; set; } = new ObservableCollection<AudiometerModel>();
        public ObservableCollection<PlaceModel> Place { get; set; } = new ObservableCollection<PlaceModel>();

        public List<HealthCenterModel> HealthCenters;

        public List<AudiometerModel> Audiometers;

        public List<PlaceModel> Places;

        private readonly ICorreiosService _services;

        public bool checkString = false;

        public readonly Crud crudOperations = new Crud();

        public string[] idLocation = {
            "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA",
            "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN",
            "RO", "RR", "RS", "SC", "SE", "SP", "TO"
        };

        public HealthCenterForms()
        {
            InitializeComponent();
            _services = new CorreiosService();
            StateAcronym.ItemsSource = idLocation;
            PatientFormButton();
        }

        public void CleanMessages()
        {
            HandyControl.Controls.Growl.Clear();
            HandyControl.Controls.Growl.ClearGlobal();
        }

        public void PatientFormButton()
        {
            CloseButton.Click += CloseButton_Click;
            SaveButton.Click += SaveButton_Click; 
            RegisterButton.Click += RegisterButton_Click;

            CEP.TextChanged += CEP_TextChanged;
        }

        private async void CEP_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(CEP.Text.Length >= 8)
                {
                    string zipCode = CEP.Text.ToString();
                    var result = await _services.GetAddressesAsync(zipCode);
                    if (result.Count() > 0)
                    {
                        var resultAddress = result.FirstOrDefault();
                        string[] address = resultAddress.Street.Split('-');
                       
                        City.Text = resultAddress.City.ToString();
                        Area.Text = resultAddress.District.ToString();
                        Address.Text = address[0].Trim();
                        StateAcronym.SelectedValue = resultAddress.State.ToString();
                    }
                }
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static int getEstadoIndex(string[] estados, string estado)
        {
            for (int i = 0; i < estados.Length; i++)
            {
                if (estados[i].Equals(estado))
                {
                    return i; 
                }
            }
            return -1; 
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            CleanMessages();
            if (string.IsNullOrEmpty(HealthCenterName.Text.ToString()) || string.IsNullOrEmpty(Nickname.Text.ToString()) ||
                string.IsNullOrEmpty(CNPJ.Text.ToString()) || string.IsNullOrEmpty(Telephone.Text.ToString()) ||
                string.IsNullOrEmpty(CEP.Text.ToString()) || string.IsNullOrEmpty(StateAcronym.SelectedValue.ToString()) ||
                string.IsNullOrEmpty(Area.Text.ToString()) || string.IsNullOrEmpty(City.Text.ToString()) ||
                string.IsNullOrEmpty(Address.Text.ToString()) || string.IsNullOrEmpty(Number.Text.ToString()) || 
                string.IsNullOrEmpty(Code.Text.ToString()) || string.IsNullOrEmpty(Model.Text.ToString()) || 
                Maintenance.SelectedDate == null)
            {
                HandyControl.Controls.Growl.WarningGlobal("Preencha os campos obrigatórios (*) abaixo.");
            }
            else
            {
                try
                {
                    Audiometers = new List<AudiometerModel>{
                    new AudiometerModel
                    {
                        Code = Code.Text.ToString(),
                        Model = Model.Text.ToString(),
                        Maintenance = Maintenance.SelectedDate.Value
                    }};

                    Places = new List<PlaceModel>{
                    new PlaceModel
                    {
                        IdLocation = getEstadoIndex(idLocation, StateAcronym.SelectedValue.ToString()),
                        City = City.Text.ToString(),
                        Area = Area.Text.ToString(),
                        Address = $"{Address.Text}, {Number.Text} - {Complement.Text}",
                        CEP = CEP.Text.ToString()
                    }};
                    
                    HealthCenters = new List<HealthCenterModel>{
                    new HealthCenterModel
                    {
                        Name = HealthCenterName.Text.ToString(),
                        Nickname = Nickname.Text.ToString(),
                        CNPJ = Nickname.Text.ToString(),
                        Telephone = Nickname.Text.ToString(),
                        Status = true,
                        IdAudiometer = Audiometer[0].Id,
                        IdPlace = Place[0].Id
                    }};
                }
                catch (Exception ex)
                {
                    HandyControl.Controls.Growl.WarningGlobal("Dados inseridos de forma incorreta, tente digitar novamente.");
                    checkString = true;
                }

                if (checkString == false)
                {
                    crudOperations.AddPlace(Places);
                    crudOperations.AddAudiometer(Audiometers);
                    crudOperations.AddHealthCenter(HealthCenters);
                    RegisterButton.IsEnabled = false;
                    HandyControl.Controls.Growl.SuccessGlobal("Paciente registrado com sucesso.");
                    Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                    {
                        Dispatcher.Invoke(() => Close());
                    });
                }
                else
                {
                    checkString = false;
                }
            }
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DatePicker dp = (DatePicker)sender;
            Regex regex = new Regex("[^0-9/]");
            e.Handled = regex.IsMatch(e.Text);

            var textBox = (TextBox)e.OriginalSource;
            string currentText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            int numDigits = currentText.Replace("/", "").Length;

            if (!regex.IsMatch(e.Text))
            {
                if (numDigits == 2 || numDigits == 4)
                {
                    textBox.Text = currentText + "/";
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
            }
        }
    }
}
