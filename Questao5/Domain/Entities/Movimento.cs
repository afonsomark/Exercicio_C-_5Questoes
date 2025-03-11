using Questao5.Domain.Enumerators;
using System.Drawing;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Movimento() { }
        public Movimento(string idmovimento, string idcontacorrente, string datamovimento, TipoMovimento tipomovimento, decimal valor)
        {
            IdMovimento = idmovimento;
            IdContaCorrente = idcontacorrente;
            DataMovimento = datamovimento;
            TipoMovimento = tipomovimento;
            Valor = valor;
        }

        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
