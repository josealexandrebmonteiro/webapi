using Microsoft.AspNetCore.Mvc;

[ApiController] //indica que sera http
[Route ("[controller]")] // rota base /movie

public class MovieController : ControllerBase
{
    public MovieController( )
    {
        
    }

    [HttpPost]
    public string Post ()
    {
	    return "Post (Criação)";
    }

    [HttpPut]
    public string Put ()
    {
	    return "Put (Alteração)";
    }

    [HttpGet]
    public string Get ()
    {
	    return "Get (Read)";
    }

    [HttpDelete]
    public string Delete ()
    {
	    return "Delete (Delete)";
    }

}