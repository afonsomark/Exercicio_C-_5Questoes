namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public ContaCorrente() { }
        public ContaCorrente(string idcontacorrente, long numero, string nome, long ativo)
        {
            IdContaCorrente = idcontacorrente;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }

        public string IdContaCorrente { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }
        public long Ativo { get; set; }
    }
}
