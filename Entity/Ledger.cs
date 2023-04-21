using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Ledger
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int TID { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal LastAmount { get; set; }
        public int ServiceTypeID { get; set; }
        public int LT { get; set; }
        public int EntryBy { get; set; }
        public string EntryDate { get; set; }
        public string Remark { get; set; }
        public string Description { get; set; }
        public string WalletID { get; set; }
        public string RFTID { get; set; }
        public string LedgerTypeID { get; set; }
        public string EntryByName { get; set; }
        public string EntryByMobile { get; set; }
        public string OtherUserID { get; set; }
        public string ReffNo { get; set; }
        public string IsVirtual { get; set; }
        public string BankName { get; set; }
        public string UTR { get; set; }
        public string TransactionID { get; set; }
        public string IsAutoBilling { get; set; }
    }
}