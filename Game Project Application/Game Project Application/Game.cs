﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project_Application
{
    public class Game
    {
        private string title;
        private string genre;
        private double price;
        private bool isUsed;



        public Game(string title, string genre, double price, bool isUsed)
        {
            this.title = title;
            this.genre = genre;
            this.price = price;
            this.isUsed = isUsed;

        }

    }
}