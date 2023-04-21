using Data;
using Entity;
using Infrastructure.Helper;
using System.Data;

namespace Infrastructure.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IDapperRepository _dapper;
        private readonly IConnectionHelper _connectionHelper;
        private readonly ICustomeLogger<TaskService> _logger;
        public TaskService(IDapperRepository dapper, ICustomeLogger<TaskService> logger, IConnectionHelper connectionHelper)
        {
            _dapper = dapper;
            _logger = logger;
            _connectionHelper=connectionHelper;
        }

        public async Task<int> TakeBackup()
        {
            int i = 0;
            i=await LedgerBackup();
            i=i+await TransactionBackup();
            return i;
        }

        private async Task<int> LedgerBackup()
        {
            int i = 0;
            try
            {
                int startId = 0;
                string sqlQuery = $@"{TableSchema.Ledger()} SELECT MAX(_Id) FROM tbl_Ledger(nolock) ";

                startId = await _dapper.GetAsync<int>(sqlQuery, new { }, CommandType.Text, _connectionHelper.ConnectionString_bk);

Select:

                sqlQuery = $@"SELECT TOP 1000 _ID ID,_UserID UserID,_TID TID,_Type Type,_Amount Amount,_CurrentAmount CurrentAmount,
                                     _LastAmount LastAmount,_ServiceTypeID ServiceTypeID,_LT LT,_EntryBy EntryBy,_EntryDate EntryDate, 
                                     _Remark Remark,_Description Description,_WalletID WalletID,_RFTID RFTID,_LedgerTypeID LedgerTypeID,
                                     _EntryByName EntryByName,_EntryByMobile EntryByMobile,_OtherUserID OtherUserID,
                                     _ReffNo ReffNo,_IsVirtual IsVirtual,_BankName BankName,_UTR UTR,_TransactionID TransactionID,
                                     _IsAutoBilling IsAutoBilling  
                             FROM tbl_Ledger(nolock) WHERE _Id > @startId Order by _ID";
                var ldg = await _dapper.GetAllAsync<Ledger>(sqlQuery, new { startId }, CommandType.Text);

                if (ldg.Count()<1)
                {
                    goto Finish;
                }

                List<string> str = new List<string>();
                foreach (Ledger ledger in ldg)
                {
                    string[] prop = typeof(Ledger).GetProperties()
                        .Select(p => $"'{p.GetValue(ledger)?.ToString() ?? string.Empty}'")
                        .ToArray();
                    string rowString = string.Join(",", prop);
                    str.Add($"({rowString})");

                    startId = ledger.Id;
                }
                string values = string.Join(",", str.ToArray());
                sqlQuery = $@"SET IDENTITY_INSERT tbl_Ledger ON
                              INSERT INTO tbl_Ledger(_ID,_UserID,_TID,_Type,_Amount,_CurrentAmount,_LastAmount,_ServiceTypeID,_LT,
                                                    _EntryBy,_EntryDate,_Remark,_Description,_WalletID,_RFTID,_LedgerTypeID,
                                                    _EntryByName,_EntryByMobile,_OtherUserID,_ReffNo,_IsVirtual,_BankName,_UTR,
                                                    _TransactionID,_IsAutoBilling)
                                              VALUES {values}
                              SET IDENTITY_INSERT tbl_Ledger OFF";
                i = i + await _dapper.ExecuteAsync(sqlQuery, new { }, CommandType.Text, _connectionHelper.ConnectionString_bk);
                goto Select;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
Finish:
            return i;
        }

        private async Task<int> TransactionBackup()
        {
            int i = 0;
            try
            {
                int startId = 0;
                string sqlQuery = $@"{TableSchema.Transaction()} SELECT MAX(_Id) FROM tbl_Transaction(nolock)";

                startId = await _dapper.GetAsync<int>(sqlQuery, new { }, CommandType.Text, _connectionHelper.ConnectionString_bk);

Select:

                sqlQuery = $@"SELECT TOP 1000 _ID ID,_TID TID,_UserID UserID,_Account Account,_LastBalance LastBalance,_Amount Amount,
				                              _Balance Balance,_RequestedAmount RequestedAmount,_Type Type,
                                              _IsFailToSuccess IsFailToSuccess,_Remark Remark,_RefundStatus RefundStatus,
                                              _RefundRemark RefundRemark,_VendorID VendorID,_LiveID LiveID,_APIRequestID APIRequestID,
                                              _APIID APIID,_API API,_APIOpCode APIOpCode,_OID OID,_SPKey SPKey,_Operator Operator,
                                              _BillingModel BillingModel,_CommType CommType,_AmtType AmtType,_CommAtRate CommAtRate,
                                              _CommAmount CommAmount,_APICommType APICommType,_APIComAmt APIComAmt,_TDSID TDSID,
                                              _TDSAmount TDSAmount,_TDSValue TDSValue,_TaxRemark TaxRemark,_GSTTaxID GSTTaxID,
                                              _GSTTaxAmount GSTTaxAmount,_GSTValue GSTValue,_IsHoldGST IsHoldGST,_DCommRate DCommRate,
                                              _DComm DComm,_ServiceID ServiceID,_EntryBy EntryBy,_EntryDate EntryDate,
                                              _ModifyBy ModifyBy,_ModifyDate ModifyDate,_RefundRequestDate RefundRequestDate,
                                              _RefundRejectDate RefundRejectDate,_RequestModeID RequestModeID,_RequestIP RequestIP,
                                              _Optional1 Optional1,_Optional2 Optional2,_Optional3 Optional3,_Optional4 Optional4,
                                              _ExtraParam ExtraParam,_TransactionID TransactionID,_UpdatePage UpdatePage,
                                              _APIBalance APIBalance,_OutletID OutletID,_WalletID WalletID,_IsCCFModel IsCCFModel,
                                              _IsVirtual IsVirtual,_VirtualID VirtualID,_WID WID,_ApiType ApiType,_IMEI IMEI,
                                              _OpTypeID OpTypeID,_OpType OpType,_IsAdminDefined IsAdminDefined,_SlabID SlabID,
                                              _RoleID RoleID,_GroupID GroupID,_CustomerNo CustomerNo,_CCID CCID,_CCMobileNo CCMobileNo,
                                              _Customercare Customercare,_CustomercareIP CustomercareIP,_Display1 Display1,
                                              _Display2 Display2,_Display3 Display3,_Display4 Display4,_TotalToken TotalToken,
                                              _CustomerName CustomerName,_IsCommissionDistributed IsCommissionDistributed,
                                              _SwitchingID SwitchingID,_DMRModelID DMRModelID,_CircleID CircleID,
                                              _IsSameSessionUpdated IsSameSessionUpdated,_CommMax CommMax,_BookingStatus BookingStatus,
                                              _IsCircleCommission IsCircleCommission,_O5 O5,_O6 O6,_O7 O7,_O8 O8,_O9 O9,_O10 O10,
                                              _O11 O11,_O12 O12,_O13 O13,_O14 O14,_O15 O15,_O16 O16,_O17 O17,_Circle Circle,
                                              _CommAtRate2 CommAtRate2,_CommType2 CommType2,_AmtType2 AmtType2,_SplitCount SplitCount,
                                              _SenderLimitID SenderLimitID,_DailyLimitID DailyLimitID,_IsGSTVerified IsGSTVerified,
                                              _IsLimitUsed IsLimitUsed,_WIDForAPI WIDForAPI,_APIContext APIContext,_MsgId MsgId,
                                              _MaxTID MaxTID,_O18n O18n,_IsMultilevel IsMultilevel,_LapuNumber LapuNumber,
                                              _ROfferAmount ROfferAmount,_APIAmount APIAmount,_CommissionFromAPI CommissionFromAPI,
                                              _APIDebited APIDebited,_3WayReconCalled ,_3WayHitCount ,_UtilityUserID UtilityUserID,
                                             _Webhook Webhook
                        FROM tbl_Transaction(NOLOCK) WHERE _ID > @startId;";
                var transactions = await _dapper.GetAllAsync<Transaction>(sqlQuery, new { startId }, CommandType.Text);

                if (transactions.Count()<1)
                {
                    goto Finish;
                }

                List<string> str = new List<string>();
                foreach (Transaction tran in transactions)
                {
                    string[] prop = typeof(Transaction).GetProperties()
                        .Select(p => $"'{p.GetValue(tran)?.ToString() ?? string.Empty}'")
                        .ToArray();
                    string rowString = string.Join(",", prop);
                    str.Add($"({rowString})");

                    startId = tran.ID;
                }
                string values = string.Join(",", str.ToArray());
                sqlQuery = $@"SET IDENTITY_INSERT tbl_Transaction ON
                              INSERT INTO tbl_Transaction(_ID,_TID,_UserID,_Account,_LastBalance,_Amount,_Balance,_RequestedAmount,_Type,_IsFailToSuccess,_Remark,_RefundStatus,_RefundRemark,_VendorID,_LiveID,_APIRequestID,_APIID,_API,_APIOpCode,_OID,_SPKey,_Operator,_BillingModel,_CommType,_AmtType,_CommAtRate,_CommAmount,_APICommType,_APIComAmt,_TDSID,_TDSAmount,_TDSValue,_TaxRemark,_GSTTaxID,_GSTTaxAmount,_GSTValue,_IsHoldGST,_DCommRate,_DComm,_ServiceID,_EntryBy,_EntryDate,_ModifyBy,_ModifyDate,_RefundRequestDate,_RefundRejectDate,_RequestModeID,_RequestIP,_Optional1,_Optional2,_Optional3,_Optional4,_ExtraParam,_TransactionID,_UpdatePage,_APIBalance,_OutletID,_WalletID,_IsCCFModel,_IsVirtual,_VirtualID,_WID,_ApiType,_IMEI,_OpTypeID,_OpType,_IsAdminDefined,_SlabID,_RoleID,_GroupID,_CustomerNo,_CCID,_CCMobileNo,_Customercare,_CustomercareIP,_Display1,_Display2,_Display3,_Display4,_TotalToken,_CustomerName,_IsCommissionDistributed,_SwitchingID,_DMRModelID,_CircleID,_IsSameSessionUpdated,_CommMax,_BookingStatus,_IsCircleCommission,_O5,_O6,_O7,_O8,_O9,_O10,_O11,_O12,_O13,_O14,_O15,_O16,_O17,_Circle,_CommAtRate2,_CommType2,_AmtType2,_SplitCount,_SenderLimitID,_DailyLimitID,_IsGSTVerified,_IsLimitUsed,_WIDForAPI,_APIContext,_MsgId,_MaxTID,_O18n,_IsMultilevel,_LapuNumber,_ROfferAmount,_APIAmount,_CommissionFromAPI,_APIDebited,_3WayReconCalled,_3WayHitCount,_UtilityUserID,_Webhook)
                                              VALUES {values}
                              SET IDENTITY_INSERT tbl_Transaction OFF";
                i = i + await _dapper.ExecuteAsync(sqlQuery, new { }, CommandType.Text, _connectionHelper.ConnectionString_bk);
                goto Select;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
Finish:
            return i;
        }
    }
}
