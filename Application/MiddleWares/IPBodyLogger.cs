public class IPBodyLogger

{
    private readonly RequestDelegate _next;
    private readonly ILogger<IPBodyLogger> _logger;

    public IPBodyLogger(RequestDelegate next, ILogger<IPBodyLogger> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        context.Request.EnableBuffering();

        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

        _logger.LogInformation(ipAddress);
        _logger.LogInformation(requestBody);

        context.Request.Body.Position = 0;


        await _next(context);

        _logger.LogInformation("sadasdda");

    }

}