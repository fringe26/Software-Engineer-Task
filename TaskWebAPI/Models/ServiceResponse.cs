namespace TaskWebAPI.Models
{
    public class ServiceResponse<T>
    {
        //Делаю универсальную обертку над возвращаемым ответом, всех сервисов
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = String.Empty;



    }
}
