using ApiRestCrossover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCrossover.Services
{
    public interface IApiAuthService
    {
        public Task<UserModel> User();
    }
}
