using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int numero;
        public string titular;
        public double depositoInicial;
        public double saldo;

        public ContaBancaria(int numero, string titular)
        {
            this.numero = numero;
            this.titular = titular;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial, double saldo)
        {
            this.numero = numero;
            this.titular = titular;
            this.depositoInicial = depositoInicial;
            this.saldo = saldo + this.depositoInicial;
        }

        public ContaBancaria Deposito(double quantia, ContaBancaria conta)
        {
            if (quantia > 0) {
                conta.saldo += quantia;
                return conta;
            }
            else
            {
                return conta;
            }
        }

        public ContaBancaria Saque(double quantia, ContaBancaria conta)
        {
            if (quantia > 0)
            {
                conta.saldo -= quantia + 3.50;
                return conta;
            }
            else
            {
                return conta;
            }
        }
    }
}
