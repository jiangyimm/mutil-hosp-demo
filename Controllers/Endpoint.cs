using Microsoft.AspNetCore.Authorization;

[HttpGet("hello"), AllowAnonymous]
sealed class SayHelloEndpoint : EndpointWithoutRequest
{
    public override async Task HandleAsync(CancellationToken c)
    {

        await SendAsync("all jobs queued!");
    }
}