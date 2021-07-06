using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware {

    private readonly RequestDelegate Next;

    public ErrorHandlingMiddleware (RequestDelegate next){        
        this.Next = next;

    }

    public async Task Invoke (HttpContext context){
        try{
         Console.WriteLine("teste");
         await Next(context);
        } 
        catch (Exception ex) { 
           await HandleExeptionAsync(context, ex);
        }
               
    }

    private static Task HandleExeptionAsync (HttpContext context, Exception ex){
        var code = HttpStatusCode.InternalServerError;
        if (ex is Exception){
            code = HttpStatusCode.NotFound;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code; //vonverti ststaus code para inteiro
        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
    }
    

}