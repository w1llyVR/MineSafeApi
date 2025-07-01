using Dapper;
using Domain.Entities;
using Domain.Ports.Output;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Output.Persistence
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _config;

        public UsuarioRepository(IDbConnection connection, IConfiguration configuration)
        {
            this._connection = connection;
            this._config = configuration;
        }

        public async Task<RegistroResponse> ChangePasswordAsync(int codigoVerificacion, string email, string newPassword)
        {
            try
            {
                if (codigoVerificacion != 100)
                {
                    return new RegistroResponse
                    {
                        CodeError = HttpErrorCode.BadRequest,
                        Msj = "Código de verificación inválido"
                    };
                }

                Cryptor cryptor = new Cryptor();
                string encryptedPassword = cryptor.Encrypt(newPassword, Encoding.UTF8.GetBytes("Project2025"));

                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@PasswordHash", encryptedPassword);

                await _connection.ExecuteAsync("dbo.usp_Usuario_ChangePassword", parameters, commandType: CommandType.StoredProcedure);

                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Contraseña actualizada correctamente"
                };
            }
            catch (Exception ex)
            {
                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al cambiar contraseña: {ex.Message}"
                };
            }

        }

        public async Task<RegistroResponse> CreateAsync(Usuario request)
        {
            try
            {
                Cryptor cryptor = new Cryptor();
                var passwordHash = cryptor.Encrypt(request.Password, Encoding.UTF8.GetBytes("Project2025"));
                var parameters = new DynamicParameters();
                parameters.Add("@Nombres", request.Nombres);
                parameters.Add("@Dni", request.Dni);
                parameters.Add("@Email", request.Email);
                parameters.Add("@PasswordHash", (passwordHash));

                await _connection.ExecuteAsync("dbo.usp_Usuario_Create", parameters, commandType: CommandType.StoredProcedure);

                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Usuario registrado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al registrar usuario: {ex.Message}"
                };
            }
        }

        public async Task<Response<UsuarioLogin>> Login(string email, string password)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                var user = await _connection.QueryFirstOrDefaultAsync<Usuario>(
                    "dbo.usp_Usuario_Login", parameters, commandType: CommandType.StoredProcedure);

                if (user == null)
                {
                    return new Response<UsuarioLogin>
                    {
                        CodeError = HttpErrorCode.NotFound,
                        Msj = "Usuario no encontrado"
                    };
                }

                Cryptor cryptor = new Cryptor();
                var decrypted = cryptor.Decrypt(user.PasswordHash, Encoding.UTF8.GetBytes("Project2025"));

                if (decrypted != password)
                {
                    return new Response<UsuarioLogin>
                    {
                        CodeError = HttpErrorCode.BadRequest,
                        Msj = "Contraseña incorrecta"
                    };
                }
                var jwt = new JwtTokenGenerator(_config);
                string token = jwt.GenerateToken(user.Email, user.Nombres, user.Dni);

                var result = new UsuarioLogin
                {
                    Id = user.Id,
                    Nombres = user.Nombres,
                    Dni = user.Dni,
                    Email = user.Email,
                    Token = token
                };

                return new Response<UsuarioLogin>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Login exitoso",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Response<UsuarioLogin>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error durante login: {ex.Message}"
                };
            }
        }
    }
}
