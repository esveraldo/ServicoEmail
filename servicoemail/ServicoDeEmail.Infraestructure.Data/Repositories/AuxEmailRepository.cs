using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServicoDeEmail.Domain.Entities;
using System.Data.Common;
using System.Numerics;
using Thrift.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServicoDeEmail.Infraestructure.Data.Repositories
{
    public class AuxEmailRepository
    {
        private readonly string? _connectionString;
        public AuxEmailRepository(string? connectionString)
        {
            _connectionString = connectionString;
        }


        public void Update(Guid emailId, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    UPDATE Emails 
                    SET
                        Status = @status
                    WHERE
                        Id = @emailId
                ";

                connection.Execute(query, new { emailId, status });
            }
        }

        public void UpdateEmailObs(Guid emailId, string obs)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    UPDATE Emails 
                    SET
                        Obs = @obs
                    WHERE
                        Id = @emailId
                ";

                connection.Execute(query, new { emailId, obs });
            }
        }

        public void UpdateCounterConsumerUser(Guid id, int counter)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                    UPDATE ConsumerUsers
                    SET
                        Counter = @counter
                    WHERE
                        Id = @id
                ";

                    connection.Execute(query, new { id, counter });
                }
            }
            catch (Exception e)
            {

                throw new Exception($"Houve um erro no ao incrementar o contador: {e}");
            }
        }

        public void UpdateSendDateEmails(Guid id, DateTime sendDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                    UPDATE Emails
                    SET
                        SendDate = @sendDate
                    WHERE
                        Id = @id
                ";

                    connection.Execute(query, new { id, sendDate });
                }
            }
            catch (Exception e)
            {

                throw new Exception($"Houve um erro no ao incrementar o contador: {e}");
            }
        }

        public void UpdateDataCounter(Guid id, int counter, DateTime currentDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("UPDATE ConsumerUsers SET Counter = @Counter, CurrentDate = @CurrentDate WHERE Id = @Id",
                    param: new { Id = id, Counter = counter, CurrentDate = currentDate });
                }
            }
            catch (Exception e)
            {

                throw new Exception($"Houve um erro no ao atualizar o contador: {e}");
            }
        } 
    }
}
