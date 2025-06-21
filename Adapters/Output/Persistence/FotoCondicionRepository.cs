using Dapper;
using Domain.Entities;
using Domain.Ports.Output;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Output.Persistence
{
    public class FotoCondicionRepository : IFotoCondicionRepository
    {
        private readonly IDbConnection _connection;

        public FotoCondicionRepository(IDbConnection connection)
        {
            this._connection = connection;
        }
        public async Task<Response<IEnumerable<FotoCondicion>>> GetAllAsync()
        {
            try
            {
                var data = await _connection.QueryAsync<FotoCondicion>("dbo.usp_FotoCondicion_GetAll",
                                                                       commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<FotoCondicion>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Foto Condicion obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FotoCondicion>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los Foto Condicion: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Response<IEnumerable<FotoCondicion>>> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var data = await _connection.QueryAsync<FotoCondicion>("dbo.usp_FotoCondicion_GetById", parameters, commandType: CommandType.StoredProcedure);

                return new Response<IEnumerable<FotoCondicion>>
                {
                    CodeError = HttpErrorCode.Success,
                    Msj = "Foto Condicion obtenidos exitosamente.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FotoCondicion>>
                {
                    CodeError = HttpErrorCode.InternalServerError,
                    Msj = $"Error al obtener los FotoCondicion: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<RegistroResponse> CreateAsync(FotoCondicion request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ReporteActaId", request.ReporteActaId);
                parameters.Add("@TipoCondicionId", request.TipoCondicionId);
                parameters.Add("@Ruta", request.Ruta);
                parameters.Add("@NivelRiesgo", request.NivelRiesgo);
                parameters.Add("@Sincronizada", request.Sincronizada);
                parameters.Add("@Descripcion", request.Descripcion);
                parameters.Add("@FechaCaptura", request.FechaCaptura);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_FotoCondicion_Insert", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<RegistroResponse> UpdateAsync(FotoCondicion request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id);
                parameters.Add("@ReporteActaId", request.ReporteActaId);
                parameters.Add("@TipoCondicionId", request.TipoCondicionId);
                parameters.Add("@Ruta", request.Ruta);
                parameters.Add("@NivelRiesgo", request.NivelRiesgo);
                parameters.Add("@Sincronizada", request.Sincronizada);
                parameters.Add("@Descripcion", request.Descripcion);
                parameters.Add("@FechaCaptura", request.FechaCaptura);


                var result = await _connection.QueryFirstOrDefaultAsync<dynamic>("dbo.usp_FotoCondicion_Update", parameters, commandType: CommandType.StoredProcedure);

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
                    "dbo.usp_FotoCondicion_Delete",
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
