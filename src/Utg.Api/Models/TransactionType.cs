namespace Utg.Api.Models
{
    public enum TransactionType : byte
    {
        Auth = 1,
        Refund = 2,
        Forced = 3,
        Reversal = 4,
        Void = 5,
        Sale = 6,
        Capture = 7,
        PayCredit = 8,
        PayCreditReverse = 9,
        PayDebit = 10,
        PayDebitReverse = 11,
        AchCredit = 12,
        AchCreditReverse = 13,
        AchDebit = 14,
        AchDebitReverse = 15,
        FullReversal = 16,
        Incremental = 17,
        Reversed = 18,
        OpenBatch=19,
        CloseBatch=20,
        BatchSettlement=21
    }
}