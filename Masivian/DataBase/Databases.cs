using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masivian.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Masivian.DataBase
{
    public class Databases : IDatabases
    {
        private readonly IServer _server;
        private readonly IDatabase _database;


        public Databases(IDatabase database, IServer server )
        {
            this._server = server;
            this._database = database;
        }

        public void Add(objRoulette objRoulette)
        {
            try
            {
                if (!this._database.KeyExists(objRoulette.Id.ToString()))
                {
                    this._database.StringSet(objRoulette.Id.ToString(), JsonConvert.SerializeObject(objRoulette), TimeSpan.FromDays(1));
                }
                else
                {
                    throw new Exception("Existing key " + objRoulette.Id.ToString());
                } 
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public objRoulette Get(objRoulette objRoulette)
        {
            try
            {
                return JsonConvert.DeserializeObject<objRoulette>(this._database.StringGet(objRoulette.Id.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public List<objRoulette> GetAll()
        {
            try
            {
                List<objRoulette> LsredisValue = new List<objRoulette>();
                List<RedisKey> redisKeys = _server.Keys().ToList();
                foreach(RedisKey redisKey in redisKeys)
                {
                    LsredisValue.Add(JsonConvert.DeserializeObject<objRoulette>(this._database.StringGet(redisKey)));
                }

                return LsredisValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public void Update(objRoulette objRoulette)
        {
            try
            {
                if (this._database.KeyExists(objRoulette.Id.ToString()))
                {
                    objRoulette RoulleteOriginal = Get(objRoulette);
                    if(objRoulette.Bets.Count > 0)
                    {
                        foreach(objBet objBet in objRoulette.Bets)
                        {
                            RoulleteOriginal.Bets.Add(objBet);
                        }
                    }
                    if(objRoulette.State != RoulleteOriginal.State)
                    {
                        RoulleteOriginal.State = objRoulette.State;
                    }
                    this._database.StringSet(objRoulette.Id.ToString(), JsonConvert.SerializeObject(RoulleteOriginal), TimeSpan.FromDays(1));
                }
                else
                {
                    throw new Exception("Key " + objRoulette.Id.ToString()+ " not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

    }
}
