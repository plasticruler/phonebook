namespace PhoneCall.API.Domain.Services.Communication{
    public class ApiResponse<T>:ApiResponseBase{
        public T Item {get;private set;}
         private ApiResponse(bool success, string message, T item):base(success, message){
                Item = item;
         }
         public ApiResponse(T item):this(true,string.Empty,item){}
         public ApiResponse(string message):this(false,message,default(T)){} 
    }   
}