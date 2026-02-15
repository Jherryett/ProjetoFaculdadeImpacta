// Bibliotecas do .NET padrão

global using System;
global using Microsoft.AspNetCore.Mvc; // Desenvolvimento Web APIs
global using Microsoft.AspNetCore.Http; // Desenvolvimento Web APIs
global using Microsoft.Extensions.DependencyInjection; // Fazer as injeções de dependências do sistema
global using System.ComponentModel.DataAnnotations.Schema; // Para poder indicar explicitamente quem é a foregin key nas tabelas
global using System.Threading.Tasks;
global using System.Collections.Generic;



// Bibliotecas do NuGet + link de importação (Importar uma vez para cada projeto)
// SWAGGER - dotnet add package Swashbuckle.AspNetCore

// Bibliotecas para a Idle + link de importação (Importar somente uma vez para a Idle inteira)

global using Microsoft.EntityFrameworkCore; // dotnet tool install --global dotnet-ef 
global using Microsoft.EntityFrameworkCore.Design; // dotnet add package Microsoft.EntityFrameworkCore.Design
global using Microsoft.EntityFrameworkCore.SqlServer; // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// o pacote Tools é importante ter, mas é usado em tempo de design e não é aplicado com Using;   dotnet add package Microsoft.EntityFrameworkCore.Tools




// Arquivos de código (Classes, Entidades)
global using ProjetoLojaDeRoupas.Entities;
global using ProjetoLojaDeRoupas.Interfaces;
global using ProjetoLojaDeRoupas.Services;
global using ProjetoLojaDeRoupas.Repositories;

global using ProjetoLojaDeRoupas.Context;