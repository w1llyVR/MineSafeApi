using Domain.Entities;
using Domain.Ports.Output;
using Shared;
using System.Data;
using Dapper;

namespace Adapters.Output.Persistence
{
    public class TipoCondicionRepository : ITipoCondicionRepository
    {
        private readonly IDbConnection _connection;

        public TipoCondicionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Response<IEnumerable<TipoCondicion>>> GetAllAsync()
        {
            try
            {
                var data = await _connection.QueryAsync<TipoCondicion>("dbo.usp_TipoCondicion_GetAll",
                                                                       commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<TipoCondicion>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Tipo Condicion obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TipoCondicion>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Tipo Condicion : {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Response<IEnumerable<TipoCondicion>>> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                var data = await _connection.QueryAsync<TipoCondicion>("dbo.usp_TipoCondicion_GetById", parameters,commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<TipoCondicion>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Tipo Condicion obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TipoCondicion>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Tipo Condicion : {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<RegistroResponse> CreateAsync(TipoCondicion tipoCondicion)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", tipoCondicion.Nombre);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_TipoCondicion_Insert", parameters, commandType: CommandType.StoredProcedure);

                var codeError = (int)result.codeError;
                var mensaje = (string)result.msj;

                return new RegistroResponse
                {
                    CodeError = codeError == 0 ? HttpErrorCode.Success : HttpErrorCode.BadRequest,
                    Msj = mensaje
                };

            }
            catch (Exception ex)
            {
                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al insertar: {ex.Message}"
                };
            }
        }

        public async Task<RegistroResponse> UpdateAsync(TipoCondicion tipoCondicion)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", tipoCondicion.Id);
                parameters.Add("@Nombre", tipoCondicion.Nombre);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_TipoCondicion_Update", parameters, commandType: CommandType.StoredProcedure);

                var codeError = (int)result.codeError;
                var mensaje = (string)result.msj;

                return new RegistroResponse
                {
                    CodeError = codeError == 0 ? HttpErrorCode.Success : HttpErrorCode.BadRequest,
                    Msj = mensaje
                };

            }
            catch (Exception ex)
            {
                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al insertar: {ex.Message}"
                };
            }
        }

        public async Task<RegistroResponse> DeleteAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>(
                    "dbo.usp_TipoCondicion_Delete",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                var codeError = (int)result.codeError;
                var mensaje = (string)result.msj;

                return new RegistroResponse
                {
                    CodeError = codeError == 0 ? HttpErrorCode.Success : HttpErrorCode.BadRequest,
                    Msj = mensaje
                };
            }
            catch (Exception ex)
            {
                return new RegistroResponse
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al eliminar: {ex.Message}"
                };
            }
        }
    }
}
