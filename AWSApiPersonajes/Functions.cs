using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using AWSApiPersonajes.Models;
using AWSApiPersonajes.Repositories;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSApiPersonajes;

public class Functions
{
    public RepositoryPersonajes repo;

    public Functions(RepositoryPersonajes repo)
    {
        this.repo = repo;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/api/personajes")]
    public async Task<IHttpResult> GetPersonajes(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Personaje> personajes = await this.repo.GetPersonajesAsync();
        string data = JsonConvert.SerializeObject(personajes);
        return HttpResults.Ok(data);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/api/personajes/{id}")]
    public async Task<IHttpResult> FindPersonaje(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        Personaje personaje = await this.repo.FindPersonajeAsync(id);
        string data = JsonConvert.SerializeObject(personaje);
        return HttpResults.Ok(data);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/api/personajes")]
    public async Task<IHttpResult> PostPersonaje([FromBody] Personaje personaje, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        Personaje personajeNuevo = await this.repo.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen);
        string data = JsonConvert.SerializeObject(personajeNuevo);
        return HttpResults.Ok(data);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Put, "/api/personajes")]
    public async Task<IHttpResult> UpdatePersonaje([FromBody] Personaje personaje, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        await this.repo.UpdatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen);
        return HttpResults.Ok("Actualizado");
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Delete, "/api/personajes/{id}")]
    public async Task<IHttpResult> DeletePersonaje(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        await this.repo.DeletePersonajeAsync(id);
        return HttpResults.Ok("Eliminado");
    }


}
