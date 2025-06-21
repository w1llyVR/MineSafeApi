using Dapper;
using Domain.Entities;
using Domain.Ports.Output;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Output.Persistence
{
    public class MedidaCorrectivaRepository : IMedidaCorrectivaRepository
    {
        private readonly IDbConnection _connection;

        public MedidaCorrectivaRepository(IDbConnection connection)
        {
            this._connection = connection;
        }

        public async Task<Response<IEnumerable<MedidaCorrectiva>>> GetAllAsync()
        {
            try
            {
                var data = await _connection.QueryAsync<MedidaCorrectiva>("dbo.usp_MedidaCorrectiva_GetAll",
                                                                       commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<MedidaCorrectiva>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Medida Correctiva obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<MedidaCorrectiva>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Medida Correctiva: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Response<IEnumerable<MedidaCorrectiva>>> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var data = await _connection.QueryAsync<MedidaCorrectiva>("dbo.usp_MedidaCorrectiva_GetById", parameters, commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<MedidaCorrectiva>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Medida Correctiva obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<MedidaCorrectiva>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Medida Correctiva: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<RegistroResponse> CreateAsync(MedidaCorrectiva request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ReporteActaId", request.ReporteActaId);
                parameters.Add("@Contenido", request.Contenido);
                parameters.Add("@Origen", request.Origen);
                parameters.Add("@FechaGeneracion", request.FechaGeneracion);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_MedidaCorrectiva_Insert", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<RegistroResponse> UpdateAsync(MedidaCorrectiva request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id);
                parameters.Add("@ReporteActaId", request.ReporteActaId);
                parameters.Add("@Contenido", request.Contenido);
                parameters.Add("@Origen", request.Origen);
                parameters.Add("@FechaGeneracion", request.FechaGeneracion);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_MedidaCorrectiva_Update", parameters, commandType: CommandType.StoredProcedure);

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
                    "dbo.usp_MedidaCorrectiva_Delete",
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
