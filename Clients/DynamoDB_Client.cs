using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using shoesAPI.Extensions;
using shoesAPI.Models;

namespace shoesAPI.Clients
{
    public class DynamoDB_Client : IDynamoDB_Client
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDB;


        public DynamoDB_Client(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
            _tableName = ConstantsDB.TableName;
        }


        public async Task<ModelShoesDB> GetDataByShoes(string shoeName)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                     {"shoeName", new AttributeValue { S = shoeName} }
                }
            };

            var response = await _dynamoDB.GetItemAsync(item);

            if (response.Item == null || !response.IsItemSet)
                return null;

            var result = response.Item.Tranform<ModelShoesDB>();

            return result;
        }




        public async Task <bool> PostDataToDB(modelsforPost dataDB)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                   
                    {"shoeName", new AttributeValue {S = dataDB.shoeName} },
                    {"brand", new AttributeValue {S = dataDB.brand} },
                    {"colorway", new AttributeValue {S = dataDB.colorway} },
                    {"styleID", new AttributeValue {S = dataDB.styleID} },
                    {"thumbnail", new AttributeValue {S = dataDB.thumbnail} },
                    {"stockX", new AttributeValue {S = dataDB.stockX.ToString()} },
                    {"goat", new AttributeValue {S = dataDB.goat.ToString()} },
                    {"flightClub", new AttributeValue {S = dataDB.flightClub.ToString()} }
                }
            };

            try
            {
                var response = await _dynamoDB.PutItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }

            catch(Exception e)
            {
                Console.WriteLine("error" + e);
                return false;
            }

        }

        public Task UpdateDataInDB()
        {
            throw new NotImplementedException();
        }



        //public async Task<bool> DeleteDataFromDB(string shoeName)
        //{
          
        //    var table = Table.LoadTable(_dynamoDBClient, "ShoesTable");

        //    var deleteRequest = new DeleteItemRequest
        //    {
        //        TableName = "ShoesTable",
        //        Key = new Dictionary<string, AttributeValue>
        //{
        //    { "shoeName", new AttributeValue { S = shoeName } }
        //}
        //    };

        //    var response = await table.DeleteItemAsync(deleteRequest);

        //    return response.HttpStatusCode == HttpStatusCode.OK;
        //}


    }
}

