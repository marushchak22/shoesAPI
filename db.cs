//using System;
//using MySql.Data.MySqlClient;

//namespace shoesAPI
//{
//	public class DB
//	{
//		MySqlConnection conection = new MySqlConnection("server=localhost;port=8889;username=root;password=root;database=apishoes");
//		public void openconnetcion()
//		{
//			if (conection.State == System.Data.ConnectionState.Closed)
//				conection.Open();
//		}

//        public void closeconnetcion()
//        {
//            if (conection.State == System.Data.ConnectionState.Open)
//                conection.Close();
//        }

//		public MySqlConnection getconnection()
//		{
//			return conection;
//		}
//    }
//}

