using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WaveFit2.Database.CRUD;
using WaveFit2.Database.Model;

namespace WaveFit2.Patient.View
{
    /// <summary>
    /// Lógica interna para PatientForm.xaml
    /// </summary>
    public partial class PatientForm : Window
    {
        public readonly Crud crudOperations = new Crud();

        public ObservableCollection<PatientModel> Patient { get; set; } = new ObservableCollection<PatientModel>();

        public List<PatientModel> Patients;

        public bool checkCodeString = false;

        public string gender = "Masculino";

        public string[] docType = { "RG", "CPF", "Outro" };

        public PatientForm()
        {
            InitializeComponent();
            this.DataContext = this;
            PatientTypeDoc.ItemsSource = docType;
            PatientTypeDoc.SelectedIndex = 2;
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
            CheckM.Checked += CheckM_Checked;
            CheckF.Checked += CheckF_Checked;
            CheckOther.Checked += CheckOther_Checked;
        }

        private void CheckOther_Checked(object sender, RoutedEventArgs e)
        {
            gender = "Outro";
        }

        private void CheckF_Checked(object sender, RoutedEventArgs e)
        {
            gender = "Feminino";
        }

        private void CheckM_Checked(object sender, RoutedEventArgs e)
        {
            gender = "Masculino";
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CleanMessages();
                crudOperations.UpdatePatient(long.Parse(PatientCode.Value.ToString()), PatientName.Text, PatientSurname.Text,
                gender, PatientBirthday.SelectedDate.Value, PatientTypeDoc.SelectedItem.ToString(), PatientNumDoc.Text.ToString());
                SaveButton.IsEnabled = false;
                HandyControl.Controls.Growl.SuccessGlobal("Paciente editado com sucesso.");
                Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                {
                    Dispatcher.Invoke(() => Close());
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            CleanMessages();
            if (string.IsNullOrEmpty(PatientName.Text.ToString()) || string.IsNullOrEmpty(PatientSurname.Text.ToString()) ||
                string.IsNullOrEmpty(PatientCode.Value.ToString()) || string.IsNullOrEmpty(gender) ||
                PatientBirthday.SelectedDate == null)
            {
                HandyControl.Controls.Growl.WarningGlobal("Preencha os campos obrigatórios (*) abaixo.");
            }
            else
            {
                try
                {
                    Patients = new List<PatientModel>{
                    new PatientModel
                    {
                        Name = PatientName.Text.ToString(),
                        Surname = PatientSurname.Text.ToString(),
                        PatientCode = long.Parse(PatientCode.Value.ToString()),
                        Birthday = PatientBirthday.SelectedDate.Value,
                        Gender = gender,
                        TypeDocument = PatientTypeDoc.SelectedItem.ToString(),
                        NumDocument = PatientNumDoc.Text.ToString(),
                        Status = true
                    }};
                }
                catch (Exception ex)
                {
                    HandyControl.Controls.Growl.WarningGlobal("Dados inseridos de forma incorreta, tente digitar novamente.");
                    checkCodeString = true;
                }

                if (checkCodeString == false)
                {
                    if (crudOperations.CheckPatientCode(Patients) == true)
                    {
                        crudOperations.AddPatient(Patients);
                        RegisterButton.IsEnabled = false;
                        HandyControl.Controls.Growl.SuccessGlobal("Paciente registrado com sucesso.");
                        string nomeCompleto = PatientName.Text.ToString() + " " + PatientSurname.Text.ToString();
                        Properties.Settings.Default.patientName = nomeCompleto;
                        Properties.Settings.Default.patientCode = long.Parse(PatientCode.Value.ToString());
                        crudOperations.GetPatientId(Properties.Settings.Default.patientCode);
                        Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith((task) =>
                        {
                            Dispatcher.Invoke(() => Close());
                        });
                    }
                    else
                    {
                        HandyControl.Controls.Growl.ErrorGlobal("Código já registrado.");
                    }
                }
                else
                {
                    checkCodeString = false;
                }
            }
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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