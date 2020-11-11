using APIADS.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APIADS.Data.Auth
{
    public class AuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Usuario> Login(string nombreUsuario, string password)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Sis_Login]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = password });
                    var response = new Usuario();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = Helpers.MapperReader.CreateObject<Usuario>(reader);
                    }
                    return response;
                }
            }
        }
    }
}
