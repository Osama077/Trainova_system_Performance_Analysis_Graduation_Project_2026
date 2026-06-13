namespace Trainova.Infrastructure.DataAccess.IdempotencyModel
{
    public class IdempotentRequest
    {
        public Guid RequestId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
