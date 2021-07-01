using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeilaoOnline.TestesUnidade
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
        {
            var leilao = new Leilao("VanGogh");
            var fulado = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulado, 800);
            leilao.RecebeLance(fulado, 1800);

            leilao.TerminaPregao();

            leilao.RecebeLance(fulado, 1000);

            var valorEsperado = 2;
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(3, new double[] {40, 800, 200})]
        [InlineData(2, new double[] { 40, 800 })]
        [InlineData(5, new double[] { 40, 800, 200, 500, 400 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizadoCases(int qtdLances, double[] ofertas)
        {
            var leilao = new Leilao("VanGogh");
            var fulano = new Interessada("Fulano", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1800);

            var valorEsperado = qtdLances;
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
