using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Enums;
using Clarity.ResponseObjects;
using System.ComponentModel;

namespace Clarity.RequestParameters 
{
	public class RecordInfoRequestParameters : IRequestParameters
	{
		[Browsable(true), Category("Required Identifier"), Description("REQUIRED: The data type name such as SmokeObservation or StructureInspection.")]
		public string datatype { get; set; }

		[Browsable(true), Category("Required Identifier"), Description("REQUIRED: The guid id of the record.")]
		public Guid record_id { get; set; }

		[Browsable(true), Category("Options"), Description("Return the attribute information.")]
		public bool attribute_info { get; set; } = true;

		[Browsable(true), Category("Options"), Description("Use the human-friendly labels for the attributes rather than the name identifiers.")]
		public bool pretty_attributes { get; set; } = false;

		[Browsable(true), Category("Options"), Description("Return the related media list.")]
		public bool media_info { get; set; } = true;

		[Browsable(true), Category("Options"), Description("Return the location coordinates.")]
		public bool coordinate_info { get; set; } = true;

		[Browsable(true), Category("Options"), Description("Return the parent record if exists.")]
		public bool parent_info { get; set; } = false;

		[Browsable(true), Category("Options"), Description("Return the children (and grandchildren) records if they exist.")]
		public bool child_info { get; set; } = false;



		public object[] ToParamArray(Api api, object Parent)
		{
			return new object[] { datatype, record_id, attribute_info, pretty_attributes, media_info, coordinate_info, parent_info, child_info }; ;
		}
	}
}
