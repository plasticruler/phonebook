namespace PhoneCall.API.Domain.Services.Communication{
    public abstract class ApiResponseBase{
        public bool Success{get;set;}
        public string Message{get;set;}
        public ApiResponseBase(bool success, string message){
            Success=success;
            Message = message;
        }

    }
}