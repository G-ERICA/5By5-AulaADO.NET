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
        public List<Endereco> Enderecos { get; private set; } = new List<Endereco>();


        public Pessoa(string nome, string cpf, DateOnly dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public int SetId(int id) 
        {
            return Id = id;
        }

        public override string ToString()
        {
            string resultado = $"Id: {Id}\nNome: {Nome}\nCPF: {Cpf}\nData de Nascimento: {DataNascimento}";
            if(Telefones is not null) 
            {
                foreach(Telefone t in this.Telefones) 
                {
                    resultado += t.ToString();
                }
                resultado += "\n";
                foreach(Endereco e in this.Enderecos) 
                {
                    resultado += e.ToString();
                }
                resultado += "\n";
            }
            return resultado;
        }

    }
}
