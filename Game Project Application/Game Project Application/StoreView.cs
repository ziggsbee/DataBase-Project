﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Project_Application
{
    /// <summary>
    /// The StoreView provides information on a store when given the storeId.  Information provided is the sales of the store,
    /// hours, number of games in inventory, and location.
    /// </summary>
    public partial class StoreView : Form
    {
        /// <summary>
        /// Constructor for StoreView and starts to form.
        /// </summary>
        public StoreView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the form when the user clicks the Finish Button.
        /// </summary>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Provides the user with information on the store such as the hours, location, number of games in inventory, and total sales.
        /// </summary>
        private void btnStoreInfo_Click(object sender, EventArgs e)
        {
            if(uxStoreId.Text != "")
            {
                int storeId = Convert.ToInt32(uxStoreId.Text);

                string[] output = getStoreInfo(storeId);

                string s = "Location: " +  output[0] + "\n" +
                           "Weekday Hours: " + output[1] + "\n" +
                           "Weekend Hours: " + output[2] + "\n" +
                           "Number of Games in Store: " + output[3] + "\n" +
                           "Total Sales: " + output[4] + "\n";
                if(output[4].Length == 0)
                {
                    s = "Please enter a correct store Id.";
                }
            
                MessageBox.Show(s);
            }
            else
            {
                MessageBox.Show("Please Insert a Store Id.");
            }

        }

        /// <summary>
        /// When given a storeId the method queries the database for the hours, location, number of games in inventory, and total sales.
        /// </summary>
        /// <param name="id">Store Id</param>
        /// <returns>A string array with all the data on the given store</returns>
        private string[] getStoreInfo(int id)
        {
            string connectionString = "Server=mssql.cs.ksu.edu;Database=cis560_team21; Integrated Security=true";
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GameStore.GetStoreInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StoreId", id);
                    connection.Open();

                    var k = command.ExecuteReader();


                    string address ="";
                    string city ="";
                    string state ="";
                    string weekdayHours = "";
                    string weekendHours = "";
                    string numOfGames = "";
                    string totalSales = "";
                    int hours;
                    int minute;
                    string m = "";
                    TimeSpan time;

                    while (k.Read())
                    {
             
                        address = k.GetString(k.GetOrdinal("Address"));
                        city = k.GetString(k.GetOrdinal("City"));
                        state = k.GetString(k.GetOrdinal("State"));

                        if(k.GetString(k.GetOrdinal("Description")).Equals("Monday to Friday Hours"))
                        {

                            time = k.GetTimeSpan(k.GetOrdinal("StartingTime"));
                            hours = Convert.ToInt32(time.TotalHours);
                            minute = ( hours * 60) - Convert.ToInt32(time.TotalMinutes);

                            if (minute < 10)
                            {
                                m = "0" + minute.ToString();
                            }
                            else
                            {
                                m = minute.ToString();
                            }

                            if (hours < 12)
                            {
                                

                                weekdayHours = hours.ToString() + ":" + m.ToString() + " AM";
                            }
                            else
                            {
                                
                                weekdayHours = (hours % 12).ToString() + ":" + m.ToString() + " PM";
                            }

                        }
                        else if(k.GetString(k.GetOrdinal("Description")).Equals("Weekend Hours"))
                        {
                            time = k.GetTimeSpan(k.GetOrdinal("StartingTime"));
                            hours = Convert.ToInt32(time.TotalHours);
                            minute = (hours * 60) - Convert.ToInt32(time.TotalMinutes);

                            if (minute < 10)
                            {
                                m = "0" + minute.ToString();
                            }
                            else
                            {
                                m = minute.ToString();
                            }

                            if (hours < 12)
                            {

                                weekendHours = hours.ToString() + ":" + m.ToString() + " AM";
                            }
                            else
                            {

                                weekendHours = (hours % 12).ToString() + ":" + m.ToString() + " PM";
                            }
                        }


                        numOfGames = k.GetInt32(k.GetOrdinal("GameCount")).ToString();
                        totalSales = "$" + k.GetDecimal(k.GetOrdinal("TotalSales")).ToString();

                    }

                    string location = address + ", " + city + ", " + state;

                    
                    k.Close();

                    string[] output = { location, weekdayHours, weekendHours, numOfGames, totalSales };
                    return output;
                }
            }
        }

        /// <summary>
        /// Returns the total number of orders made and total sales of each month and each year
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        private string[] getSalesInfo(int storeId, int i)
        {
            string connectionString = "Server=mssql.cs.ksu.edu;Database=cis560_team21; Integrated Security=true";
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GameStore.GetSalesInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("StoreId", storeId);
                    connection.Open();

                    var k = command.ExecuteReader();

                    string orders = "";
                    string totalSales = "";

                    while (k.Read())
                    {
                        if (k.GetInt32(k.GetOrdinal("OrderMonth")).Equals(i.ToString()))
                        {
                            orders = k.GetInt32(k.GetOrdinal("OrderCount")).ToString();
                            totalSales = k.GetDecimal(k.GetOrdinal("Sales")).ToString();
                        }
                        else if(i == 0 && k.GetInt32(k.GetOrdinal("OrderMonth")).Equals("12"))
                        {
                            orders = k.GetInt32(k.GetOrdinal("YearlyOrders")).ToString();
                            totalSales = k.GetDecimal(k.GetOrdinal("YearlySales")).ToString();
                        }
                    }

                    k.Close();

                    string[] output = { orders, totalSales };
                    return output;
                }
            }
        }


        private void btnSales_Click(object sender, EventArgs e)
        {
            if (uxStoreId.Text != "")
            {
                int storeId = Convert.ToInt32(uxStoreId.Text);
                StringBuilder s = new StringBuilder();

                string[] output;

                for(int i = 1; i <= 12; i++)
                {
                    output = getSalesInfo(storeId, i);
                    switch (i)
                    {
                        case 1:
                            s.Append("Month: January\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 2:
                            s.Append("Month: February\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 3:
                            s.Append("Month: March\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 4:
                            s.Append("Month: April\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 5:
                            s.Append("Month: May\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 6:
                            s.Append("Month: June\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 7:
                            s.Append("Month: July\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 8:
                            s.Append("Month: August\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");        
                            break;
                        case 9:
                            s.Append("Month: September\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 10:
                            s.Append("Month: October\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 11:
                            s.Append("Month: November\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n");
                            break;
                        case 12:
                            s.Append("Month: December\n" +
                                     "Monthly Orders: " + output[0] + "\n" +
                                     "Monthly Sales:  " + output[1] + "\n\n");
                            break;
                    }

                }

                output = getSalesInfo(storeId, 0);
                s.Append("Yearly Orders: " + output[0] + "\n" +
                         "Yearly Sales:  " + output[1]);

                MessageBox.Show(s.ToString());
            }
            else
            {
                MessageBox.Show("Please Insert a Store Id.");
            }
        }

        private void uxStoreId_TextChanged(object sender, EventArgs e)
        {
            int index = uxStoreId.SelectionStart;
            string text = uxStoreId.Text;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    if (i == 0)
                    {
                        text = text.Substring(1, text.Length - 1);
                    }
                    else if (i == text.Length - 1)
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    else
                    {
                        text = text.Substring(0, i) + text.Substring(i + 1, text.Length - i - 1);
                    }
                    uxStoreId.Text = text;
                    if (index > 0)
                    {
                        uxStoreId.SelectionStart = index - 1;
                    }
                    else
                    {
                        uxStoreId.SelectionStart = index;
                    }
                }
            }
        }
    }
}
