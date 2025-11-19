using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_ADO.NET
{
    internal class Telefone
    {

        public int Id { get; private set; }
        public string DDD { get; private set; }
        public string Numero { get; private set; }
        public string Tipo { get; private set; }
        public int PessoaId { get; private set; }
        

        public Telefone(string ddd, string numero, string tipo, int pessoaId)
        {
            DDD = ddd;
            Numero = numero;
            Tipo = tipo;
            PessoaId = pessoaId;
        }

        public override string ToString() 
        {
            return $"\nDDD: {DDD}\nNúmero: {Numero}\nTipo: {Tipo}";
        }

    }
}
