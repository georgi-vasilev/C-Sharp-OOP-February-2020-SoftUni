using System;
using System.Text;

namespace Person
{
	public class Person
	{
		private string name;

		private int age;

		public string Name
		{
			get
			{
				return this.name;
			}
			private set
			{
				this.name = value;
			}
		}

		public virtual int Age
		{
			get
			{
				return this.age;
			}
			protected set
			{
				if (age < 0)
				{
					throw new ArgumentException("The age value cannot be negative number");
				}
				this.age = value;
			}
		}
		public Person(string name, int age)
		{
			this.Name = name;
			this.Age = age;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder
				.Append(String.Format("Name: {0}, Age: {1}",
				this.Name,
				this.Age));

			return stringBuilder.ToString();
		}

	}
}
