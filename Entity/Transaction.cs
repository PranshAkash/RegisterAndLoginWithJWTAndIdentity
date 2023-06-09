﻿
namespace Entity
{
    public class Transaction
    {
        public int ID { get; set; }
        public int TID { get; set; }
        public int UserID { get; set; }
        public string Account { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal RequestedAmount { get; set; }
        public int Type { get; set; }
        public bool IsFailToSuccess { get; set; }
        public string Remark { get; set; }
        public int RefundStatus { get; set; }
        public string RefundRemark { get; set; }
        public string VendorID { get; set; }
        public string LiveID { get; set; }
        public string APIRequestID { get; set; }
        public int APIID { get; set; }
        public string API { get; set; }
        public string APIOpCode { get; set; }
        public int OID { get; set; }
        public string SPKey { get; set; }
        public string Operator { get; set; }
        public string BillingModel { get; set; }
        public bool CommType { get; set; }
        public bool AmtType { get; set; }
        public decimal CommAtRate { get; set; }
        public decimal CommAmount { get; set; }
        public bool APICommType { get; set; }
        public decimal APIComAmt { get; set; }
        public int TDSID { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal TDSValue { get; set; }
        public string TaxRemark { get; set; }
        public int GSTTaxID { get; set; }
        public decimal GSTTaxAmount { get; set; }
        public decimal GSTValue { get; set; }
        public bool IsHoldGST { get; set; }
        public decimal DCommRate { get; set; }
        public decimal DComm { get; set; }
        public int ServiceID { get; set; }
        public int EntryBy { get; set; }
        public string EntryDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string RefundRequestDate { get; set; }
        public string RefundRejectDate { get; set; }
        public int RequestModeID { get; set; }
        public string RequestIP { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public string Optional4 { get; set; }
        public string ExtraParam { get; set; }
        public string TransactionID { get; set; }
        public string UpdatePage { get; set; }
        public decimal APIBalance { get; set; }
        public int OutletID { get; set; }
        public int WalletID { get; set; }
        public bool IsCCFModel { get; set; }
        public bool IsVirtual { get; set; }
        public int VirtualID { get; set; }
        public int WID { get; set; }
        public int ApiType { get; set; }
        public string IMEI { get; set; }
        public int OpTypeID { get; set; }
        public string OpType { get; set; }
        public bool IsAdminDefined { get; set; }
        public int SlabID { get; set; }
        public int RoleID { get; set; }
        public string GroupID { get; set; }
        public string CustomerNo { get; set; }
        public int CCID { get; set; }
        public string CCMobileNo { get; set; }
        public string Customercare { get; set; }
        public string CustomercareIP { get; set; }
        public string Display1 { get; set; }
        public string Display2 { get; set; }
        public string Display3 { get; set; }
        public string Display4 { get; set; }
        public int TotalToken { get; set; }
        public string CustomerName { get; set; }
        public string IsCommissionDistributed { get; set; }
        public int SwitchingID { get; set; }
        public int DMRModelID { get; set; }
        public int CircleID { get; set; }
        public bool IsSameSessionUpdated { get; set; }
        public bool CommMax { get; set; }
        public int BookingStatus { get; set; }
        public bool IsCircleCommission { get; set; }
        public string O5 { get; set; }
        public string O6 { get; set; }
        public string O7 { get; set; }
        public string O8 { get; set; }
        public string O9 { get; set; }
        public string O10 { get; set; }
        public string O11 { get; set; }
        public string O12 { get; set; }
        public string O13 { get; set; }
        public string O14 { get; set; }
        public string O15 { get; set; }
        public string O16 { get; set; }
        public string O17 { get; set; }
        public string Circle { get; set; }
        public decimal CommAtRate2 { get; set; }
        public bool CommType2 { get; set; }
        public bool AmtType2 { get; set; }
        public int SplitCount { get; set; }
        public int SenderLimitID { get; set; }
        public int DailyLimitID { get; set; }
        public bool IsGSTVerified { get; set; }
        public bool IsLimitUsed { get; set; }
        public int WIDForAPI { get; set; }
        public string APIContext { get; set; }
        public int MsgId { get; set; }
        public int MaxTID { get; set; }
        public int O18n { get; set; }
        public bool IsMultilevel { get; set; }
        public string LapuNumber { get; set; }
        public decimal ROfferAmount { get; set; }
        public decimal APIAmount { get; set; }
        public decimal CommissionFromAPI { get; set; }
        public decimal APIDebited { get; set; }
        public bool _3WayReconCalled { get; set; }
        public int _3WayHitCount { get; set; }
        public string UtilityUserID { get; set; }
        public string Webhook { get; set; }
    }
}
