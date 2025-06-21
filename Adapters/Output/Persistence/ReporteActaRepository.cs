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
    public class ReporteActaRepository : IReporteActaRepository
    {
        private readonly IDbConnection _connection;

        public ReporteActaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Response<IEnumerable<ReporteActa>>> GetAllAsync()
        {
            try
            {
                var data = await _connection.QueryAsync<ReporteActa>("dbo.usp_ReporteActa_GetAll",
                                                                       commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<ReporteActa>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Reporte Acta obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReporteActa>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Reporte Actas : {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Response<IEnumerable<ReporteActa>>> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var data = await _connection.QueryAsync<ReporteActa>("dbo.usp_ReporteActa_GetById", parameters, commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<ReporteActa>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Reporte Acta obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReporteActa>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Reporte Acta: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<int> CreateAsync(ReporteActa request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UsuarioId", request.UsuarioId);
                parameters.Add("@FechaCreacion", request.FechaCreacion);
                parameters.Add("@Observaciones", request.Observaciones);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_ReporteActa_Insert", parameters, commandType: CommandType.StoredProcedure);

                var codeError = (int)result.codeError;
                var mensaje = (string)result.msj;
                var id = (int)result.id;

                return id;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<RegistroResponse> UpdateAsync(ReporteActa request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id);
                parameters.Add("@UsuarioId", request.UsuarioId);
                parameters.Add("@FechaCreacion", request.FechaCreacion);
                parameters.Add("@Observaciones", request.Observaciones);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_ReporteActa_Update", parameters, commandType: CommandType.StoredProcedure);

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
                    "dbo.usp_ReporteActa_Delete",
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
