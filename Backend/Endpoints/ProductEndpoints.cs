using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints;

public static class ProductEndpoints
{
	public static void MapProductEndpoints(this WebApplication webApplication, [FromServices] LocalDbContext localDbContext)
	{
		webApplication.MapGet("/products", async () =>
		{
			return await localDbContext.Products.ToListAsync();
		});

		webApplication.MapPost("/products", async ([FromBody] Product newProduct) =>
		{
			localDbContext.Products.Add(newProduct);
			await localDbContext.SaveChangesAsync();
			return Results.Created("/products", newProduct.Id);
		});
	}
}
