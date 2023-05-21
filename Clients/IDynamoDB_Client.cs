using System;
using shoesAPI.Models;

namespace shoesAPI.Clients
{
	public interface IDynamoDB_Client
	{
		public Task<ModelShoesDB> GetDataByShoes(string shoes);
		public Task<bool> PostDataToDB(modelsforPost dataDB);
		public Task UpdateDataInDB();
		//public Task Delete();
	}
}

