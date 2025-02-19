/*
 Você está desenvolvendo uma aplicação que precisa buscar informações de usuários a partir de um serviço remoto (por exemplo, uma API). Como a operação de busca pode levar algum tempo, você decide implementar a busca de forma assíncrona para não bloquear a thread principal da aplicação.
Objetivo:

Implementar uma função assíncrona que simula a busca de dados de usuários em um serviço remoto. A função deve retornar uma lista de usuários após um pequeno atraso simulado.

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;


namespace Exercicio_pratico_com_asynt_e_await
{
    internal class Program
    {       

        static async Task Main(string[] args)
        {
            try
            {
                //Chame o metodo assincroono e aguarde o resultado
                var users = await GetuserAsync(10);

                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}");
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: "+ex.Message);
            }

        }

        //Implemente o metodo assiincrono aqui
        static async Task<List<User>> GetuserAsync(int c)
        {
            if (c <= 0)
                throw new ArgumentException("O numero de usuarios deve ser maior que zero.");

            //simule um atrazo de 2 segundos
            await Task.Delay(1000);

            var users = new List<User>();
            for(int i=1; i<=c; i++)
            {
                users.Add(new User { Id =i, Name = $"User {i}" });
            }

            return users;
        }



    }
}
