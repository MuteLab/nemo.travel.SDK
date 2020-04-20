﻿using System.Runtime.Serialization;

namespace GeneralEntities.PNRDataContent.Ancillary
{
	/// <summary>
	/// Содержит специфичные для ЕМД параметры
	/// </summary>
	[DataContract(Namespace = "http://nemo-ibe.com/STL")]
	public class EMDSpecificData
	{
		/// <summary>
		/// Тип ЕМД - A/S, A - associated, S - standalone
		/// </summary>
		[DataMember(Order = 0, EmitDefaultValue = false)]
		public string EMDType { get; set; }

		/// <summary>
		/// Номер билета, в привязке к которому выписан данный ЕМД. В основном имеет смысл только для ЕМД-А
		/// </summary>
		[DataMember(Order = 1, EmitDefaultValue = false)]
		public string ParentTicket { get; set; }

		/// <summary>
		/// Свободно-текстовое описание
		/// </summary>
		[DataMember(Order = 2, EmitDefaultValue = false)]
		public string Description { get; set; }

		public bool Equals(EMDSpecificData item1, EMDSpecificData item2)
		{
			return item1 == null && item2 == null ||
				item1 != null && item2 != null && item1.EMDType == item2.EMDType && item1.ParentTicket == item2.ParentTicket && item1.Description == item2.Description;
		}

		public EMDSpecificData Copy()
		{
			var result = new EMDSpecificData();

			result.EMDType = EMDType;
			result.ParentTicket = ParentTicket;
			result.Description = Description;

			return result;
		}
	}
}