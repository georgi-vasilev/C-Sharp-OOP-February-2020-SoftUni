using System;

namespace Zoo
{
	public abstract class Animal
    {
		private string name;

		public string Name
		{
			get 
			{ 
				return this.name; 
			}
			set 
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("The name should not be an empty string.");
				}
				this.name = value; 
			}
		}

		public Animal(string name)
		{
			this.Name = name;
		}

	}
}
