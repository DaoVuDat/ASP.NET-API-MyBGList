namespace MyBGList_ApiVersion.DTO.v1;

public class RestDTO<T>
{
    public List<LinkDTO[]> Links { get; set; } = new();

    public T Data { get; set; } = default!;
}