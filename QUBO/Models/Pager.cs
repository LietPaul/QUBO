namespace QUBO.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pager() { }
        public Pager(int totalitems, int page, int pagesize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalitems / (decimal)pagesize);
            int currentPage = page;

            int startpage = currentPage - 5;
            int endpage = currentPage + 4;

            if (startpage <= 0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }
            if (endpage > totalPages)
            {
                endpage = totalPages;
                if (endpage > 10)
                {
                    startpage = endpage - 9;
                }
            }

            TotalItems = totalitems;
            CurrentPage = currentPage;
            PageSize = pagesize;
            TotalPages = totalPages;
            StartPage = startpage;
            EndPage = endpage;
        }
    }
}
