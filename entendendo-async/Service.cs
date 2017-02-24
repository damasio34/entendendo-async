using System;
using System.Threading;
using System.Threading.Tasks;

namespace entendendo_async
{
    public class Service
    {
        public Service(string nome) { Nome = nome; }

        public delegate void MudancaDeStatusDoServico(Service service, StatusDoServico statusDoServico);
        public event MudancaDeStatusDoServico OnMudancaNoStatusDoServicoEventHandler;

        public delegate void NovaSoma(Service service, int ordem, int a, int b, int delay);
        public event NovaSoma OnNovaSoma;
        public string Nome { get; }

        public Task Inicializa()
        {
            this.OnMudancaNoStatusDoServicoEventHandler?.Invoke(this, StatusDoServico.Inicializando);

            // Cria a task e já inicializa a mesma
            return Task.Factory.StartNew(() =>
            {
                var r = new Random();
                Thread.Sleep(r.Next(1000, 9000));
                this.OnMudancaNoStatusDoServicoEventHandler?.Invoke(this, StatusDoServico.Inicializado);
            });            
        }
        public Task GeraVariasTasks()
        {
            return Task.Factory.StartNew(() =>
            {
                var r = new Random();

                for (var i = 1; i <= 100; i++)
                {
                    Soma(i, r.Next(1, 9), r.Next(1, 9), r.Next(1000, 5000));
                }
            });
        }

        private Task Soma(int ordem, int a, int b, int delay)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(delay);
                this.OnNovaSoma?.Invoke(this, ordem, a, b, delay);
            });
        }
    }

    public enum StatusDoServico
    {
        Inicializando,
        Inicializado        
    }
}
