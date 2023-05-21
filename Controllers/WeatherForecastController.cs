using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using shoesAPI.Clients;
using shoesAPI.Extensions;
using shoesAPI.Models;
using static shoesAPI.Models.ModelShoesDB;


namespace shoesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ShoesClient _shoesClient;
        private readonly IDynamoDB_Client _dynamoDB_Client;

        public WeatherForecastController(ShoesClient shoesClient, IDynamoDB_Client dynamoDB_Client)
        {
            _shoesClient = shoesClient;
            _dynamoDB_Client = dynamoDB_Client;
        }

        
        [HttpGet("shoesmodels")]
        public async Task<ActionResult<IEnumerable<SneakersModel>>> GetSneakersAsync([FromQuery] Modelsforshoes models)
        {
            var sneakersJson = await _shoesClient.GetShoesbyBrands(models.Shoesmodel);

            var sneakers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SneakersModel>>(sneakersJson);

            var result = new List<SneakersModel>();

            foreach (var sneaker in sneakers)
            {
                var sneakersModel = new SneakersModel
                {
                    shoeName = sneaker.shoeName,
                    brand = sneaker.brand,
                    colorway = sneaker.colorway,
                    styleID = sneaker.styleID,
                    thumbnail = sneaker.thumbnail,
                    lowestResellPrice = sneaker.lowestResellPrice
                 
                };
                result.Add(sneakersModel);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavorShoes([FromQuery] string shoes)
        {

            var result = await _dynamoDB_Client.GetDataByShoes(shoes);

            if (result == null)
                return NotFound("not founded this shoes:(");

            var shoesResponce = new ModelShoesDB
            {
                shoeName = result.shoeName,
                brand = result.brand,
                colorway = result.colorway,
                styleID = result.styleID,
                thumbnail = result.thumbnail,
             
                    stockX = result.stockX,
                    goat = result.goat,
                    flightClub = result.flightClub
               

            };
            return Ok(shoesResponce);
        }

    
        [HttpPost("add shoes")]
        public async Task <IActionResult> AddShoesToDB([FromBody] modelsforPost modelShoesDB)
        {
            var dataDB = new modelsforPost
            {

                shoeName = modelShoesDB.shoeName,
                brand = modelShoesDB.brand,
                colorway = modelShoesDB.colorway,
                styleID = modelShoesDB.styleID,
                thumbnail = modelShoesDB.thumbnail,

                stockX = modelShoesDB.stockX,
                goat = modelShoesDB.goat,
                flightClub = modelShoesDB.flightClub


            };
            var result = await _dynamoDB_Client.PostDataToDB(dataDB);

            if(result == false)
            {
                return BadRequest("Can not insert :(");
            }

            return Ok("Access");
        }

        //[HttpDelete("delete shoes/{shoeName}")]
        //public async Task<IActionResult> DeleteShoesFromDB(string shoeName)
        //{
        //    var result = await _dynamoDB_Client.DeleteDataFromDB(shoeName);

        //    if (!result)
        //    {
        //        return BadRequest("Can not delete :(");
        //    }

        //    return Ok("Access");
        //}

    }

}




