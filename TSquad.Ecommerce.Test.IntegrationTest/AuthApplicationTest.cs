using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface;
using TSquad.Ecommerce.Application.Interface.UseCases;

namespace TSquad.Ecommerce.Test.IntegrationTest;

[TestClass]
public class AuthApplicationTest
{
    
    private static WebApplicationFactory<Program> _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    
    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        // var factory = new WebApplicationFactory<Program>();
        // _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    }
    
    [TestMethod]
    public async Task SignIn_CuandoNoSeEnvianParametros_RetornaMensajeErrorValidaciontes()
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IAuthApplication>();

        var signInDto = new SignInDto()
        {
            Email = "",
            Password = "",
        };
        var excepted = "Errores de validación";

        var result = await service.SignInAsync(signInDto);
        var actual = result.Message;
        
        Assert.AreEqual(excepted, actual);
    }
    
    [TestMethod]
    public async Task SignIn_CuandoNoSeEnvianParametrosIncorrectos_RetornaMensajeUsuarioNoExiste()
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IAuthApplication>();

        var signInDto = new SignInDto()
        {
            Email = "Prueba@google.com",
            Password = "Contraseña",
        };
        var excepted = "User not found.";

        var result = await service.SignInAsync(signInDto);
        var actual = result.Message;
        
        Assert.AreEqual(excepted, actual);
    }
    
    [TestMethod]
    public async Task SignIn_CuandoNoSeEnvianParametrosCorrectos_RetornaMensajeExitoso()
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IAuthApplication>();

        var signInDto = new SignInDto()
        {
            Email = "lcruz@yopmail.com",
            Password = "Admin123.",
        };
        var excepted = "Token created successfully.";

        var result = await service.SignInAsync(signInDto);
        var actual = result.Message;

        Assert.AreEqual(excepted, actual);
    }
}