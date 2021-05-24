using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DiamondApi.Data;
using DiamondApi.Entities;
using DiamondApi.Models;

namespace DiamondApi.Controllers
{
    public class TypesController : ApiController
    {
        private DiamondContext db = new DiamondContext();

        public IEnumerable<Domain> GetTypes()
        {
            return db.Types.Select(type => new Domain { Id = type.Id, Name = type.Name }).ToList();
        }
    }
}