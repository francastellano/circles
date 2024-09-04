namespace circles.api.contracts.Abstractions.Paginations;

public class PagingResult<T>
{
    public List<T> Items { get; set; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; set; }
    public int LastPage { get; init; }

}
