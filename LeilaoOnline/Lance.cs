using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoOnline
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
            {
                 throw new System.ArgumentException("Valor não pode ser menor que 0");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}
