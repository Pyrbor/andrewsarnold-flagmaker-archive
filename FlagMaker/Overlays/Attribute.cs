﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlagMaker.Overlays
{
	public class Attribute
	{
		public string Name { get; private set; }
		public bool IsDiscrete { get; set; }
		public double Value { get; set; }
		public bool UseMaxX { get; private set; }

		public Attribute(string name, bool isDiscrete, double initialValue, bool useMaxX)
		{
			Name = name;
			IsDiscrete = isDiscrete && (initialValue % 1 == 0);
			Value = initialValue;
			UseMaxX = useMaxX;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Name, Value);
		}
	}

	public static class AttributeExtensions
	{
		public static Attribute Get(this List<Attribute> attributes, string name)
		{
			return attributes.First(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? attributes.First();
		}
	}
}
