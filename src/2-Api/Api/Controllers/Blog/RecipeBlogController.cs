﻿using Api.Controllers.Configuration.Response;
using Blog.Business.Interfaces.Services;
using Blog.Models.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Blog;

[Route("api/recipe-blog")]
public class RecipeBlogController : MainController
{
    private readonly IRecipeBlogService _service;
    public RecipeBlogController(IRecipeBlogService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id)
    {
        var result = await _service.GetRecipeById(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetRecipes()
    {
        var result = await _service.GetRecipes();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpPost]
    public async Task<IActionResult> PostRecipe(RecipeBlog recipe)
    {
        await _service.AddRecipe(recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
}
