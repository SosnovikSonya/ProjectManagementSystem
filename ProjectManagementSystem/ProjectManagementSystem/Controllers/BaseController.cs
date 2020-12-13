using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Repository;

namespace ProjectManagementSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController(IMapper mapper, IRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        protected IRepository Repository { get; set; }

        protected IMapper Mapper { get; set; }        
    }
}