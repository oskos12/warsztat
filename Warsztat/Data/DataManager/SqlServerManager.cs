using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text;
using System.Xml;
using Warsztat.Contracts;
using Warsztat.Data.Entity;
using Z.Dapper.Plus;

namespace Warsztat.Data.DataManager
{
    public class SqlServerManager : ISqlServerManager
    {
        private readonly string _connectionString;
        private readonly IWorkshopDbContext _workshopDbContext;
        private readonly List<Dictionary> _dictionary;
        public SqlServerManager(IWorkshopDbContext workshopDbContext)
        {
            _workshopDbContext = workshopDbContext;
            _dictionary = Task.FromResult(_workshopDbContext.DictionarySet.ToList()).Result;
            _connectionString = "Server=dbijak.ddns.net,49703\\SQLEXPRESS;Database=Warsztat;user=api;password=TXPA8cGa";
        }
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await Task.FromResult(_workshopDbContext.UsersSet.ToList());
        }
        public IEnumerable<Clients> GetAllClients()
        {
            List<Clients> list = new();
            var cmdText = @"select * from dbo.ClientsSet";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(cmdText, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    list.Add(new Clients
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        City = reader["City"].ToString(),
                        Post = reader["Post"].ToString(),
                        Address = reader["Address"].ToString(),
                        Active = (bool)reader["Active"],//.ToString() == "1" ? true : false,
                        Users_Id = (int)reader["Users_Id"]
                    });
                }
            }
            return list;
        }
        public async Task<IEnumerable<Clients>> GetAllClientsEF()
        {
            return await Task.FromResult(_workshopDbContext.ClientsSet);
        }
        public IEnumerable<Clients> GetAllClientsDapper()
        {
            List<Clients> list = new();
            var cmdText = @"select * from dbo.ClientsSet";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                list = connection.Query<Clients>(cmdText).ToList();
                
            }
            return list;
        }
        public async Task<int> RegisterUser(string login, string password, string email)
        {
            var user = new Users { Login = login, Password = password, Email = email, Active = true, DictionaryType_Id = _dictionary.Where(x => x.Key == "Typ konta" && x.Value == "User").First().Id };
            try
            {
                await _workshopDbContext.UsersSet.AddAsync(user);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(user.Id);
        }
        public async Task<int> AddNewClient(string name, string surname, string phoneNumber, string city, string post, string address, string email)
        {
            //var user = await RegisterUser(email, surname + phoneNumber.Substring(0, 3), email);
            //if (user < 0)
            //    return -1;
            var client = new Clients { Name = name, Surname = surname, PhoneNumber = phoneNumber, City = city, Post = post, Address = address, Active = true, Users_Id = 0 };
            try
            {
                await _workshopDbContext.ClientsSet.AddAsync(client);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(client.Id);
        }

        public List<Clients> CreateClientList(int count)
        {
            List<Clients> list = new();
            for (int i = 0; i < count; i++)
            {
                list.Add(new Clients { Name = "Jan" + i, Surname = "Kowalski" + i, PhoneNumber = i.ToString(), City = "Warszawa", Post = "Post", Address = "Random address", Active = true, Users_Id = 2 });
            }
            return list;
        }
        public async Task<int> AddCountOfClientsSimple(int count)
        {
            List<Clients> list = CreateClientList(count);

            var cmdText = @"insert into dbo.ClientsSet values (@name, @surname, @phonenumber, @city, @post, @address, @active, @userId)";
            try
            {
                foreach (Clients client in list)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        var command = new SqlCommand(cmdText, connection);
                        command.Parameters.AddWithValue("@name", client.Name);
                        command.Parameters.AddWithValue("@surname", client.Surname);
                        command.Parameters.AddWithValue("@phonenumber", client.PhoneNumber);
                        command.Parameters.AddWithValue("@city", client.City);
                        command.Parameters.AddWithValue("@post", client.Post);
                        command.Parameters.AddWithValue("@address", client.Address);
                        command.Parameters.AddWithValue("@active", client.Active);
                        command.Parameters.AddWithValue("@userId", client.Users_Id);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsConnection(int count)
        {
            List<Clients> list = CreateClientList(count);

            var cmdText = @"insert into dbo.ClientsSet values (@name, @surname, @phonenumber, @city, @post, @address, @active, @userId)";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    foreach (Clients client in list)
                    {
                        var command = new SqlCommand(cmdText, connection);
                        command.Parameters.AddWithValue("@name", client.Name);
                        command.Parameters.AddWithValue("@surname", client.Surname);
                        command.Parameters.AddWithValue("@phonenumber", client.PhoneNumber);
                        command.Parameters.AddWithValue("@city", client.City);
                        command.Parameters.AddWithValue("@post", client.Post);
                        command.Parameters.AddWithValue("@address", client.Address);
                        command.Parameters.AddWithValue("@active", client.Active);
                        command.Parameters.AddWithValue("@userId", client.Users_Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsStringBuilder(int count)
        {
            List<Clients> list = CreateClientList(count);

            //var cmdText = @"insert into dbo.ClientsSet values (@name, @surname, @phonenumber, @city, @post, @address, @active, @userId)";
            var cmdText = list.Aggregate(new StringBuilder(), (sb, client) =>
            sb.AppendLine(@$"insert into dbo.ClientsSet values ('{client.Name}', '{client.Surname}', '{client.PhoneNumber}', '{client.City}', '{client.Post}', '{client.Address}', '{client.Active}', {client.Users_Id})"));
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(cmdText.ToString(), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsEF(int count)
        {
            List<Clients> list = CreateClientList(count);
            try
            {
                foreach (Clients client in list)
                {
                    _workshopDbContext.ClientsSet.Add(client);
                }
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }
        public async Task<int> AddCountOfClientsEFRange(int count)
        {
            List<Clients> list = CreateClientList(count);
            try
            {
                _workshopDbContext.ClientsSet.AddRange(list);
                
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsDapperSimple(int count)
        {
            List<Clients> list = CreateClientList(count);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmdText = @"insert into dbo.ClientsSet values (@name, @surname, @phonenumber, @city, @post, @address, @active, @Users_Id)";
                    foreach (var client in list)
                    {
                        connection.Execute(cmdText, client);
                    }
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsDapperList(int count)
        {
            List<Clients> list = CreateClientList(count);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmdText = @"insert into dbo.ClientsSet values (@name, @surname, @phonenumber, @city, @post, @address, @active, @Users_Id)";
                    connection.Execute(cmdText, list);
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }

        public async Task<int> AddCountOfClientsDapperBulk(int count)
        {
            List<Clients> list = CreateClientList(count);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.BulkInsert(list);
                }
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(1);
        }
    }
}
