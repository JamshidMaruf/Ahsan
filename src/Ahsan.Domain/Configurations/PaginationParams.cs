namespace Ahsan.Domain.Configurations
{
    public class PaginationParams
    {
        private const short _maxSize = 10;
        private const short _minSize = 1;
        private short _pageSize = 1;
        private int _pageIndex = 1;
        public short PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxSize ? _maxSize : value < _minSize ? _minSize : value;
        }

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 0 ? _pageIndex : value;
        }
    }
}
