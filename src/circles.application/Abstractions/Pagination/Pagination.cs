using circles.api.contracts.Abstractions.Paginations;
using Microsoft.EntityFrameworkCore;


namespace circles.application.Abstractions.Pagination;

public class Pagination<T>
{
    public async Task<PagingResult<T>> FetchPaginatedDataAsync(
        IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {

        if (pageNumber <= 0)
            pageNumber = 1;

        var skipAmount = pageSize * (pageNumber - 1);

        var fetchData = await query
                                .Skip(skipAmount)
                                .Take(pageSize)
                                .ToListAsync();

        return new PagingResult<T>
        {
            Items = fetchData,
            PageNumber = pageNumber,
            PageSize = pageSize,
            LastPage = (await query.CountAsync() / pageSize) + (await query.CountAsync() % pageSize > 0 ? 1 : 0)
        };

    }

}
