using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeilaoOnline
{
    public class LanceSuperiorMaisProximo : IModalidadeAvaliacao
    {
        public double ValorDestino { get; set; }
        public LanceSuperiorMaisProximo(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                    .Where(l => l.Valor > ValorDestino)
                    .OrderBy(l => l.Valor)
                    .FirstOrDefault();
        }
    }
}
