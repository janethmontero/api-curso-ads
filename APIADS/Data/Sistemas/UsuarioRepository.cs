using APIADS.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APIADS.Data.Sistemas
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetUsuarios(int CveEmpresa, int? CveUsuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Sis_UsuariosGet]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CveEmpresa", CveEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@CveUsuario", CveUsuario ?? (object)DBNull.Value));

                    var response = new List<Usuario>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        response = Helpers.MapperReader.CreateList<Usuario>(reader);
                    }
                    return response;
                }
            }
        }

        public async Task<int> InsertUsuario(int CveEmpresa, Usuario usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Sis_UsuarioSave]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CveEmpresa", CveEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", usuario.NombreUsuario));
                    cmd.Parameters.Add(new SqlParameter("@Email", usuario.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", usuario.Password));

                    await sql.OpenAsync();

                    int id = 0;
                    object identity = await cmd.ExecuteScalarAsync();
                    if (identity != null)
                    {
                        int.TryParse(identity.ToString(), out id);
                    }

                    return id;
                }
            }
        }

        public async Task<int> UpdatetUsuario(int CveUsuario, Usuario usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Sis_UsuarioUpdate]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CveUsuario", CveUsuario));
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", usuario.NombreUsuario));
                    cmd.Parameters.Add(new SqlParameter("@Email", usuario.Email));
                    cmd.Parameters.Add(new SqlParameter("@Estatus", usuario.Estatus));

                    await sql.OpenAsync();

                    int id = 0;
                    object identity = await cmd.ExecuteScalarAsync();
                    if (identity != null)
                    {
                        int.TryParse(identity.ToString(), out id);
                    }

                    return id;
                }
            }
        }

        public async Task<int> DeleteUsuario(int CveUsuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Sis_UsuarioDelete]", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CveUsuario", CveUsuario));
                    await sql.OpenAsync();
                    int id = 0;
                    object identity = await cmd.ExecuteScalarAsync();
                    if (identity != null)
                    {
                        int.TryParse(identity.ToString(), out id);
                    }

                    return id;
                }
            }
        }
    }
}
