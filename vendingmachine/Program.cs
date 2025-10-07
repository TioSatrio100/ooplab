//телеграм : riobas28
//ФИО :Субагио Сатрио Брахманторо Ади
//группу:K3240

using vendingmachine.Services;

namespace vendingmachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();
            machine.Run();
        }
    }
}

