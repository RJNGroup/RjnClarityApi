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

		public static DataTable DataRecordsToDataTable(Dictionary<string,object>[] items, string[] attributes)
		{

            bool hasAttributes = (attributes != null && attributes.Length > 0) ? true : false;

            //Generate the data table
            var table = new DataTable();
            table.Columns.Add(new DataColumn("id", typeof(Guid)));
            DataColumn[] key = { table.Columns[0] };
            table.PrimaryKey = key;
            table.Columns.Add(new DataColumn("parent_id", typeof(Guid)));
            table.Columns.Add(new DataColumn("project_id", typeof(Guid)));
            table.Columns.Add(new DataColumn("name", typeof(string)));
            table.Columns.Add(new DataColumn("created", typeof(DateTime)));
            table.Columns.Add(new DataColumn("modified", typeof(DateTime)));
            table.Columns.Add(new DataColumn("info_path", typeof(string)));

            //Create the additional columns
            if (hasAttributes)
            {
                foreach (var attribute in attributes)
                {
                    //Determine type
                    Type T = typeof(string);
                    foreach (var item in items)
                    {
                        if (item.ContainsKey(attribute) && item[attribute] != null)
                        {
                            if (item[attribute] is int)
                            {
                                T = typeof(int);
                                break;
                            }
                            else if (item[attribute] is double)
                            {
                                T = typeof(double);
                                break;
                            }
                            else if (item[attribute] is DateTime)
                            {
                                T = typeof(DateTime);
                                break;
                            }

                        }

                    }
                    table.Columns.Add(new DataColumn(attribute, T));
                }
            }

            //Add the rows
            foreach (var item in items)
            {
                var row = new List<object>();
                row.Add(item["id"]);
                row.Add(item["parent_id"]);
                row.Add(item["project_id"]);
                row.Add(item["name"]);
                row.Add(item["created"]);
                row.Add(item["modified"]);
                row.Add(item["info_path"]);

                if (hasAttributes)
                {
                    foreach (var attribute in attributes)
                    {
                        if (item.ContainsKey(attribute) && item[attribute] != null)
                        {
                            row.Add(item[attribute]);
                        }
                        else
                        {
                            row.Add(DBNull.Value);
                        }
                    }
                }


                table.Rows.Add(row.ToArray());
            }

            return table;

        }
    }
}
