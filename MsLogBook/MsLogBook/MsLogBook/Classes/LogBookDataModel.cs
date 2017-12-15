using System;
using System.Collections.Generic;
using PV.App.Managers.Standard.PackageModels;

namespace MsLogBook
{
    public class LogBookDataModel
    {
        public DateTime ServiceDate { get; set; }
        public string Clinic { get; set; }
        public int LogNumber { get; set; }
        public string UsesScheduling { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public int? ScheduledMethod { get; set; }
        public string KioskPIN { get; set; }
        public byte? KioskStatus { get; set; }
        public Guid? KioskSessionPk { get; set; }
        public byte? eRegStatus { get; set; }
        public int PatientNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? CheckInPrintOutDateTime { get; set; }
        public Guid? CheckInPrintUserProfilePk { get; set; }
        public string CheckInPrintUserName { get; set; }
        public string PrivateStatus { get; set; }
        public string EpsStatus { get; set; }
        public string WorkCompStatus { get; set; }
        public string MiscStatus { get; set; }
        public string PrivateSelfPay { get; set; }
        public string PrivateChargeHeaderStatus { get; set; }
        public string EpsChargeHeaderStatus { get; set; }
        public string WorkCompChargeHeaderStatus { get; set; }
        public DateTime? PrivateTimeOut { get; set; }
        public DateTime? EpsTimeOut { get; set; }
        public DateTime? WorkCompTimeOut { get; set; }
        public DateTime? MiscTimeOut { get; set; }
        public string PRV_No_Pivot_Flag { get; set; }
        public string EPS_No_Pivot_Flag { get; set; }
        public string WC_No_Pivot_Flag { get; set; }
        public string Misc_No_Pivot_Flag { get; set; }
        public decimal? Prev_Pay_Amt { get; set; }
        public decimal PoaPaidAmount { get; set; }
        public int? RTE_Status { get; set; }
        public byte? VirtualVisitTypeMisc { get; set; }
        public byte? VirtualVisitTypePriv { get; set; }
        public byte? VirtualVisitTypeWC { get; set; }
        public byte? VirtualVisitTypeEPS { get; set; }
        public string ProcCodes { get; set; }
        public string MiscLogBookStatus { get; set; }
        public string PreferredName { get; set; }
        public bool? PrivateIsChartSigned { get; set; }
        public bool? WorkCompIsChartSigned { get; set; }
        public bool? EpsIsChartSigned { get; set; }
        public bool? MiscIsChartSigned { get; set; }
        public Guid? LogBookRegistrationHoldingPk { get; set; }
        public int? AppointmentId { get; set; }
        public bool ClinicUsesHcaWebCheckin { get; set; }
        public List<LogDetailModel> LogDetailModel { get; set; }
    }
}
