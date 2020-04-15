﻿using AviaEntities.v1_1.SharedElements.Ancillaries.RequestElements;
using GeneralEntities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AviaEntities.v2_1.ModifyContent
{
	[CollectionDataContract(Namespace = "http://nemo-ibe.com/Avia", ItemName = "ModifyAncillaryService", Name = "ModifyAncillaryServicesList_1_1")]
	public class ModifyAncillaryServicesList : List<ModifyAncillaryService>
	{
		/// <summary>
		/// Допуслуги которые необходимо добавить
		/// </summary>
		public IEnumerable<AncillaryServiceRQ> ToAdd
		{
			get { return Select(PNRContentModifyAction.Add); }
		}

		/// <summary>
		/// Допуслуги которые необходимо удалить
		/// </summary>
		public IEnumerable<AncillaryServiceRQ> ToRemove
		{
			get { return Select(PNRContentModifyAction.Remove); }
		}


		private IEnumerable<AncillaryServiceRQ> Select(PNRContentModifyAction action)
		{
			return this.Where(s => s.Action == action).Select(s => s.AncillaryService);
		}
	}
}