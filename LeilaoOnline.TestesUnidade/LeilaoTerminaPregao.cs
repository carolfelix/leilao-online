using System;
using Xunit;

namespace LeilaoOnline.TestesUnidade
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1250, 1270, new double[] { 800, 900, 1270, 1400 })]
        [InlineData(3000, 3400, new double[] { 800, 3400, 5600, 1400 })]
        public void RetornaValorSuperiorMaisProximoDadaNovaLeiDaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new LanceSuperiorMaisProximo(valorDestino);
            var leilao = new Leilao("VanGogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, valor);

                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            var esperado = valorEsperado;
            var obtido = leilao.Ganhador.Valor;

            Assert.Equal(esperado, obtido);
        }
        [Theory]
        [InlineData(1400, new double[] { 800,900,1200,1400})]
        [InlineData(5600, new double[] { 800, 3400, 5600, 1400 })]
        [InlineData(800, new double[] { 800})]
        public void RetornaMaiorValorDadoLeilaoComMaiorLance(double valorEsperado, double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, valor);

                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            var esperado = valorEsperado;
            var obtido = leilao.Ganhador.Valor;

            Assert.Equal(esperado, obtido);
        }

        [Fact]
        public void RetornaLanceDadoLeilaoComApenasUmLance()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            var fulado = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulado, 800);

            leilao.TerminaPregao();

            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaExecaoQuandoTerminaPregaoEChamadoSemInicializarPregao()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            var ex = Assert.Throws<System.InvalidOperationException>(() => leilao.TerminaPregao());

            var msgEx = "Não é possível terminar pregrão sem antes inicializa-lo.";

            Assert.Equal(msgEx, ex.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("VanGogh", modalidade);
            leilao.IniciaPregao();
            leilao.TerminaPregao();

            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
