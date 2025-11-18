using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_ADO.NET
{
    internal class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateOnly DataNascimento { get; private set; }

        public List<Telefone> Telefones { get; private set; } = new List<Telefone>();


        public Pessoa(string nome, string cpf, DateOnly dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public void setId(int id) 
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nCPF: {Cpf}\nData de Nascimento: {DataNascimento}";
        }

    }
}
