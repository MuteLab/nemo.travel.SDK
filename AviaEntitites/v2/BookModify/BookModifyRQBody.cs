﻿using AviaEntities.ModifyContent;
using AviaEntities.SharedElements;
using GeneralEntities.ModifyContent;
using GeneralEntities.Shared;
using System.Runtime.Serialization;

namespace AviaEntities.v2.BookModify
{
	/// <summary>
	/// Содержит описание запроса на модификацию брони
	/// </summary>
	[DataContract(Namespace = "http://nemo-ibe.com/Avia", Name = "BookModifyRQBody_2_0")]
	public class BookModifyRQBody : OnlyBookIDElement
	{
		/// <summary>
		/// Модификация данных пассажира
		/// </summary>
		[DataMember(Order = 1, EmitDefaultValue = false)]
		public ModifyTravellerInformationList Travellers { get; set; }

		/// <summary>
		/// Модификация перелёта
		/// </summary>
		[DataMember(Order = 2, EmitDefaultValue = false)]
		public ModifyFlightService Flight { get; set; }

		/// <summary>
		/// Контент брони
		/// </summary>
		[DataMember(Order = 3, EmitDefaultValue = false)]
		public ModifyPNRDataItemList DataItems { get; set; }

		/// <summary>
		/// Допуслуги
		/// </summary>
		[DataMember(Order = 4, EmitDefaultValue = false)]
		public ModifyAncillaryServicesList AncillaryServices { get; set; }

		/// <summary>
		/// Теги для ЦО
		/// </summary>
		[DataMember(Order = 5, EmitDefaultValue = false)]
		public TagList RequestorTags { get; set; }

		[DataMember(Order = 6, EmitDefaultValue = false)]
		public int? RefererID { get; set; }
	}
}