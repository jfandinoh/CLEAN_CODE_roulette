using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Masivian.Models;
using Masivian.DataBase;

namespace Masivian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase 
    {
        private IDatabases _Databases;

        private Random random = new Random();

        public RouletteController(IDatabases databases)
        {
            this._Databases = databases;
        }

        [HttpGet("NewRoulette")]
        public IActionResult NewRoulette()
        {
            try
            {
                objRoulette objRoulette = new objRoulette();
                objRoulette.Id = random.Next();
                this._Databases.Add(objRoulette);

                return Ok(objRoulette.Id);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{IdRoulette}/OpenRoulette")]
        public IActionResult OpenRoulette(int IdRoulette)
        {
            try
            {
                objRoulette objRoulette = new objRoulette();
                objRoulette.State = objState.Open;
                objRoulette.Id = IdRoulette;
                this._Databases.Update(objRoulette);

                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpPost("{IdRoulette}/BetRoulette")]
        public IActionResult BetRoulette([FromHeader(Name = "IdUser")] int IdUser,[FromBody] objBet objBet, int IdRoulette)
        {
            try
            {
                objRoulette objRoulette = new objRoulette();
                objBet Newobjbets = new objBet();
                Newobjbets.IdUser = IdUser;
                Newobjbets.Color = objBet.Color;
                Newobjbets.Number = objBet.Number;
                Newobjbets.Value = objBet.Value;
                objRoulette.Id = IdRoulette;
                objRoulette.Bets.Add(Newobjbets);
                this._Databases.Update(objRoulette);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{IdRoulette}/CloseRoulette")]
        public IActionResult CloseRoulette(int IdRoulette)
        {
            try
            {
                objRoulette objRoulette = new objRoulette();
                objRoulette.Id = IdRoulette;
                objRoulette.State = objState.Close;
                this._Databases.Update(objRoulette);
                objRoulette = this._Databases.Get(objRoulette);
                int WinNumber = random.Next(1, 36);
                objColor WinColor = WinNumber % 2 == 0 ? objColor.Red : objColor.Black;
                List<objBet> LsWinners = objRoulette.Bets.Where(x => x.Color == WinColor || x.Number == WinNumber).ToList();
                for (int i = 0; i < LsWinners.Count; i++)
                {
                    LsWinners[i].Won = (LsWinners[i].Number == WinNumber ? LsWinners[i].Value * 5 : 0) + (LsWinners[i].Color == WinColor ? LsWinners[i].Value * 1.8M : 0);
                }

                return Ok(LsWinners);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("AllRoulettes")]
        public IActionResult AllRoulettes()
        {
            try
            {
                return Ok(this._Databases.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
