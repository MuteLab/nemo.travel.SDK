﻿using AviaEntities.FlightSearch.RequestElements;
using GeneralEntities;
using GeneralEntities.PNRDataContent;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace AviaEntities.v1_2.SearchFlights
{
	/// <summary>
	/// Тело запроса поиска перелётов
	/// </summary>
	[DataContract(Namespace = "http://nemo-ibe.com/Avia")]
	public class SearchFlightsRQBody
	{
		/// <summary>
		/// Содержит техническую информацию о сегметах запрашиваемого перелёта
		/// (время вылета, прямой или с пересадками и т.д.)
		/// </summary>
		[DataMember(Order = 0, IsRequired = true)]
		public RequestElements.FlightDirection RequestedFlightInfo { get; set; }

		/// <summary>
		/// Информация о пассажирах, для которых выполняется поиск перелёта
		/// </summary>
		[DataMember(Order = 1, IsRequired = true)]
		public List<Passenger> Passengers { get; set; }

		/// <summary>
		/// Допольнительный условия/ограничея, накладываемые на поиск перелётов
		/// <para>NeverNullAfterNormalization</para>
		/// </summary>
		[DataMember(Order = 2, EmitDefaultValue = false)]
		public RequestElements.AdditionalSearchInfo Restrictions { get; set; }

		/// <summary>
		/// Данный конечного пользователя
		/// </summary>
		[DataMember(Order = 3, EmitDefaultValue = false)]
		public EndUserDataDataItem EndUserData { get; set; }

		/// <summary>
		/// Описание точки продажи
		/// </summary>
		[DataMember(Order = 4, EmitDefaultValue = false)]
		public SellingPointDescriptionDataItem SellingPointDescription { get; set; }


		/// <summary>
		/// Привязка к сегменту из запроса
		/// </summary>
		[XmlIgnore]
		[JsonIgnore]
		[IgnoreDataMember]
		public int? MultiOWRequestedSegmentNumber { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		[IgnoreDataMember]
		public bool AsyncSearch
		{
			get
			{
				return Restrictions != null && Restrictions.AsyncSearch;
			}
		}

		[XmlIgnore]
		[JsonIgnore]
		[IgnoreDataMember]
		public bool IsMetaSearch
		{
			get
			{
				return Restrictions != null && Restrictions.RequestorTags != null && Restrictions.RequestorTags.Contains("meta");
			}
		}

		[XmlIgnore]
		[JsonIgnore]
		[IgnoreDataMember]
		public HashSet<ClassType> UserRequestedClass { get; set; }

		/// <summary>
		/// Выполняет полное копирование объекта, реализация интерфейса ICloneable
		/// </summary>
		/// <returns>Результат копирования</returns>
		public SearchFlightsRQBody Clone()
		{
			var result = new SearchFlightsRQBody();

			result.MultiOWRequestedSegmentNumber = MultiOWRequestedSegmentNumber;
			result.UserRequestedClass = UserRequestedClass;

			result.RequestedFlightInfo = new RequestElements.FlightDirection();
			result.RequestedFlightInfo.ODPairs = new RequestElements.FlightPairList();
			result.Passengers = new List<Passenger>();

			result.RequestedFlightInfo.AroundDates = RequestedFlightInfo.AroundDates;
			result.RequestedFlightInfo.Direct = RequestedFlightInfo.Direct;
			result.RequestedFlightInfo.ForcedType = RequestedFlightInfo.ForcedType;
			result.RequestedFlightInfo.SubType = RequestedFlightInfo.SubType;

			foreach (var seg in RequestedFlightInfo.ODPairs)
			{
				result.RequestedFlightInfo.ODPairs.Add(seg.FullCopy());
			}

			foreach (var pass in Passengers)
			{
				var tmpPass = new Passenger();
				tmpPass.Count = pass.Count;
				tmpPass.Type = pass.Type;

				result.Passengers.Add(tmpPass);
			}

			if (Restrictions != null)
			{
				result.Restrictions = new RequestElements.AdditionalSearchInfo();
				result.Restrictions.ClassPreference = new RequestElements.ClassPrefList();
				result.Restrictions.ClassPreference.AddRange(Restrictions.ClassPreference);
				result.Restrictions.PrivateFaresOnly = Restrictions.PrivateFaresOnly;
				result.Restrictions.SourcePreference = Restrictions.SourcePreference;
				result.Restrictions.MaxConnectionTime = Restrictions.MaxConnectionTime;
				result.Restrictions.ResultsGrouping = Restrictions.ResultsGrouping;
				result.Restrictions.MaxResultCount = Restrictions.MaxResultCount;
				result.Restrictions.PriceRefundType = Restrictions.PriceRefundType;
				result.Restrictions.AsyncSearch = Restrictions.AsyncSearch;
				result.Restrictions.AdditionalPublicFaresOnly = Restrictions.AdditionalPublicFaresOnly;
				result.Restrictions.MaxConnections = Restrictions.MaxConnections;
				result.Restrictions.RequestorTags = Restrictions.RequestorTags;
				result.Restrictions.ThreeDomainAgreementNumber = Restrictions.ThreeDomainAgreementNumber;

				if (Restrictions.CompanyFilter != null)
				{
					result.Restrictions.CompanyFilter = new List<Company>();
					foreach (var oldComp in Restrictions.CompanyFilter)
					{
						var comp = new Company();
						comp.Code = oldComp.Code;
						comp.Include = oldComp.Include;
						comp.SegmentNumber = oldComp.SegmentNumber;

						result.Restrictions.CompanyFilter.Add(comp);
					}
				}
			}

			if (EndUserData != null)
			{
				result.EndUserData = new EndUserDataDataItem();
				result.EndUserData.EndUserBrowserAgent = EndUserData.EndUserBrowserAgent;
				result.EndUserData.EndUserIP = EndUserData.EndUserIP;
				result.EndUserData.RequestOrigin = EndUserData.RequestOrigin;
			}

			return result;
		}

		/// <summary>
		/// Получение данных для хэша запроса для использования в кэш-сервере
		/// </summary>
		/// <returns></returns>
		public string GetCacheHashData()
		{
			var result = new StringBuilder();

			result.Append(JsonConvert.SerializeObject(RequestedFlightInfo));
			result.Append(JsonConvert.SerializeObject(Passengers));

			if (Restrictions != null)
			{
				result.Append(Restrictions.AdditionalPublicFaresOnly);
				result.Append(JsonConvert.SerializeObject(Restrictions.ClassPreference));
				result.Append(JsonConvert.SerializeObject(Restrictions.CompanyFilter));
				result.Append(Restrictions.MaxConnections);
				result.Append(Restrictions.MaxConnectionTime);
				result.Append(Restrictions.MaxResultCount);
				result.Append(Restrictions.PriceRefundType);
				result.Append(Restrictions.PrivateFaresOnly);

				//непосредственно на запрос не влияют, поэтому не учитываем тут
				//Restrictions.AsyncSearch
				//Restrictions.RequestorTags
				//Restrictions.ResultsGrouping
				//Restrictions.SourcePreference
			}

			return result.ToString();
		}
	}
}