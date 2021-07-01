using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeilaoOnline.TestesUnidade
{
    public class LeilaoRecebeLance
    {
        [Fact]

        public void NaoAceitaProximoLanceDadoOMesmoClienteRealizouOUltimoLance()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(fulano, 1800);

            leilao.TerminaPregao();

            var qtdeEsperada = 1;
            var qtadeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtadeObtida);

        }
        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade); var fulado = new Interessada("Fulano", leilao);
            var ciclano = new Interessada("Ciclano", leilao);


            leilao.IniciaPregao();

            leilao.RecebeLance(fulado, 800);
            leilao.RecebeLance(ciclano, 1800);

            leilao.TerminaPregao();

            leilao.RecebeLance(fulado, 1000);

            var qtdeEsperada = 2;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(3, new double[] {40, 800, 200})]
        [InlineData(2, new double[] { 40, 800 })]
        [InlineData(5, new double[] { 40, 800, 200, 500, 400 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizadoCases(int qtdLances, double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if (i%2 == 0)
                {
                    leilao.RecebeLance(fulano, valor);

                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1800);

            var qtdeEsperada = qtdLances;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
