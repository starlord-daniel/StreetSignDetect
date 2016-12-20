using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using SignalRService.Objects;

namespace SignalRService
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Set SQL query
            string query = "SELECT TOP (10) [DeviceID],[Latitude],[Longitude],[StreetSign] FROM[dbo].[locationdata] ORDER BY TimeStamp DESC; ";

            // Get SQL Data
            var sqlData = GetSQLResult(query);

            // Convert Results to JSON
            var jsonSQL = JsonConvert.SerializeObject(sqlData);

            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage("Server", jsonSQL);
        }

        private List<GeoObject> GetSQLResult(string query)
        {
            List<GeoObject> resultList = new List<GeoObject>();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        resultList.Add(new GeoObject {
                            deviceId = rdr[0].ToString(),
                            lat = rdr[1].ToString(),
                            lon = rdr[2].ToString(),
                            streetSign = rdr[3].ToString(),
                        });
                    }
                }
                return resultList;
            }
            catch (Exception e)
            {
                return new List<GeoObject> { new GeoObject { deviceId = e.InnerException.ToString() } };
            }
        }
    }
}