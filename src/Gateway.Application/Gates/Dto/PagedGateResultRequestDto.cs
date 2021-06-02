namespace Gateway.Gates.Dto
{
    using Abp.Application.Services.Dto;

    /// <summary>
    ///     The paged Gate result request DTO.
    /// </summary>
    public class PagedGateResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        /// <summary>
        ///     Gets or sets a value indicating whether descending.
        /// </summary>
        public bool Descending { get; set; }       

        /// <summary>
        ///     Gets or sets the keyword.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        ///     Gets or sets the sorting.
        /// </summary>
        public string Sorting { get; set; }
    }
}
