using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    internal class AppDriver
    {
       
            //fields
            private string nome;
            private string licenseplate;
            private string car;
            private short age;
            private float grade;

            //methodo
            public void ShowDriverinformation(string argName, string argLicenseplate,
                                               string argCar, short argAge, float argGrade)
            {
                nome = argName;
                licenseplate = argLicenseplate;
                car = argCar;
                age = argAge;
                grade = argGrade;

                Console.WriteLine("The driver is " + nome + "with license plate: " + licenseplate);
                Console.WriteLine("Car model: " + car + " Driver age: " + age);
                Console.WriteLine("Driver's grade is: " + grade + " of 5.0");
            }
        
    }
}
