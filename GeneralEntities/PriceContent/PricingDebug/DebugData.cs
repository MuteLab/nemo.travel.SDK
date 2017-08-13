﻿using System.Runtime.Serialization;
using System.Text;

namespace GeneralEntities.PriceContent.PricingDebug
{
	[DataContract(Namespace = "http://nemo-ibe.com/STL")]
	public class DebugData
	{
		[DataMember(Order = 0)]
		public VendorsCollection VendorsForCheck { get; set; }

		[DataMember(Order = 1, EmitDefaultValue = false)]
		public string CalculatedVV { get; set; }

		[DataMember(Order = 2, EmitDefaultValue = false)]
		public string AirlineCommission { get; set; }

		[DataMember(Order = 3, EmitDefaultValue = false)]
		public string Bonus { get; set; }

		[DataMember(Order = 4, EmitDefaultValue = false)]
		public string AgencyCommission { get; set; }

		[DataMember(Order = 5, EmitDefaultValue = false)]
		public string Strategy { get; set; }

		[DataMember(Order = 6, EmitDefaultValue = false)]
		public string CurrentProfit { get; set; }

		[DataMember(Order = 7, EmitDefaultValue = false)]
		public string ChargeProfit { get; set; }

		[DataMember(Order = 8, EmitDefaultValue = false)]
		public string CommissionProfit { get; set; }

		[DataMember(Order = 9, EmitDefaultValue = false)]
		public string BonusProfit { get; set; }

		[DataMember(Order = 10, EmitDefaultValue = false)]
		public string DeltaProfit { get; set; }

		[DataMember(Order = 11, EmitDefaultValue = false)]
		public string MinimalProfit { get; set; }

		[DataMember(Order = 12, EmitDefaultValue = false)]
		public string TotalCharge { get; set; }

		[DataMember(Order = 13)]
		public RulesDebugInfoCollection RulesDebugInfo { get; set; }

		[DataMember(Order = 14)]
		public ObjectData Object { get; set; }

		public string Dump()
		{
			var logBuilder = new StringBuilder();

			logBuilder.Append("Checked vendors: ").AppendLine(string.Join(", ", VendorsForCheck));
			if (!string.IsNullOrEmpty(CalculatedVV))
			{
				logBuilder.Append("Selected VV: ").AppendLine(CalculatedVV);
			}
			if (!string.IsNullOrEmpty(AirlineCommission))
			{
				logBuilder.Append("AirlineCommission: ").AppendLine(AirlineCommission);
			}
			if (!string.IsNullOrEmpty(AgencyCommission))
			{
				logBuilder.Append("Subagency commission: ").AppendLine(AgencyCommission);
			}
			if (!string.IsNullOrEmpty(Bonus))
			{
				logBuilder.Append("Airline bonus: ").AppendLine(Bonus);
			}
			if (!string.IsNullOrEmpty(CurrentProfit))
			{
				logBuilder.Append("Current profit: ").AppendLine(CurrentProfit);
			}
			if (!string.IsNullOrEmpty(ChargeProfit))
			{
				logBuilder.Append("charge profit: ").AppendLine(ChargeProfit);
			}
			if (!string.IsNullOrEmpty(CommissionProfit))
			{
				logBuilder.Append("commission profit: ").AppendLine(CommissionProfit);
			}
			if (!string.IsNullOrEmpty(BonusProfit))
			{
				logBuilder.Append("airline bonus profit: ").AppendLine(BonusProfit);
			}
			if (!string.IsNullOrEmpty(MinimalProfit))
			{
				logBuilder.AppendFormat("Minimal profit: ").AppendLine(MinimalProfit);
			}
			if (!string.IsNullOrEmpty(TotalCharge))
			{
				logBuilder.Append("Setted charge: ").AppendLine(TotalCharge);
			}

			logBuilder.Append("VV;ID;AutoticketingRestriction;Sources;UTMSource;PriceActual;DepartureAndArrival;BookingClasses;SalesDates;FirstVendor;FlightType;OwnPart;").
				Append("MarketingVendors;ServiceClasses;Aircraft;Passengers;OperatingVendors;CodeSharing;ContractType;PrivateFare;FlightDate;Zone;Tariffs;Taxes;FlightNumber;Price;DaysOfWeek;").
				Append("RouteType;Routes;Environment;AirlinesAndClasses;Priority;ManualVV;Commission;CommissionResult;AgencyCommission;Bonus;BonusResult;Charge;AdditionalCharge;").
				AppendLine("ChargeResult;MinProfitPriority;MinProfit;Discount;AuthCode;TourCode;RuleIsOK;BestRule;").
				AppendLine(Object.Dump());
			foreach (var ruleData in RulesDebugInfo)
			{
				logBuilder.AppendLine(ruleData.Dump());
			}

			return logBuilder.ToString();
		}
	}
}