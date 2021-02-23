using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Masivian.Models;

namespace Masivian.DataBase
{
    public interface  IDatabases
    {
        void Add(objRoulette objRoulette);
        objRoulette Get(objRoulette objRoulette);
        List<objRoulette> GetAll();
        void Update(objRoulette objRoulette);
    }
}
