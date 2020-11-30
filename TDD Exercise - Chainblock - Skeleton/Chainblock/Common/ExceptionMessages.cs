namespace Chainblock.Common
{
    public static class ExceptionMessages
    {
        public static string InvalidIdMessage = 
            "ID's cannot be zero or negative number!";
        public static string InvalidSenderUsernameMessage = 
            "Sender name cannot be empty or white space!";
        public static string InvalidReceiverUsernameMessage =
            "Receiver name cannot be empty or white space!";
        public static string InvalidTransactionAmountMessage =
            "Transaction amount should be greater than 0!";


        public static string AddingExistingTransactionMessage =
            "Transaction already exists in our records!";
        public static string ChangingStatusOfNonExistingTransaction =
            "You cannot change the status of non-existing transaction";
        public static string NonExistingTransactionMessage =
            "Transaction with given ID not found!";
        public static string RemovingNonExistingTransactionMessage =
            "You cannot remove non existing transaction!";
        public static string EmptyStatusTransactionCollectionMessage =
            "There are not transactions matching with the provided transaction status!";
        public static string NoTransactionsForGivenSenderMessage =
            "There are no corresponding transactions for this sender with given name!";
        public static string NoTransactionsForGivenReceiverMessage =
            "There are no corresponding transactions for this receiver with given name!";
    }
}
