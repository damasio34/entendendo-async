using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entendendo_async
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("************** Iniciando a console  **************");
            var servico = new Service("Bolsa");
            servico.OnMudancaNoStatusDoServicoEventHandler += Servico_OnMudancaNoStatusDoServicoEventHandler;
            servico.OnNovaSoma += Servico_OnNovaSoma;
            servico.Inicializa();

            var servico2 = new Service("Fundo");
            servico2.OnMudancaNoStatusDoServicoEventHandler += Servico_OnMudancaNoStatusDoServicoEventHandler;
            servico2.OnNovaSoma += Servico_OnNovaSoma;
            servico2.Inicializa();

            var servico3 = new Service("Fixa ");
            servico3.OnMudancaNoStatusDoServicoEventHandler += Servico_OnMudancaNoStatusDoServicoEventHandler;
            servico3.OnNovaSoma += Servico_OnNovaSoma;
            servico3.Inicializa();

            Console.WriteLine("NÃO TRAVOU");

            Console.ReadKey();
        }

        private static void Servico_OnNovaSoma(Service servico, int ordem, int a, int b, int delay)
        {
            Console.WriteLine($"{servico.Nome} |{ordem}º - {a} + {b} = { a + b } | {delay}");
        }
        private static void Servico_OnMudancaNoStatusDoServicoEventHandler(Service servico, StatusDoServico status)
        {            
            Console.WriteLine($"{servico.Nome} | Mudança de status do serviço: {status}.");
            if (status.Equals(StatusDoServico.Inicializado)) servico.GeraVariasTasks();
        }
    }
}
