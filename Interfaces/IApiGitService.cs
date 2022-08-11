using ApiRestCrossover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCrossover.Interfaces
{
    public interface IApiGitService
    {
        public Task<List<UserGitModel>> Repository();
    }
}
