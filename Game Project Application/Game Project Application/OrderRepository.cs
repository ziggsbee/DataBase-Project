﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project_Application
{
    class OrderRepository : IOrderRepository
    {
        public List<Order> RetrieveOrders(SearchConditions sc)
        {
            string connectionString = "Server=mssql.cs.ksu.edu;Database=cis560_team21; Integrated Security=true";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GameStore.RetrieveOrders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;



                    /*command.Parameters.AddWithValue("Title", sc.Title);
                    command.Parameters.AddWithValue("Genre", sc.Genre);
                    command.Parameters.AddWithValue("MinPrice", sc.MinPrice);
                    command.Parameters.AddWithValue("MaxPrice", sc.MaxPrice);
                    command.Parameters.AddWithValue("StoreId", sc.StoreId);
                    command.Parameters.AddWithValue("IsUsed", sc.IsUsed);
                    connection.Open();
                    
                    var reader = command.ExecuteReader();
                    */
                    var orderList = new List<Order>();

                    /*while (reader.Read())
                    {
                        string condition;

                        if (reader.GetBoolean(reader.GetOrdinal("IsUsed")) == true)
                        {
                            condition = "Used";
                        }
                        else
                        {
                            condition = "New";
                        }

                        orderList.Add(new Order(
                           reader.GetString(reader.GetOrdinal("Title")),
                           "GENRE",
                           reader.GetDecimal(reader.GetOrdinal("UnitPrice")).ToString(),
                           reader.GetInt32(reader.GetOrdinal("Quantity")).ToString(),
                           condition,
                           reader.GetInt32(reader.GetOrdinal("storeId")),
                           reader.GetInt32(reader.GetOrdinal("gameId"))));
                    }
                    */
                    return orderList;
                }
            }
        }
    }
}