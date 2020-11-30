﻿using System;

namespace ValidationAttributes.Attributes
{
    public class MyRangeAttribute : MyValidatorAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;

        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.ValidateRange(minValue, maxValue);

            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override bool IsValid(object obj)
        {
            if (obj is Int32)
            {
                int value = (int)obj;

                if (value < this.minValue || value > this.maxValue)
                {
                    return false;
                }

                return true;

            }
            else
            {
                throw new InvalidOperationException("Canot validate given data type!");
            }
        }

        private void ValidateRange(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("Invalid range!");
            }
        }
    }
}
