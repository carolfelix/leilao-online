using System;
using Xunit;

namespace LeilaoOnline.TestesUnidade
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1400, new double[] { 800,900,1200,1400})]
        [InlineData(5600, new double[] { 800, 3400, 5600, 1400 })]
        [InlineData(800, new double[] { 800})]
        public void RetornaMaiorValorDadoLeilaoComMaiorLance(double valorEsperado, double[] ofertas)
        {
            var leilao = new Leilao("VanGogh");
            var fulado = new Interessada("Fulano", leilao);

            foreach (var oferta in ofertas)
            {
                leilao.RecebeLance(fulado, oferta);
            }

            leilao.TerminaPregao();

            var esperado = valorEsperado;
            var obtido = leilao.Ganhador.Valor;

            Assert.Equal(esperado, obtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            var leilao = new Leilao("VanGogh");
            var fulado = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulado, 800);

            leilao.TerminaPregao();

            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LeilaoSemLances()
        {
            var leilao = new Leilao("VanGogh");

            leilao.TerminaPregao();

            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
