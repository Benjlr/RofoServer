namespace RofoServer.Core.Logic.AccountConfirmation
{
    public class AccountConfirmationEmailResponseModel : ResponseBase
    {
        public string ValidationCode { get; set; }
        public string CallbackUrl { get; set; }

    }
}
