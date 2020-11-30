﻿using System;
using System.Collections.Generic;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double WEIGHT_MULTIPLIER = 0.3;

        public Cat(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {

        }

        public override double WeightMultiplier =>
            WEIGHT_MULTIPLIER;

        public override ICollection<Type> PrefferedFoods =>
            new List<Type>() { typeof(Meat), typeof(Vegetable) };

        public override string ProduceSound()
        {
            return $"Meow";
        }
    }
}
