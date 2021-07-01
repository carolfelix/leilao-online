using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.TestesUnidade
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentoExceptionDadoValorNegativo()
        {
            var valorNegativo = -100;

            Assert.Throws<ArgumentException>(() => new Lance(null, valorNegativo));
        }
    }
}
