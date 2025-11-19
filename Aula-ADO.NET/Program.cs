
using Aula_ADO.NET;
using Microsoft.Data.SqlClient;


//Informar a conexão do banco 
var connection = new SqlConnection(DBConnection.GetConnectionString());

#region Insert
//INSERT PESSOA
var pessoa = new Pessoa("Sophia Oliveira", "75395185200", new DateOnly(1996, 12, 15));
var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, datanascimento) VALUES (@Nome, @CPF, @DataNascimento); SELECT SCOPE_IDENTITY();";

connection.Open();

var command = new SqlCommand(sqlInsertPessoa, connection);

command.Parameters.AddWithValue("@Nome", pessoa.Nome);
command.Parameters.AddWithValue("@CPF", pessoa.Cpf);
command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

int pessoaId = Convert.ToInt32(command.ExecuteScalar());   //Usado para fazer o Insert e retornar o primeiro valor da primeira coluna (neste caso: Id)

//INSERT TELEFONE
var telefone = new Telefone("11", "9999653258", "Celular", pessoaId);

var sqlInsertTelefone = $"INSERT INTO Telefones(ddd, numero, tipo, pessoaId) VALUES (@ddd, @Numero, @Tipo, @PessoaId);";
command = new SqlCommand(sqlInsertTelefone, connection);
command.Parameters.AddWithValue("@ddd", telefone.DDD);
command.Parameters.AddWithValue("@Numero", telefone.Numero);
command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
command.Parameters.AddWithValue("@PessoaID", telefone.PessoaId);

command.ExecuteNonQuery();  //Usado para fazer o Insert de forma direta, sem retornar nenhuma valor.

//INSERT ENDEREÇO
var endereco = new Endereco("Estrada Rural Antonio Carlos", null, "Sitio Arco-Iris", "Bairro Rural", "São Paulo", "SP", "11604774", pessoaId);

var sqlInsertEndereco = $"INSERT INTO Enderecos(logradouro, numero, complemento, bairro, cidade, estado, cep, pessoaId) VALUES (@Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Cep, @PessoaId)";
command = new SqlCommand(sqlInsertEndereco, connection);
command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
command.Parameters.AddWithValue("@Numero", endereco.Numero == null ? DBNull.Value : endereco.Numero);
command.Parameters.AddWithValue("@Complemento", endereco.Complemento == null ? DBNull.Value : endereco.Complemento);
command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
command.Parameters.AddWithValue("@Cidade", endereco.Cidade);
command.Parameters.AddWithValue("@Estado", endereco.Estado);
command.Parameters.AddWithValue("@Cep", endereco.Cep);
command.Parameters.AddWithValue("@PessoaId", endereco.PessoaId);

command.ExecuteNonQuery();

connection.Close();
#endregion

#region SelectPessoas
connection.Open();

var sqlSelectPessoa = $"SELECT id, nome, cpf, dataNascimento FROM Pessoas";

command = new SqlCommand(sqlSelectPessoa, connection);

var reader = command.ExecuteReader();

Console.WriteLine($"\n=-=-=-=-=-=-=-= LISTA DE PESSOAS =-=-=-=-=-=-=-=");
while (reader.Read()) 
{
    pessoa = new Pessoa
    (
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))       
    );
    pessoa.SetId(reader.GetInt32(0));

    Console.WriteLine($"{pessoa}\n");

}


connection.Close();
#endregion

#region SelectPessoasTelefone
connection.Open();

var sqlSelectPessoasTelefone = "SELECT p.id, p.nome, p.cpf, p.dataNascimento, t.ddd, t.numero, t.tipo, t.pessoaId " +
                       "FROM Pessoas p " +
                       "JOIN Telefones t " +
                       "ON t.pessoaId = p.id";

command = new SqlCommand(sqlSelectPessoasTelefone, connection);

reader = command.ExecuteReader();

var pessoasTelefone = new List<Pessoa>();

var telefones = new List<Telefone>();

Console.WriteLine($"\n=-=-=-=-=-=LISTA DE PESSOAS E TELEFONES=-=-=-=-=-=");
while (reader.Read())
{
    var idPessoa = reader.GetInt32(0);

    telefones.Add(new Telefone(
        reader.GetString(4),
        reader.GetString(5),
        reader.GetString(6),
        reader.GetInt32(7)
    ));

    if(pessoasTelefone.Any(p => p.Id == idPessoa))
    {
        var pessoalistada = pessoasTelefone.Find(p => p.Id == idPessoa);
        pessoalistada.Telefones.Add(telefones.Last());
        continue;
    }

    var novaPessoa = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
    );

    novaPessoa.SetId(idPessoa);
    novaPessoa.Telefones.Add(telefones.Last());

    pessoasTelefone.Add(novaPessoa);


}

foreach (var p in pessoasTelefone) 
{
    Console.WriteLine(p);
}

connection.Close();
#endregion

#region SelectPessoasEndereço
connection.Open();

var sqlSelectPessoasEndereco = $"SELECT p.id, p.nome, p.cpf, p.dataNascimento, " +
                            $"e.logradouro, e.numero, e.complemento, e.bairro, e.cidade, e.estado, e.cep, e.pessoaId " +
                            $"FROM Pessoas p " +
                            $"LEFT JOIN Enderecos e " +
                            $"ON p.id = e.pessoaId";

command = new SqlCommand(sqlSelectPessoasEndereco, connection);

reader = command.ExecuteReader();

var pessoasEndereco = new List<Pessoa>();
var enderecos = new List<Endereco>();

Console.WriteLine($"\n=-=-=-=-=-= LISTA DE PESSOAS E ENDERECOS =-=-=-=-=-=");
while (reader.Read()) 
{
    var idPessoa = reader.GetInt32(0);

    enderecos.Add(new Endereco(
        reader.IsDBNull(4) ? null : reader.GetString(4),
        reader.IsDBNull(5) ? null : reader.GetInt32(5),
        reader.IsDBNull(6) ? null : reader.GetString(6),
        reader.IsDBNull(7) ? null : reader.GetString(7),
        reader.IsDBNull(8) ? null : reader.GetString(8),
        reader.IsDBNull(9) ? null : reader.GetString(9),
        reader.IsDBNull(10) ? null : reader.GetString(10),
        reader.IsDBNull(11) ? null : reader.GetInt32(11)
    ));

    if (enderecos.Any(e => e.Id == idPessoa)) 
    {
        var pessoaListada = pessoasEndereco.Find(p => p.Id == idPessoa);
        pessoaListada.Enderecos.Add(enderecos.Last());
        continue;      
    }

    var novaPessoa = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
    );
    novaPessoa.SetId(idPessoa);

    novaPessoa.Enderecos.Add(enderecos.Last());

    pessoasEndereco.Add(novaPessoa);
}

foreach(var p in pessoasEndereco)
{
    Console.WriteLine(p);
}

connection.Close();
#endregion

#region Update

connection.Open();

var sqlUpdatePessoa = "UPDATE Pessoas SET nome = @Nome WHERE id = @Id;";

command = new SqlCommand(sqlUpdatePessoa, connection);
command.Parameters.AddWithValue("@Nome", "Clarice Costa");
command.Parameters.AddWithValue("Id", 1);

//command.ExecuteNonQuery();
connection.Close();
#endregion

#region Delete
connection.Open();

var sqlDeletePessoa = "DELETE FROM Pessoas WHERE id = @Id;";

command = new SqlCommand(sqlDeletePessoa, connection);
command.Parameters.AddWithValue("Id", 2);
//command.ExecuteNonQuery();

connection.Close();

#endregion

