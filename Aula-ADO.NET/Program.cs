
using Aula_ADO.NET;
using Microsoft.Data.SqlClient;

//Informar a conexão do banco 
var connection = new SqlConnection(DBConnection.GetConnectionString());

#region Insert

var pessoa = new Pessoa("Maria Silva", "78965412300", new DateOnly(1985, 6, 6));

var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, datanascimento) VALUES (@Nome, @CPF, @DataNascimento); SELECT SCOPE_IDENTITY();";


connection.Open();

var command = new SqlCommand(sqlInsertPessoa, connection);

command.Parameters.AddWithValue("@Nome", pessoa.Nome);
command.Parameters.AddWithValue("@CPF", pessoa.Cpf);
command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

//command.ExecuteNonQuery();  //>> Usado para fazer o Insert de forma direta, sem retornar nenhuma valor.

//int pessoaId = Convert.ToInt32(command.ExecuteScalar());   // >> Usado para fazer o Insert e retornar o primeiro valor da primeira coluna (Id)

//var telefone = new Telefone("11", "945218569", "Celular", pessoaId);

//var sqlInsertTelefone = $"INSERT INTO Telefones(ddd, numero, tipo, pessoaId) VALUES (@DDD, @Numero, @Tipo, @PessoaId);";
//command = new SqlCommand(sqlInsertTelefone, connection);
//command.Parameters.AddWithValue("@ddd", telefone.DDD);
//command.Parameters.AddWithValue("@Numero", telefone.Numero);
//command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
//command.Parameters.AddWithValue("@PessoaID", telefone.PessoaId);

//command.ExecuteNonQuery();

connection.Close();
#endregion

#region SelectPessoa
connection.Open();

var sqlSelectPessoas = "SELECT p.id, p.nome, p.cpf, p.dataNascimento " +
                       "FROM Pessoas p";

command = new SqlCommand(sqlSelectPessoas, connection);

var reader = command.ExecuteReader();

while (reader.Read())
{
    var pessoaListada = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
        );
    pessoaListada.setId(reader.GetInt32(0));


    Console.WriteLine($"\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
    Console.WriteLine($"\n{pessoaListada}\n");
}
reader.Close();

connection.Close();
#endregion

#region SelectPessoaTelefone
connection.Open();

var sqlSelectPessoasTel = "SELECT p.id, p.nome, p.cpf, p.dataNascimento, t.id, t.ddd, t.numero, t.tipo, t.pessoaId " +
                       "FROM Pessoas p " +
                       "JOIN Telefones t " +
                       "ON t.pessoaId = p.id";

command = new SqlCommand(sqlSelectPessoasTel, connection);

reader = command.ExecuteReader();

while (reader.Read())
{
    var pessoaListada = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
        );
    pessoaListada.setId(reader.GetInt32(0));
    var telefoneListado = new Telefone(
        reader.GetString(5),
        reader.GetString(6),
        reader.GetString(7),
        reader.GetInt32(8)
        );

    Console.WriteLine($"\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
    Console.WriteLine($"\n{pessoaListada}\n{telefoneListado}");
}
reader.Close();

connection.Close();
#endregion

#region Update

connection.Open();

var sqlUpdatePessoa = "UPDATE Pessoas SET nome = @Nome WHERE id = @Id;";

command = new SqlCommand(sqlUpdatePessoa, connection);
command.Parameters.AddWithValue("@Nome", "Felipe Silva");
command.Parameters.AddWithValue("Id", 1);

command.ExecuteNonQuery();
connection.Close();
#endregion

#region Delete
connection.Open();

var sqlDeletePessoa = "DELETE FROM Pessoas WHERE id = @Id;";

command = new SqlCommand(sqlDeletePessoa, connection);
command.Parameters.AddWithValue("Id", 2);
command.ExecuteNonQuery();

connection.Close();

#endregion

