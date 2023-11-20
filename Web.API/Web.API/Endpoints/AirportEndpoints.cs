using Dapper;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using Web.API.Models;
using Web.API.Services;


namespace Web.API.Endpoints
{
    public static class AirportEndpoints
    {
        public static void MapAirportEndpoints(this IEndpointRouteBuilder builder) {
            builder.MapGet("airports", async (SqlConnectionFactory sqlConnectionFactory) => {

                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT ID, IATACode,GeographyLevel1ID,Type from AirportDB";

                var airports = await connection.QueryAsync<Airport>(sql);

                return Results.Ok(airports);
            });

            builder.MapGet("airports/{id}", async(int id, SqlConnectionFactory sqlConnectionFactory ) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                                SELECT Id, IATACode,GeographyLevel1ID,Type
                                From AirportDB
                                Where Id = @AirportId
                                """;

                var airport = await connection.QuerySingleOrDefaultAsync<Airport>(
                    sql,
                    new {AirportId = id});

                return airport is not null ? Results.Ok(airport) : Results.NotFound();
            });

            builder.MapGet("countries", async (SqlConnectionFactory sqlConnectionFactory) => {

                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT ID, Name from CountryDB";

                var countries = await connection.QueryAsync<Country>(sql);

                return Results.Ok(countries);
            });

            builder.MapPost("countries", async (Country country, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();
                const string sql = """
                    INSERT INTO CountryDB (Name)
                    VALUES(@Name)
                """;
                await connection.ExecuteAsync(sql, country);
                return Results.Ok(country);
            });


            //builder.MapPut("countries", async (int Id, Country country, SqlConnectionFactory sqlConnectionFactory) =>
            //{
            //    using var connection = sqlConnectionFactory.Create();

            //    country.Id = Id;

            //    const string sql = """
            //        UPDATE CountryDB
            //        SET Name = @Name'
            //        WHERE Id = @Id
            //    """;
            //    await connection.ExecuteAsync(sql, country);
            //    return Results.Ok(country);
            //});

            //builder.MapDelete("countries/{id}", async (int id, Country country, SqlConnectionFactory sqlConnectionFactory) =>
            //{
            //    using var connection = sqlConnectionFactory.Create();

            //    const string sql = "DELETE FROM CountryDB WHERE Id = @CountryId";

            //    await connection.ExecuteAsync(sql, new { CountryId = id });
            //    return Results.NoContent();
            //});


            builder.MapGet("routes/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                                SELECT Id, RouteName from RouteDB
                                Where Id = @RouteId
                                """;

                var airroute = await connection.QuerySingleOrDefaultAsync<AirRoute>(
                    sql,
                    new { RouteId = id });

                return airroute is not null ? Results.Ok(airroute) : Results.NotFound();
            });
        }
    }
}
