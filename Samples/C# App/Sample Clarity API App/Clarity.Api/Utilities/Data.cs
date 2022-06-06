using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Clarity.Utilities
{
	public class Data
	{
		/// <summary>
		/// Converts the object array to a datatable.
		/// </summary>
		/// <param name="items">The item array.</param>
		/// <returns>A data table with a row for each item and a column for each property.</returns>
		public static DataTable ToDataTable(object[] items)
		{
			var result = new DataTable();

			if (items.Length > 0)
			{
				//Create the columns
				var props = items.First().GetType().GetProperties();
				foreach (var prop in props )
				{
					result.Columns.Add(prop.Name, prop.PropertyType);
				}

				//Add the rows
				foreach (var item in items)
				{
					result.Rows.Add(props.Select(p => item.GetType().GetProperty(p.Name).GetValue(item)).ToArray());
				}
			}


			return result;
		}
	}
}
