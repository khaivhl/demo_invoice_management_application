namespace API_them_sua_xoa_du_lieu.Helper
{
    public class Pagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage 
        {
            get
            {
                if (PageSize == 0) return 0;
                var total = TotalCount / PageSize;
                if (TotalCount % PageSize != 0)
                {
                    total++;
                }
                return total;
            }
        }
        public Pagination()
        {
            PageSize = -1;
            PageNumber = 1;
        }
    }
}
