namespace Electronic_Application.Models.Common
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(T data, int dataLength)
        {
            Data = data;
            DataLength = dataLength;
        }
        public T Data { get; set; }
        public int DataLength { get; set; }
    }
}
