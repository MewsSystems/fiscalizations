namespace Mews.Fiscalization.Italy.Communication
{
    public class SdiNotificationAction
    {
        public const string DeliveryReceiptNotification = "http://www.fatturapa.it/TrasmissioneFatture/RicevutaConsegna";
        public const string FailedDeliveryNotification = "http://www.fatturapa.it/TrasmissioneFatture/NotificaMancataConsegna";
        public const string RejectionNotification = "http://www.fatturapa.it/TrasmissioneFatture/NotificaScarto";
        public const string OutcomeNotification = "http://www.fatturapa.it/TrasmissioneFatture/NotificaEsito";
        public const string DeadlinePassedNotification = "http://www.fatturapa.it/TrasmissioneFatture/NotificaDecorrenzaTermini";
        public const string ImpossibleDeliveryNotification = "http://www.fatturapa.it/TrasmissioneFatture/AttestazioneTrasmissioneFattura";
    }
}
